using System.ComponentModel.DataAnnotations;

namespace RiceLinkAPI.Models.Customer
{
    public class UpsertCustomerRequest
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string Email { get; set; }
        public string Phone { get; set; } 
        public string? Company { get; set; } 
        public string Address { get; set; }
    }
}

