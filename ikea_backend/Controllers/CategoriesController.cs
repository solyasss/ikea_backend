using ikea_data.Data;
using ikea_data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ikea_backend.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly IkeaDbContext _db;

    public CategoriesController(IkeaDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _db.Categories
            .Include(c => c.Children)
            .Where(c => c.ParentId == null)
            .Select(c => new
            {
                c.Id, c.ParentId, c.Title, c.Slug,
                Children = c.Children.Select(sc => new { sc.Id, sc.ParentId, sc.Title, sc.Slug })
            })
            .ToListAsync();

        return Ok(list);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var cat = await _db.Categories.Include(c => c.Children).FirstOrDefaultAsync(c => c.Id == id);
        if (cat == null) return NotFound();
        return Ok(new
        {
            cat.Id, cat.ParentId, cat.Title, cat.Slug,
            Children = cat.Children.Select(sc => new { sc.Id, sc.ParentId, sc.Title, sc.Slug })
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(Category input)
    {
        _db.Categories.Add(input);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = input.Id }, input);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Category input)
    {
        var cat = await _db.Categories.FindAsync(id);
        if (cat == null) return NotFound();
        cat.Title = input.Title;
        cat.Slug = input.Slug;
        cat.ParentId = input.ParentId;
        await _db.SaveChangesAsync();
        return Ok(cat);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var cat = await _db.Categories.Include(c => c.Children).FirstOrDefaultAsync(c => c.Id == id);
        if (cat == null) return NotFound();
        _db.Categories.Remove(cat);
        await _db.SaveChangesAsync();
        return Ok(cat);
    }
}
