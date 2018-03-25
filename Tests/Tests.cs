using Scan;
using System;
using Xunit;
using System.Linq;

namespace Tests
{
    public class Tests
    {
        
        [Fact]
        public void GetAllProducts()
        {
            var service = new DataService();
            var product = service.GetProducts();
            Assert.Equal(8, product.Count);
            Assert.Equal("test1", product.First().Name);
        }

        [Fact]
        public void GetProduct()
        {
            var service = new DataService();
            var product = service.GetProduct(5944760000000);
            Assert.Equal("option3", product.Name);
        }

        /*[Fact]
         public void CreateProduct()
       {
           var service = new DataService();
           var product = service.CreateProduct(12345, "test1");
           Assert.True(product.Code > 0);
           Assert.Equal("test1", product.Name);

       }*/

        [Fact]
        public void DeleteProduct()
        {
            var service = new DataService();
            var product = service.CreateProduct(111, "test");
            var result = service.DeleteProduct(product.Code);
            Assert.True(result);
            product = service.GetProduct(product.Code);
            Assert.Null(product);
        }

        [Fact]
        public void UpdateProduct()
        {
            var service = new DataService();
            var product = service.CreateProduct(222, "test");
            var result = service.UpdateProduct(product.Code, "UpdatedName");
            Assert.True(result);
            product = service.GetProduct(product.Code);
            Assert.Equal("UpdatedName", product.Name);
            // cleanup
            service.DeleteProduct(product.Code);
        }

        [Fact]
        public void GetProduct_List()
        {
            var service = new DataService();
            var list = service.GetList();
            Assert.Equal("test1", list.First().Name);
        }

        [Fact]
        public void CreateProduct_List()
        {
            var service = new DataService();
            var list = service.CreateProduct_List("test2");
            Assert.Equal("test2", list.Name);

        }

        [Fact]
        public void DeleteProduct_List()
        {
            var service = new DataService();
            var list = service.CreateProduct_List("testing");
            var result = service.DeleteProduct_List(list.Name);
            Assert.True(result);
        }
    }
}
