using System.ComponentModel.DataAnnotations;

namespace RiceLinkAPI.Models.Customer
{
    public class PatchCustomerRequest
    {
        [Required(ErrorMessage = "CustomerId is required.")]
        public int CustomerId { get; set; }

      
        [StringLength(50, ErrorMessage = "FirstName size must be less than 50 characters.")]
        public string? FirstName { get; set; }

 
        [StringLength(50, ErrorMessage = "LastName size must be less than 50 characters.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(50, ErrorMessage = "Company must be less than 50 characters.")]
        public string? Company { get; set; }

    
        [StringLength(200, ErrorMessage = "Address must be less than 200 characters.")]
        public string? Address { get; set; }
    }
}
