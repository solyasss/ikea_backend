namespace ikea_backend.Models;

public class ProductImage
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ImageUrl { get; set; } = "";
    public int SortOrder { get; set; }
    public Product? Product { get; set; }
}