namespace ikea_business.DTO;

public record UserCardInput(
    int    UserId,
    string CardNumber,
    int    ValidDay,
    int    ValidYear,
    string CardType,
    string Cvv);