using System.Buffers;

namespace Domain.Models;

public class Post
{
    public int id { get; set; }
    public User Owner { get; }
    public string Title { get; }
    public string Body { get; }

    public Post(User owner, string title, string body)
    {
        Owner = owner;
        Title = title;
        Body = body;
    }
}