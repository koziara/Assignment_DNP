namespace Domain.Models;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public List<Post> Posts { get; set; }
    public int Age { get; set; }
}