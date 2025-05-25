namespace ikea_business.DTO;

public record OrderInput(
    string   OrderNumber,
    int      UserId,
    int      ProductId,
    DateTime OrderDate,
    DateTime ReceiveDate,
    bool     IsCash,
    decimal  TotalSum);