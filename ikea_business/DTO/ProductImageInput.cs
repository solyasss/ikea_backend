namespace ikea_business.DTO
{
    public record ProductImageInput(
       int ProductId,
       string ImageUrl,
       int SortOrder
   );
}
