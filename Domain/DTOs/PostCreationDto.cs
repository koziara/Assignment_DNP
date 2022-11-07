namespace Domain.DTOs;

public class PostCreationDto
{
    public string OwnerName { get; }
    public string Title { get; }
    public string Body { get; }
    
    public PostCreationDto(string ownerName, string title, string body)
    {
        OwnerName = ownerName;
        Title = title;
        Body = body;
    }
}