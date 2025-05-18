namespace ikea_business.DTO
{
    public record ProductInput(
        string   Article,
        int      CategoryId,
        string   Name,
        decimal  Price,
        string?  MainImage,
        string?  Description, 
        string?  Color,
        string?  Dimensions,
        decimal? Weight,
        string?  Type,
        string?  CountryOfOrigin,
        string?  PackageContents,
        string?  Warranty,
        string?  Materials,
        decimal? Rating 
    );
}