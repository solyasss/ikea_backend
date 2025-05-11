using ikea_backend.Data;
using ikea_data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ikea_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IkeaDbContext _db;

        public ProductsController(IkeaDbContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            return await _db.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var products = await _db.Products.SingleOrDefaultAsync(p => p.Id == id);
            if (products == null)
            {
                return NotFound();
            }
            return new ObjectResult(products);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Product input)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();

            product.Name = input.Name;
            product.Slug = input.Slug;
            product.Price = input.Price;
            product.MainImage = input.MainImage;
            product.CategoryId = input.CategoryId;

            await _db.SaveChangesAsync();

            return Ok(product);
        }


        [HttpPost]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            var product = await _db.Products.SingleOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return Ok(product);
        }
    }
}
