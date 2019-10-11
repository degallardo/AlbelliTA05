using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http.Results;
using AlbelliTA05.Controllers;
using AlbelliTA05.Models;

namespace AlbelliTA05.Tests
{
    [TestClass]
    class TestProductsController
    {
        private List<Product> GetTestProducts()
        {
            var testProducts = new List<Product>()
            {
                new Product() { Name = "Photo Book", Width = 19, MaxStack = 1 },
                new Product() { Name = "Calendar", Width = 10, MaxStack = 1 },
                new Product() { Name = "Canvas", Width = 16, MaxStack = 1 },
                new Product() { Name = "Greeting Cards", Width = 4.7, MaxStack = 1 },
                new Product() { Name = "Mug", Width = 94, MaxStack = 4 }
            };

            return testProducts;
        }

        [TestMethod]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            var testProducts = GetTestProducts();
            var controller = new ProductsController();

            var result = controller.GetProducts() as List<Product>;
            Assert.AreEqual(testProducts.Count, result.Count);
        }

        [TestMethod]
        public void GetProduct_ShouldReturnCorrectProduct()
        {
            var testProducts = GetTestProducts();
            var controller = new ProductsController();

            var result = controller.GetProduct(4) as ProductDTO;
            Assert.IsNotNull(result);
            Assert.AreEqual(testProducts[3].Name, result.Name);
        }
    }
}
