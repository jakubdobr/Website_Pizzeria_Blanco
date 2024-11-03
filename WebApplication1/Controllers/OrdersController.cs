using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Models;
using Data.Data_Server;
using Api.Services;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAddressVerificationService _addressVerificationService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(AppDbContext context, IAddressVerificationService addressVerificationService, ILogger<OrdersController> logger)
        {
            _context = context;
            _addressVerificationService = addressVerificationService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .Select(order => new OrderDto
                {
                    Id = order.Id,
                    CustomerName = order.CustomerName,
                    Address = order.Address,
                    OrderDate = order.OrderDate,
                    TotalPrice = order.TotalPrice,
                    OrderItems = order.OrderItems.Select(item => new OrderItemDto
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Price
                    }).ToList()
                }).ToListAsync();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.Id == id)
                .Select(order => new OrderDto
                {
                    Id = order.Id,
                    CustomerName = order.CustomerName,
                    Address = order.Address,
                    OrderDate = order.OrderDate,
                    TotalPrice = order.TotalPrice,
                    OrderItems = order.OrderItems.Select(item => new OrderItemDto
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Price
                    }).ToList()
                }).FirstOrDefaultAsync();

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> PostOrder(CreateOrderDto createOrderDto)
        {
            // Weryfikacja adresu
            var isWithinRange = await _addressVerificationService.IsWithinDeliveryRangeAsync(createOrderDto.Address, 30); // 30 km limit

            if (!isWithinRange)
            {
                return BadRequest("Address is outside the delivery range.");
            }

            // Tworzenie nowego zamówienia
            var order = new Order
            {
                CustomerName = createOrderDto.CustomerName,
                Address = createOrderDto.Address,
                PhoneNumber = createOrderDto.PhoneNumber,
                OrderDate = DateTime.Now,
                TotalPrice = createOrderDto.OrderItems.Sum(item => item.Price * item.Quantity),
                OrderItems = createOrderDto.OrderItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();


            var orderDto = new OrderDto
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                Address = order.Address,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                OrderItems = order.OrderItems.Select(item => new OrderItemDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            };

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, orderDto);
        }
    }
}
