using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AlbelliTA05.Models;
using System;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace AlbelliTA05.Controllers
{
    public class ProductsController : ApiController
    {
        private OrdersContext db = new OrdersContext();

        // Project products to product DTOs.
        private IQueryable<ProductDTO> MapProducts()
        {
            return from p in db.Products
                   select new ProductDTO() { Id = p.Id, Name = p.Name, Width = p.Width, MaxStack = p.MaxStack };
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            return MapProducts().AsEnumerable();
        }

        public ProductDTO GetProduct(int id)
        {
            var product = (from p in MapProducts()
                           where p.Id == id
                           select p).FirstOrDefault();
            if (product == null)
            {
                throw new HttpResponseException(
                    Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return product;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}