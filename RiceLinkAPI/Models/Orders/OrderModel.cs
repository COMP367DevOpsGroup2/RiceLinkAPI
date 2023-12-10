using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiceLinkAPI.Models.Orders
{
    public class OrderModel
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one item is required in the order.")]
        public List<CreateOrderItemModel> Items { get; set; } = new List<CreateOrderItemModel>();
    }

    public class CreateOrderItemModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
    }
    
    //Implementing Data Transfer Objects (DTOs) that represent the data to send over by the API.
    //This approach helps avoid issues with circular references and gives more control over the data exposed to the API consumers.

    public class OrderDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }

    public class OrderItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

}
