using Data.Data_Server;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Data
{
    public class AppDbContextTests
    {
        private AppDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task CanAddRetrieverOrder()
        {
            using var context = CreateInMemoryDbContext();

            var order = new Order
            {
                CustomerName = "John Doe",
                Address = "123 Main St",
                PhoneNumber = "123-456-789",
                TotalPrice = 50M
            };

            context.Orders.Add(order);
            await context.SaveChangesAsync();

            var retrieverOrder = await context.Orders.FindAsync(order.Id);
            Assert.NotNull(retrieverOrder);
            Assert.Equal("John Doe", retrieverOrder.CustomerName);
        }






    }
}
