using Domain.DTOs;
using Domain.Models;

namespace HttpsClients.ClientInterfaces;

public interface IUserService
{
    Task<User> Create(UserCreationDto dto);
    Task<IEnumerable<User>> GetUsersAsync();
    public Task<User> GetUserByNameAsync(string? usernameContains = null);
}