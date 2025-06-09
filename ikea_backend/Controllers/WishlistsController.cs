using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ikea_business.DTO;

namespace ikea_backend.Controllers;

[ApiController]
[Route("api/wishlists")]
public class WishlistsController : ControllerBase
{
    private const string WishlistSessionKey = "Wishlist";

    [HttpGet]
    public IActionResult GetWishlist()
    {
        var wishlistJson = HttpContext.Session.GetString(WishlistSessionKey);
        var wishlist = string.IsNullOrEmpty(wishlistJson)
            ? new List<WishlistInput>()
            : JsonSerializer.Deserialize<List<WishlistInput>>(wishlistJson);

        return Ok(wishlist);
    }

    [HttpPost("add")]
    public IActionResult AddToWishlist([FromBody] WishlistItemInput item)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return Unauthorized(new { message = "User not logged in" });

        var wishlistJson = HttpContext.Session.GetString(WishlistSessionKey);
        var wishlist = string.IsNullOrEmpty(wishlistJson)
            ? new List<WishlistInput>()
            : JsonSerializer.Deserialize<List<WishlistInput>>(wishlistJson)!;

        if (wishlist.Any(x => x.ProductId == item.ProductId))
        {
            return BadRequest(new { message = "Item already in wishlist" });
        }

        var newItem = new WishlistInput(
            UserId: userId.Value,
            ProductId: item.ProductId
        );

        wishlist.Add(newItem);

        HttpContext.Session.SetString(WishlistSessionKey, JsonSerializer.Serialize(wishlist));
        return Ok(new { message = "Item added to wishlist" });
    }

    [HttpPost("clear")]
    public IActionResult ClearWishlist()
    {
        HttpContext.Session.Remove(WishlistSessionKey);
        return Ok(new { message = "Wishlist cleared" });
    }
}