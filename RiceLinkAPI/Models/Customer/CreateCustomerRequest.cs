using System.ComponentModel.DataAnnotations;

namespace RiceLinkAPI.Models.Customer
{
    public class CreateCustomerRequest
    {
        [Required(ErrorMessage = "FirstName is required.")]
        [StringLength(50, ErrorMessage = "FirstName size must be less than 50 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "LastName is required.")]
        [StringLength(50, ErrorMessage = "LastName size must be less than 50 characters.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(20, ErrorMessage = "Phone number must be less than 20 characters.")]
        public string Phone { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Company must be less than 50 characters.")]
        public string? Company { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address must be less than 200 characters.")]
        public string Address { get; set; } = string.Empty;
    }
}
