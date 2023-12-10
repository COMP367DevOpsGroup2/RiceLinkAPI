using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiceLinkAPI.Models;
using RiceLinkAPI.Models.Customer;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RiceLinkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        [Route("~/api/Customers")]
        public async Task<ActionResult<IEnumerable<CustomerModel>>> GetCustomers()
        {
            return await _context.CustomerModel.ToListAsync();
        }

        // GET: api/Customer?CustomerId=5
        [HttpGet("")]
        public async Task<ActionResult<CustomerModel>> GetCustomer([FromQuery] int customerId)
        {
            var customer = await _context.CustomerModel.FindAsync(customerId);

            if (customer == null)
            {
                return NotFound("Customer not found");
            }

            return customer;
        }

    }
}
