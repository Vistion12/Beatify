using Beatify.Database.Entities;

namespace Beatify.Database.Repositories.Interfaces;

public interface ISongRepository
{
    Task<List<Song>> GetAllAsync();
    Task<Song> GetByIdAsync(int id);
    Task AddAsync(Song song);
    Task RemoveAsync(int id);
    Task UpdateAsync(int id, Song song);
    Task<bool> ExistsByIdAsync(int id);
    Task<bool> ExistsByTitleAsync(string title);
}
