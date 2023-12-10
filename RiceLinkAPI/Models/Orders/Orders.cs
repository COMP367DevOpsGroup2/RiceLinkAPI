using RiceLinkAPI.Models.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RiceLinkAPI.Models.Orders
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [Required]
        [StringLength(255)]
        public string Status { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

        [Required]
        public int CustomerId { get; set; }

        // Navigation property to Customer
        [ForeignKey("CustomerId")]
        public CustomerModel Customer { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }

    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitPrice { get; set; }

        [Required]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }

    public class UpdateOrderModel
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [Required]
        [StringLength(255)]
        public string Status { get; set; } = string.Empty;

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public List<UpdateOrderItemModel> Items { get; set; } = new List<UpdateOrderItemModel>();
    }

    public class UpdateOrderItemModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
    }

    public class PatchOrderModel
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? OrderDate { get; set; }

        [StringLength(255)]
        public string? Status { get; set; }

        public decimal? TotalPrice { get; set; }

        public List<PatchOrderItemModel> Items { get; set; } = new List<PatchOrderItemModel>();
    }

    public class PatchOrderItemsModel
    {
        [Required]
        public int OrderId { get; set; }

        public List<PatchOrderItemModel> Items { get; set; } = new List<PatchOrderItemModel>();
    }
    public class PatchOrderItemModel
    {
        [Required]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
    }

}

#nullable restore