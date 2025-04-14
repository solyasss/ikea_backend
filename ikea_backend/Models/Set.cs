namespace ikea_backend.Models;

public class Set
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Slug { get; set; }
    public string? ImageUrl { get; set; }
}