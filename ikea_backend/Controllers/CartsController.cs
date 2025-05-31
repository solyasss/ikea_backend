using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ikea_business.DTO;

namespace ikea_backend.Controllers;

[ApiController]
[Route("api/carts")]
public class CartsController : ControllerBase
{
    private const string CartSessionKey = "Cart";

    [HttpGet]
    public IActionResult GetCart()
    {
        var cartJson = HttpContext.Session.GetString(CartSessionKey);
        var cart = string.IsNullOrEmpty(cartJson)
            ? new List<CartInput>()
            : JsonSerializer.Deserialize<List<CartInput>>(cartJson);

        return Ok(cart);
    }

    [HttpPost("add")]
    public IActionResult AddToCart([FromBody] CartInput item)
    {
        var cartJson = HttpContext.Session.GetString(CartSessionKey);
        var cart = string.IsNullOrEmpty(cartJson)
            ? new List<CartInput>()
            : JsonSerializer.Deserialize<List<CartInput>>(cartJson);

        cart!.Add(item);

        HttpContext.Session.SetString(CartSessionKey, JsonSerializer.Serialize(cart));
        return Ok(new { message = "Item added to cart" });
    }

    [HttpPost("clear")]
    public IActionResult ClearCart()
    {
        HttpContext.Session.Remove(CartSessionKey);
        return Ok(new { message = "Cart cleared" });
    }
}