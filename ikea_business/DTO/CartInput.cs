namespace ikea_business.DTO;

public record CartInput(
    int     UserId,
    int     ProductId,
    int     Quantity,
    bool    IsCash,
    decimal TotalSum);