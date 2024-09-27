using Beatify.Database.Data;
using Beatify.Database.Entities;

namespace Beatify.Database.Repositories.Interfaces;

public interface IGroupRepository
{
    Task<List<Group>> GetAllAsync();
    Task<Group> GetByIdAsync(int id);
    Task AddAsync(Group group);
    Task RemoveAsync(int id);
    Task UpdateAsync(int id, Group group);
    Task<bool> ExistsByIdAsync(int id);
    Task<bool> ExistsByTitleAsync(string title);
}
