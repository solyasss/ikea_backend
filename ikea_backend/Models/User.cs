namespace ikea_backend.Models;

public class User
{
    public int Id            { get; set; }
    public bool IsAdmin { get; set; } = false;
    public string FirstName  { get; set; } = "";
    public string LastName   { get; set; } = "";
    public DateTime BirthDate { get; set; }
    public string Country    { get; set; } = "";
    public string Address    { get; set; } = "";
    public string Phone      { get; set; } = "";
    public string Email      { get; set; } = "";
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
}