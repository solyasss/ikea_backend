using ikea_business.DTO;
using ikea_data.Data;
using ikea_data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ikea_backend.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IkeaDbContext _db;
        public ProductsController(IkeaDbContext db) => _db = db;

        // ---------- GET: api/products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _db.Products
                .Include(p => p.Category)
                .Include(p => p.Images)
                .Include(p => p.Characteristics)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.User)
                .Select(p => new
                {
                    p.Id,
                    p.Article,
                    p.Name,
                    p.Price,
                    p.MainImage,
                    p.Color,
                    p.Dimensions,
                    p.Weight,
                    p.Type,
                    p.CountryOfOrigin,
                    p.PackageContents,
                    p.Warranty,
                    p.Materials,
                    p.Rating,
                    Category   = new { p.Category!.Id, p.Category.Title },
                    Images      = p.Images          .OrderBy(i => i.SortOrder).Select(i => i.ImageUrl),
                    Characteristics = p.Characteristics.Select(c => new { c.Name, c.Value }),
                    Comments    = p.Comments        .Select(c => new
                    {
                        c.Id,
                        c.CommentText,
                        c.Rating,
                        User = new { c.User!.Id, c.User.FirstName, c.User.LastName }
                    })
                })
                .ToListAsync();

            return Ok(list);
        }

        // ---------- GET: api/products/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var p = await _db.Products
                .Include(p => p.Category)
                .Include(p => p.Images)
                .Include(p => p.Characteristics)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (p is null) return NotFound();

            return Ok(new
            {
                p.Id,
                p.Article,
                p.Name,
                p.Price,
                p.MainImage,
                p.Color,
                p.Dimensions,
                p.Weight,
                p.Type,
                p.CountryOfOrigin,
                p.PackageContents,
                p.Warranty,
                p.Materials,
                p.Rating,
                Category   = new { p.Category!.Id, p.Category.Title },
                Images      = p.Images          .OrderBy(i => i.SortOrder).Select(i => i.ImageUrl),
                Characteristics = p.Characteristics.Select(c => new { c.Name, c.Value }),
                Comments    = p.Comments        .Select(c => new
                {
                    c.Id,
                    c.CommentText,
                    c.Rating,
                    User = new { c.User!.Id, c.User.FirstName, c.User.LastName }
                })
            });
        }

        // ---------- POST: api/products
        [HttpPost]
        public async Task<IActionResult> Create(ProductInput input)
        {
            var p = new Product
            {
                Article         = input.Article,
                CategoryId      = input.CategoryId,
                Name            = input.Name,
                Price           = input.Price,
                MainImage       = input.MainImage,
                Color           = input.Color,
                Dimensions      = input.Dimensions,
                Weight          = input.Weight,
                Type            = input.Type,
                CountryOfOrigin = input.CountryOfOrigin,
                PackageContents = input.PackageContents,
                Warranty        = input.Warranty,
                Materials       = input.Materials
            };

            _db.Products.Add(p);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = p.Id }, new { p.Id });
        }

        // ---------- PUT: api/products/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, ProductInput input)
        {
            var p = await _db.Products.FindAsync(id);
            if (p is null) return NotFound();

            p.Article         = input.Article;
            p.CategoryId      = input.CategoryId;
            p.Name            = input.Name;
            p.Price           = input.Price;
            p.MainImage       = input.MainImage;
            p.Color           = input.Color;
            p.Dimensions      = input.Dimensions;
            p.Weight          = input.Weight;
            p.Type            = input.Type;
            p.CountryOfOrigin = input.CountryOfOrigin;
            p.PackageContents = input.PackageContents;
            p.Warranty        = input.Warranty;
            p.Materials       = input.Materials;

            await _db.SaveChangesAsync();
            return Ok(new { p.Id });
        }

        // ---------- DELETE: api/products/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _db.Products.FindAsync(id);
            if (p is null) return NotFound();

            _db.Products.Remove(p);
            await _db.SaveChangesAsync();
            return Ok(new { p.Id });
        }
    }
}
