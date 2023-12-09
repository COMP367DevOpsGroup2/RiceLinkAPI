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


        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct([FromBody] CreateProductRequest request)
        {
            var newProduct = new Product
            {
                Name = request.Name,
                Origin = request.Origin,
                PackageSize = request.PackageSize,
                Price = request.Price,
                Currency = request.Currency,
                InStock = request.InStock,
                Quantity = request.Quantity,
                Description = request.Description
            };

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
        }

        // PUT: api/Product
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpsertProductRequest request)
        {
            if (!request.Id.HasValue)
            {
                return BadRequest("Product ID is required");
            }

            var product = await _context.Products.FindAsync(request.Id.Value);
            if (product == null)
                return NotFound("Product not found");

            // Update only the fields that are provided (not null)
            if (request.Name != null)
                product.Name = request.Name;

            if (request.Origin != null)
                product.Origin = request.Origin;

            if (request.PackageSize != null)
                product.PackageSize = request.PackageSize;

            if (request.Price.HasValue)
                product.Price = request.Price.Value;

            if (request.Currency != null)
                product.Currency = request.Currency;

            if (request.InStock.HasValue)
                product.InStock = request.InStock.Value;

            if (request.Quantity.HasValue)
                product.Quantity = request.Quantity.Value;

            if (request.Description != null)
                product.Description = request.Description;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent(); // return NoContent (204) for successful PUT requests
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(e => e.Id == request.Id.Value))
                {
                    return NotFound("Product not found");
                }
                else
                {
                    throw;
                }
            }
        }

        // PATCH: api/Product
        [HttpPatch]
        public async Task<IActionResult> PatchProduct([FromBody] PatchProductRequest productUpdate)
        {
            if (!productUpdate.Id.HasValue)
            {
                return BadRequest("Product ID is required");
            }

            var product = await _context.Products.FindAsync(productUpdate.Id);

            if (product == null)
            {
                return NotFound("Product not found");
            }

            // Update the product with new values from productUpdate
            if (productUpdate.Name != null)
                product.Name = productUpdate.Name;

            if (productUpdate.Origin != null)
                product.Origin = productUpdate.Origin;

            if (productUpdate.PackageSize != null)
                product.PackageSize = productUpdate.PackageSize;

            if (productUpdate.Price.HasValue)
                product.Price = productUpdate.Price.Value;

            if (productUpdate.Currency != null)
                product.Currency = productUpdate.Currency;

            if (productUpdate.InStock.HasValue)
                product.InStock = productUpdate.InStock.Value;

            if (productUpdate.Quantity.HasValue)
                product.Quantity = productUpdate.Quantity.Value;

            if (productUpdate.Description != null)
                product.Description = productUpdate.Description;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(e => e.Id == productUpdate.Id))
                {
                    return NotFound("Product not found");
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE: api/Product?id=5
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromQuery] int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound("Product not found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent(); // NoContent (204) for successful DELETE requests
        }




    }
}
