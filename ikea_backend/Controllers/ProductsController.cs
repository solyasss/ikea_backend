using ikea_data.Data;
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
            var product = await _db.Products
                .Where(p => p.Id == id)
                .Select(p => new
                {
                    Product = p,
                    Category = p.Category,
                    ProductCharacteristics = _db.ProductCharacteristics.Where(pc => pc.ProductId == p.Id).ToList(),
                    ProductComments = _db.ProductComments.Where(pc => pc.ProductId == p.Id).ToList(),
                    ProductImages = _db.ProductImages.Where(pi => pi.ProductId == p.Id).ToList(),
                    SetItems = _db.SetItems.Where(si => si.ProductId == p.Id).ToList()
                })
                .FirstOrDefaultAsync();

            if (product == null)
                return NotFound();

            return Ok(new
            {
                product.Product,
                product.Category,
                product.ProductCharacteristics,
                product.ProductComments,
                product.ProductImages,
                product.SetItems
            });
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
