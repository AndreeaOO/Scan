/*using EfExample;
using System;
using Xunit;
using System.Linq;

namespace Tests
{
    public class Tests
    {
        public void CreateCategory_ValidData_CreteCategoryAndRetunsNewObject()
        {
            var service = new DataService();
            var category = service.CreateProduct(123, "test");
            Assert.True(category.Id > 0);
            Assert.Equal("Test", category.Name);
            Assert.Equal("CreateCategory_ValidData_CreteCategoryAndRetunsNewObject", category.Description);

            // cleanup
            service.DeleteCategory(category.Id);
        }
    }
}*/
