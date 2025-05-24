using ikea_business.DTO;
using ikea_business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ikea_backend.Controllers;

[ApiController]
[Route("api/wishlists")]
public class WishlistsController : ControllerBase
{
    private readonly IWishlistService _svc;
    public WishlistsController(IWishlistService svc) => _svc = svc;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _svc.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id) =>
        await _svc.GetAsync(id) is { } w ? Ok(w) : NotFound();

    [HttpPost]
    public async Task<IActionResult> Create(WishlistInput dto)
    {
        var id = await _svc.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id }, new { id });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        await _svc.DeleteAsync(id) ? Ok(new { id }) : NotFound();
}