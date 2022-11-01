using Domain.Models;

namespace HttpsClients.ClientInterfaces;

public interface IAuthLogic
{
    Task<User> ValidateUser(string username, string password);
    Task RegisterUser(User user);
}