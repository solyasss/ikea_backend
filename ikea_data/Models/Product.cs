namespace ikea_data.Models;

public class Product
{
    public int Id { get; set; }
    public string  Article          { get; set; } = "";
    public int     CategoryId       { get; set; }
    public string  Name             { get; set; } = "";
    public decimal Price            { get; set; }
    public string? MainImage        { get; set; }
    
    public string? Color            { get; set; }
    public string? Dimensions       { get; set; }  
    public decimal? Weight          { get; set; }   
    public string? Type             { get; set; }
    public string? CountryOfOrigin  { get; set; }
    public string? PackageContents  { get; set; }
    public string? Warranty         { get; set; }
    public string? Materials        { get; set; }
    public decimal? Rating          { get; set; } 
 
    public Category? Category { get; set; }

    public List<ProductImage>          Images          { get; set; } = new();
    public List<ProductCharacteristic> Characteristics { get; set; } = new();
    public List<ProductComment>        Comments        { get; set; } = new();
}

