using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ikea_data.Models;

public class Order
{
    public int Id { get; set; }

    [MaxLength(32)]
    public string OrderNumber { get; set; } = string.Empty;

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    [ForeignKey(nameof(Product))]
    public int ProductId { get; set; }

    public DateTime OrderDate   { get; set; }
    public DateTime ReceiveDate { get; set; }

    public bool    IsCash   { get; set; }
    public decimal TotalSum { get; set; }

    public User?    User    { get; set; }
    public Product? Product { get; set; }
}