using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ikea_data.Models;

public class UserCard
{
    public int Id        { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId    { get; set; }

    [MaxLength(16)]
    public string CardNumber { get; set; } = "";

    public int  ValidDay  { get; set; }   
    public int  ValidYear { get; set; } 

    [MaxLength(32)]
    public string CardType { get; set; } = "";

    public byte[] CvvHash { get; set; } = []; 

    public User? User { get; set; }
}