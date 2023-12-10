using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RiceLinkAPI.Models.Customer
{
    public class UpsertCustomerRequest
    {
        [Required(ErrorMessage = "CustomerId is required.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "FirstName is required.")]
        [StringLength(50, ErrorMessage = "FirstName size must be less than 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        [StringLength(50, ErrorMessage = "LastName size must be less than 50 characters.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [StringLength(20, ErrorMessage = "Phone number must be less than 20 characters.")]
        public string Phone { get; set; }

        [StringLength(50, ErrorMessage = "Company must be less than 50 characters.")]
        public string? Company { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address must be less than 200 characters.")]
        public string Address { get; set; }
    }
}

#nullable restore