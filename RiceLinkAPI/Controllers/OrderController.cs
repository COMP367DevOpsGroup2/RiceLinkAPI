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

        // POST: api/Order
        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] OrderModel orderModel)
        {
            // Check if CustomerId is valid
            var customer = await _context.CustomerModel.FindAsync(orderModel.CustomerId);
            if (customer == null)
            {
                return BadRequest("Invalid CustomerId");
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    List<OrderItem> orderItems = new List<OrderItem>();
                    foreach (var item in orderModel.Items)
                    {
                        var product = await _context.Products
                            .Where(p => p.Id == item.ProductId)
                            .FirstOrDefaultAsync();

                        if (product == null)
                        {
                            throw new InvalidOperationException($"ProductId {item.ProductId} not found.");
                        }

                        if (item.Quantity > product.Quantity)
                        {
                            throw new InvalidOperationException($"Insufficient quantity for ProductId {item.ProductId}. Available quantity: {product.Quantity}.");
                        }

                        // Deduct the quantity from the product
                        product.Quantity -= item.Quantity;

                        var orderItem = new OrderItem
                        {
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            UnitPrice = product.Price
                        };
                        orderItems.Add(orderItem);
                    }

                    var newOrder = new Order
                    {
                        CustomerId = orderModel.CustomerId,
                        OrderDate = orderModel.OrderDate,
                        Status = "Reserved",
                        Items = orderItems,
                        TotalPrice = orderItems.Sum(i => i.UnitPrice * i.Quantity)
                    };

                    _context.Orders.Add(newOrder);
                    await _context.SaveChangesAsync();

                    // The IDs should be updated after SaveChangesAsync
                    var orderDto = new OrderDto
                    {
                        OrderId = newOrder.OrderId,
                        OrderDate = newOrder.OrderDate,
                        Status = newOrder.Status,
                        TotalPrice = newOrder.TotalPrice,
                        Items = newOrder.Items.Select(i => new OrderItemDto
                        {
                            Id = i.Id,
                            ProductId = i.ProductId,
                            Quantity = i.Quantity,
                            UnitPrice = i.UnitPrice
                        }).ToList()
                    };

                    await transaction.CommitAsync();

                    return CreatedAtAction(nameof(GetOrder), new { id = orderDto.OrderId }, orderDto);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return BadRequest(ex.Message);
                }
            }
        }//End CreateOrder

    }//End of Controller 
}
