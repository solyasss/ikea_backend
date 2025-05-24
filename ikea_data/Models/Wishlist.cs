using System.ComponentModel.DataAnnotations.Schema;

namespace ikea_data.Models;

public class Wishlist
{
    public int Id { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    [ForeignKey(nameof(Product))]
    public int ProductId { get; set; }

    public User?    User    { get; set; }
    public Product? Product { get; set; }
}