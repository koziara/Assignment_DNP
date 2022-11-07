using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao postDao;
    private readonly IUserDao userDao;

    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        this.postDao = postDao;
        this.userDao = userDao;
    }

    public async Task<Post> CreateAsync(PostCreationDto dto)
    {
        try
        {
            User? user = await userDao.GetByUsernameAsync(dto.OwnerName);
        if (user == null)
        {
            throw new Exception($"User with id {dto.OwnerName} was not found.");
        }
ValidateTodo(dto);
        Post post = new Post(user, dto.Title, dto.Body);
        Post created = await postDao.CreateAsync(post);
        return created;
        }
        catch (Exception e)
        {
            Console.WriteLine(e + "nie ma owner");
            throw;
        }
        
     
        
    }

    public async Task<IEnumerable<Post>> GetAsync()
    {
        IEnumerable<Post> posts = await postDao.GetAsync();
        return posts;
    }

    public async Task DeleteAsync(int id)
    {
        Post? post = await postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with id {id} was not found");
        }
        await postDao.DeleteAsync(id);
        
    }

    public async Task<Post> GetByIdAsync(int id)
    {
        Post? post = await postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with id {id} was not found");
        }

        return post;
    }

    private void ValidateTodo(PostCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");

    }
}