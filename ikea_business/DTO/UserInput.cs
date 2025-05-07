namespace ikea_business.DTO;

public record UserInput(
    bool     IsAdmin,
    string   FirstName,
    string   LastName,
    DateTime BirthDate,
    string   Country,
    string   Address,
    string   Phone,
    string   Email,
    string   Password);