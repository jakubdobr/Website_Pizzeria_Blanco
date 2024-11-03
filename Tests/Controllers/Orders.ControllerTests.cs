using Xunit;
using Moq;
using Api.Controllers;
using Data.Data_Server;
using Data.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Api.Services;

namespace Tests.Controllers
{
    public class OrdersControllerTests
    {
        private readonly OrdersController _controller;
        private readonly Mock<IAddressVerificationService> _addressVerificationServiceMock; 
        private readonly Mock<ILogger<OrdersController>> _loggerMock;
        private readonly AppDbContext _dbContext;

        public OrdersControllerTests()
        {
            // Konfiguracja SQLite In-Memory dla AppDbContext
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("Filename=:memory:") // SQLite w trybie in-memory
                .Options;

            _dbContext = new AppDbContext(options);
            _dbContext.Database.OpenConnection(); // Otwiera połączenie z bazą danych w pamięci
            _dbContext.Database.EnsureDeleted(); // Usuwa istniejącą bazę danych
            _dbContext.Database.EnsureCreated(); // Tworzy strukturę bazy danych na podstawie modelu

            // Tworzenie mocków zależności
            _addressVerificationServiceMock = new Mock<IAddressVerificationService>(); // Zmieniono na IAddressVerificationService
            _loggerMock = new Mock<ILogger<OrdersController>>();

            // Inicjalizacja kontrolera
            _controller = new OrdersController(_dbContext, _addressVerificationServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetOrders_ReturnsOkResult_WithListOfOrders()
        {
            // Arrange
            _dbContext.Orders.Add(new Order
            {
                Id = 1,
                CustomerName = "Test Customer",
                Address = "123 Test St",
                OrderDate = System.DateTime.Now,
                TotalPrice = 100M,
                PhoneNumber = "123-456-789",
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { ProductId = 1, Quantity = 2, Price = 50M }
                }
            });
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _controller.GetOrders();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var orders = Assert.IsType<List<OrderDto>>(okResult.Value);
            Assert.Single(orders); // Sprawdzamy, czy jest tylko jedno zamówienie
        }

        [Fact]
        public async Task GetOrder_ReturnsOkResult_WithOrderDto()
        {
            // Arrange
            var order = new Order
            {
                Id = 2,
                CustomerName = "Test Customer 2",
                Address = "456 Test St",
                OrderDate = System.DateTime.Now,
                TotalPrice = 150M,
                PhoneNumber = "123-456-7890",
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { ProductId = 2, Quantity = 3, Price = 50M }
                }
            };
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _controller.GetOrder(order.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedOrder = Assert.IsType<OrderDto>(okResult.Value);
            Assert.Equal(order.Id, returnedOrder.Id);
        }

        [Fact]
        public async Task GetOrder_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            // Act
            var result = await _controller.GetOrder(999); // Nieistniejące ID

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostOrder_ReturnsCreatedAtAction_WithOrderDto()
        {
            // Arrange
            var createOrderDto = new CreateOrderDto
            {
                CustomerName = "New Customer",
                Address = "789 New St",
                PhoneNumber = "123-456-7890",
                OrderItems = new List<CreateOrderItemDto>
                {
                    new CreateOrderItemDto { ProductId = 3, Quantity = 1, Price = 200M }
                }
            };

            // Konfiguracja mocka AddressVerificationService do zwrócenia true
            _addressVerificationServiceMock.Setup(service => service.VerifyAddressAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.PostOrder(createOrderDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var orderDto = Assert.IsType<OrderDto>(createdAtActionResult.Value);
            Assert.Equal(createOrderDto.CustomerName, orderDto.CustomerName);
            Assert.Single(orderDto.OrderItems);
            Assert.Equal(200M, orderDto.TotalPrice);
        }

        [Fact]
        public async Task PostOrder_ReturnsBadRequest_WhenAddressIsInvalid()
        {
            // Arrange
            var createOrderDto = new CreateOrderDto
            {
                CustomerName = "Invalid Address Customer",
                Address = "Invalid Address",
                PhoneNumber = "123-456-7890",
                OrderItems = new List<CreateOrderItemDto>
                {
                    new CreateOrderItemDto { ProductId = 4, Quantity = 1, Price = 100M }
                }
            };

            // Konfiguracja mocka AddressVerificationService do zwrócenia false
            _addressVerificationServiceMock.Setup(service => service.VerifyAddressAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.PostOrder(createOrderDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Invalid address", badRequestResult.Value);
        }
    }
}
