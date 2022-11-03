using Domain.DTOs;
using Domain.Models;

namespace HttpsClients.ClientInterfaces;

public interface IPostService
{
    Task CreateAsync(PostCreationDto dto);
    Task<ICollection<Post>> GetAsync();
    Task<Post> GetByIdAsync(int id);
}