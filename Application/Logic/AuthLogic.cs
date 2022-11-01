using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using Application.DaoInterfaces;
using Domain.Models;
using HttpsClients.ClientInterfaces;

namespace Application.Logic;

public class AuthLogic : IAuthLogic
{
    private readonly IUserDao userDao;

    public AuthLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    public async Task<User> ValidateUser(string username, string password)
    {
        try
        {
            User? existingUser = await userDao.GetByUsernameAsync(username);
            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            if (!existingUser.Password.Equals(password))
            {
                throw new Exception("Password incorrect");
            }

            return existingUser;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("JEJ");
        }
        
        
    }
    
    public Task<User> GetUser(string username, string password)
    {
        throw new NotImplementedException();
    }
    
    public async Task RegisterUser(User user)
    {
        if (string.IsNullOrEmpty(user.UserName))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ValidationException("Password cannot be null");
        }

        await userDao.CreateAsync(user);
    }
}