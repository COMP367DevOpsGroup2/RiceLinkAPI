using System.ComponentModel.DataAnnotations;

namespace RiceLinkAPI.Models.Products
{
    public class CreateProductRequest
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name must be less than 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "Origin must be less than 100 characters.")]
        public string Origin { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "Package size must be less than 50 characters.")]
        public string PackageSize { get; set; } = string.Empty;

        [Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10000.")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Currency must be less than 10 characters.")]
        public string Currency { get; set; } = string.Empty;

       //InStock is a required boolean
        public bool InStock { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int Quantity { get; set; }

        [StringLength(500, ErrorMessage = "Description must be less than 500 characters.")]
        public string Description { get; set; } = string.Empty;
    }
}
