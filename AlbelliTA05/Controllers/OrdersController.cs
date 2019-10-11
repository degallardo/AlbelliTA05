using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AlbelliTA05.Models;

namespace AlbelliTA05.Controllers
{
    //[Authorize]
    public class OrdersController : ApiController
    {
        private OrdersContext db = new OrdersContext();

        // GET api/Orders
        public IEnumerable<Order> GetOrders()
        {
            return db.Orders;
        }

        // GET api/Orders/5
        public OrderDTO GetOrder(int id)
        {
            Order order = db.Orders.Include("OrderDetails.Product")
                .First(o => o.Id == id);
            if (order == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            OrderDTO orderDTO = new OrderDTO()
            {
                Details = from d in order.OrderDetails
                          select new OrderDTO.Detail()
                          {
                              ProductID = d.Product.Id,
                              Product = d.Product.Name,
                              Width = d.Product.Width,
                              MaxStack = d.Product.MaxStack,
                              Quantity = d.Quantity
                          }
            };

            orderDTO.RequiredBinWidth = order.RequiredBinWidth;

            return orderDTO;
        }

        // POST api/Orders
        public HttpResponseMessage PostOrder(OrderDTO dto)
        {
            if (ModelState.IsValid)
            {
                double orderWidth = 0;
                foreach(OrderDTO.Detail orderDetail in dto.Details)
                {
                    orderWidth += orderDetail.Width * Math.Ceiling(orderDetail.Quantity / orderDetail.MaxStack);
                }
                dto.RequiredBinWidth = orderWidth;
                var order = new Order()
                {
                    Customer = User.Identity.Name,
                    OrderDetails = (from item in dto.Details
                                    select new OrderDetail() { ProductId = item.ProductID, Quantity = item.Quantity }).ToList()
                };

                order.RequiredBinWidth = orderWidth;

                db.Orders.Add(order);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, order);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = order.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}