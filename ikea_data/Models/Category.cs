namespace ikea_data.Models;

public class Category
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string Title { get; set; } = "";
    public string Slug { get; set; } = "";
    public Category? Parent { get; set; }
    public List<Category> Children { get; set; } = new();
}
