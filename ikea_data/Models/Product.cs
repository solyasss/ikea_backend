namespace ikea_data.Models;

public class Product
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = "";
    public string Slug { get; set; } = "";
    public decimal Price { get; set; }
    public string? MainImage { get; set; }
    public Category? Category { get; set; }
}
