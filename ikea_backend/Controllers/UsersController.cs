using ikea_business.DTO;
using ikea_business.Helpers;
using ikea_data.Data;
using ikea_data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ikea_backend.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IkeaDbContext _db;

    public UsersController(IkeaDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _db.Users
            .Select(u => new
            {
                u.Id, u.IsAdmin, u.FirstName, u.LastName, u.BirthDate,
                u.Country, u.Address, u.Phone, u.Email
            })
            .ToListAsync();

        return Ok(list);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var u = await _db.Users.FindAsync(id);
        if (u == null) return NotFound();
        return Ok(new
        {
            u.Id, u.IsAdmin, u.FirstName, u.LastName, u.BirthDate,
            u.Country, u.Address, u.Phone, u.Email
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserInput input)
    {
        PasswordHasher.CreateHash(input.Password, out var hash, out var salt);

        var u = new User
        {
            IsAdmin = input.IsAdmin,
            FirstName = input.FirstName,
            LastName = input.LastName,
            BirthDate = input.BirthDate,
            Country = input.Country,
            Address = input.Address,
            Phone = input.Phone,
            Email = input.Email,
            PasswordHash = hash,
            PasswordSalt = salt
        };

        _db.Users.Add(u);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = u.Id }, new { u.Id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UserInput input)
    {
        var u = await _db.Users.FindAsync(id);
        if (u == null) return NotFound();

        u.IsAdmin = input.IsAdmin;
        u.FirstName = input.FirstName;
        u.LastName = input.LastName;
        u.BirthDate = input.BirthDate;
        u.Country = input.Country;
        u.Address = input.Address;
        u.Phone = input.Phone;
        u.Email = input.Email;

        PasswordHasher.CreateHash(input.Password, out var hash, out var salt);
        u.PasswordHash = hash;
        u.PasswordSalt = salt;

        await _db.SaveChangesAsync();
        return Ok(new { u.Id });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var u = await _db.Users.FindAsync(id);
        if (u == null) return NotFound();
        _db.Users.Remove(u);
        await _db.SaveChangesAsync();
        return Ok(new { u.Id });
    }
}
