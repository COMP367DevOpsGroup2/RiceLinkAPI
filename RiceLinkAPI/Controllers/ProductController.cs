using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiceLinkAPI.Models;
using RiceLinkAPI.Models.Products;
using System;

namespace RiceLinkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }


        // GET: api/Products
        [HttpGet]
        [Route("~/api/Products")]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return Ok(await _context.Products.ToListAsync());
        }

        // GET: api/Product?productId=5
        [HttpGet("")]
        public async Task<ActionResult<Product>> GetProduct([FromQuery(Name = "productId")] int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }

        // GET: api/Product/search?name=rice
        [HttpGet("search")]
        public async Task<ActionResult<List<Product>>> SearchProductsByName([FromQuery] ProductSearchModel searchModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var matchingProducts = await _context.Products
                                                .Where(p => EF.Functions.Like(p.Name, $"%{searchModel.Name}%"))
                                                .ToListAsync();

            if (!matchingProducts.Any())
            {
                return NotFound("No products found with the given name.");
            }

            return Ok(matchingProducts);
        }



    }
}
