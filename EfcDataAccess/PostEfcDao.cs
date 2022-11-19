using System.Runtime.CompilerServices;
using Application.DaoInterfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess;

public class PostEfcDao : IPostDao
{
    private readonly PostContext context;

    public PostEfcDao(PostContext context)
    {
        this.context = context;
    }

    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> newPost = await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();
        return newPost.Entity;
    }

    public async Task<IEnumerable<Post>> GetAsync()
    {
        IQueryable<Post> postsQuery = context.Posts.Include(post => post.Owner).AsQueryable();
        List<Post> result = await postsQuery.ToListAsync();
        return result;
    }

    public async Task DeleteAsync(int id)
    {
        Post? post = await GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with id {id} not found");
        }

        context.Posts.Remove(post);
        await context.SaveChangesAsync();

    }

    public async Task<Post> GetByIdAsync(int id)
    {
        Post? post = await context.Posts.AsNoTracking().Include(post => post.Owner).SingleOrDefaultAsync(post => post.id == id);
        return post;
    }
}