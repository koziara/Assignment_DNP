using System.Buffers;

namespace Domain.Models;

public class Post
{
    public int id { get; set; }
    public User Owner { get; private set; }
    public string Title { get; private set; }
    public string Body { get; private set; }

    public Post(User owner, string title, string body)
    {
        Owner = owner;
        Title = title;
        Body = body;
    }

    private Post()
    {
    }
}