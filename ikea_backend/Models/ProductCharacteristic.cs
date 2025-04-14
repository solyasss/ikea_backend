namespace ikea_backend.Models;

public class ProductCharacteristic
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string Name { get; set; } = "";
    public string Value { get; set; } = "";
    public Product? Product { get; set; }
}
