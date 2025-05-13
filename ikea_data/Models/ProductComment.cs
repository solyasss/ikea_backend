namespace ikea_data.Models;

public class ProductComment
{
    public int     Id          { get; set; }
    public int     ProductId   { get; set; }
    public int     UserId      { get; set; }  
    public string  CommentText { get; set; } = "";
    public int?    Rating      { get; set; }

    public Product? Product { get; set; }
    public User?    User    { get; set; }   
}