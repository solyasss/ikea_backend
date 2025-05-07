using ikea_backend.Data;
using ikea_business.DTO;
using ikea_data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ikea_backend.Controllers;

[ApiController]
[Route("api/new-arrivals")]
public class NewArrivalsController : ControllerBase
{
    private readonly IkeaDbContext _db;

    public NewArrivalsController(IkeaDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _db.NewArrivals.ToListAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var n = await _db.NewArrivals.FindAsync(id);
        return n != null ? Ok(n) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(NewArrivalInput input)
    {
        var n = new NewArrival { ImageUrl = input.ImageUrl, Text = input.Text };
        _db.NewArrivals.Add(n);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = n.Id }, n);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, NewArrivalInput input)
    {
        var n = await _db.NewArrivals.FindAsync(id);
        if (n is null) return NotFound();
        n.ImageUrl = input.ImageUrl;
        n.Text = input.Text;
        await _db.SaveChangesAsync();
        return Ok(n);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var n = await _db.NewArrivals.FindAsync(id);
        if (n is null) return NotFound();
        _db.NewArrivals.Remove(n);
        await _db.SaveChangesAsync();
        return Ok();
    }
}