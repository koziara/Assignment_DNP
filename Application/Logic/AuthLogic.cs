
using Application.DaoInterfaces;
using Domain.Models;

namespace WebAPI.Services;

public class AuthLogic : IAuthLogic
{
    private readonly IUserDao userDao;

    public AuthLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    public async Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = await userDao.GetByUsernameAsync(username);

        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return existingUser;
    }

    public Task RegisterUser(User user)
    {
        if (string.IsNullOrEmpty(user.UserName))
        {
            throw new Exception("Username cannot be null");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new Exception("Password cannot be null");
        }

        userDao.CreateAsync(user);

        return Task.CompletedTask;
    }
}
