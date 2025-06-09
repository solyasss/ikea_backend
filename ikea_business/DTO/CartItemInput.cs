namespace ikea_business.DTO;

public record CartItemInput(
        int ProductId,
        int Quantity,
        bool IsCash,
        decimal TotalSum);
