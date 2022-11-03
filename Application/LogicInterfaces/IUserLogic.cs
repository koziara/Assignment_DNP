using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> CreateAsync(UserCreationDto userToCreate);
    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
    public Task<User?> GetByNameAsync(String userName);
}