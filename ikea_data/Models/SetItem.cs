namespace ikea_data.Models;

public class SetItem
{
    public int Id { get; set; }
    public int SetId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public Set? Set { get; set; }
    public Product? Product { get; set; }
}