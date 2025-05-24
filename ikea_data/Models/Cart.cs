using System.ComponentModel.DataAnnotations.Schema;

namespace ikea_data.Models;

public class Cart
{
    public int Id { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    [ForeignKey(nameof(Product))]
    public int ProductId { get; set; }

    public int     Quantity     { get; set; }          
    public bool    IsCash       { get; set; }          
    public decimal TotalSum     { get; set; } 

    public User?    User    { get; set; }
    public Product? Product { get; set; }
}