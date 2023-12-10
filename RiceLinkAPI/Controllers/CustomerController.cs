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

        // POST: api/Customer
        [HttpPost]
        public async Task<ActionResult<CustomerModel>> CreateCustomer([FromBody] CreateCustomerRequest request)
        {
            var customer = new CustomerModel
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                Company = request.Company,
                Address = request.Address
            };

            _context.CustomerModel.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { customerId = customer.CustomerId }, customer);
        }

        // PUT: api/Customer
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpsertCustomerRequest request)
        {
            var customer = await _context.CustomerModel.FindAsync(request.CustomerId);

            if (customer == null)
            {
                return NotFound("Customer not found");
            }

            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.Email = request.Email;
            customer.Phone = request.Phone;
            customer.Company = request.Company;
            customer.Address = request.Address;

            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
