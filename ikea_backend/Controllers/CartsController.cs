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
    public IActionResult AddToCart([FromBody] CartItemInput item)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return Unauthorized(new { message = "User not logged in" });

        var cartJson = HttpContext.Session.GetString(CartSessionKey);
        var cart = string.IsNullOrEmpty(cartJson)
            ? new List<CartInput>()
            : JsonSerializer.Deserialize<List<CartInput>>(cartJson)!;

        var existingItem = cart.FirstOrDefault(x => x.ProductId == item.ProductId);
        if (existingItem != null)
        {
            existingItem = existingItem with
            {
                Quantity = existingItem.Quantity + item.Quantity,
                TotalSum = existingItem.TotalSum + item.TotalSum
            };

            cart.RemoveAll(x => x.ProductId == item.ProductId);
            cart.Add(existingItem);
        }
        else
        {
            var newItem = new CartInput(
                UserId: userId.Value,
                ProductId: item.ProductId,
                Quantity: item.Quantity,
                IsCash: item.IsCash,
                TotalSum: item.TotalSum
            );

            cart.Add(newItem);
        }

        HttpContext.Session.SetString(CartSessionKey, JsonSerializer.Serialize(cart));
        return Ok(new { message = "Item added to cart" });
    }

    [HttpPost("clear")]
    public IActionResult ClearCart()
    {
        HttpContext.Session.Remove(CartSessionKey);
        return Ok(new { message = "Cart cleared" });
    }

    [HttpDelete("remove/{productId}")]
    public IActionResult RemoveFromCart(int productId)
    {
        var cartJson = HttpContext.Session.GetString(CartSessionKey);
        var cart = string.IsNullOrEmpty(cartJson)
            ? new List<CartInput>()
            : JsonSerializer.Deserialize<List<CartInput>>(cartJson)!;

        cart = cart.Where(item => item.ProductId != productId).ToList();

        HttpContext.Session.SetString(CartSessionKey, JsonSerializer.Serialize(cart));
        return Ok(new { message = "Item removed from cart" });
    }

}