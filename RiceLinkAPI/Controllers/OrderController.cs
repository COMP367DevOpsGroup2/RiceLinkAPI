using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiceLinkAPI.Models;
using RiceLinkAPI.Models.Orders;
using RiceLinkAPI.Models.Customer;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RiceLinkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        [Route("~/api/Orders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .ToListAsync();

            var orderDtos = orders.Select(o => new OrderDto
            {
                CustomerId = o.CustomerId,
                OrderId = o.OrderId,
                OrderDate = o.OrderDate,
                Status = o.Status,
                TotalPrice = o.TotalPrice,
                Items = o.Items.Select(i => new OrderItemDto
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            }).ToList();

            return Ok(orderDtos);
        }//End GetOrders

        // GET: api/Order?orderId=5001
        [HttpGet("")]
        public async Task<ActionResult<OrderDto>> GetOrder([FromQuery(Name = "orderId")] int id)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound("Order not found");
            }

            // Map the Order entity to OrderDto
            var orderDto = new OrderDto
            {
                CustomerId = order.CustomerId,
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                Status = order.Status,
                TotalPrice = order.TotalPrice,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            return Ok(orderDto);
        }//End GetOrder

    }//End of Controller 
}
