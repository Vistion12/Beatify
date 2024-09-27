using Beatify.Database.Entities;

namespace Beatify.Database.Repositories.Interfaces;

public interface IGenreRepository
{
    Task<List<Genre>> GetAllAsync();
    Task<Genre> GetByIdAsync(int id);
    Task AddAsync(Genre genre);
    Task RemoveAsync(int id);
    Task UpdateAsync(int id, Genre genre);
    Task<bool> ExistsByIdAsync(int id);
    Task<bool> ExistsByTitleAsync(string title);

}
