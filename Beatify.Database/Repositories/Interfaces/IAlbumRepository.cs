using Beatify.Database.Entities;

namespace Beatify.Database.Repositories.Interfaces;

public interface IAlbumRepository
{
    Task<List<Album>> GetAllAsync();
    Task<Album> GetByIdAsync(Guid id);
    Task AddAsync(Album album);
    Task RemoveAsync(Guid id);
    Task UpdateAsync(Guid id, Album album);
    Task<bool> ExistsByIdAsync(Guid id);
    Task<bool> ExistsByTitleAsync(string title);
}
