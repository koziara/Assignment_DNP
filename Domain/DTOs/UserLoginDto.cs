using System.Threading.Tasks.Dataflow;

namespace Domain.DTOs;

public class UserLoginDto
{
    public string Username { get; init; }
    public string Password { get; init; }
    
}