using Beatify.Database.Data;
using Beatify.Database.Entities;
using Beatify.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Beatify.Database.Repositories;

public class GroupRepository(BeatifyDataBaseContext beatifyDataBaseContext): IGroupRepository
{
    public async Task<List<Group>> GetAllAsync() =>
        await beatifyDataBaseContext.Groups.ToListAsync();
    public async Task<Group> GetByIdAsync(int id) =>
        await beatifyDataBaseContext.Groups.SingleAsync(groups => groups.Id == id);
    public async Task AddAsync(Group group)
    {
        await beatifyDataBaseContext.AddAsync(group);
        await beatifyDataBaseContext.SaveChangesAsync();
    }
    public async Task RemoveAsync(int id)=>
         await beatifyDataBaseContext.Groups
            .Where(group => group.Id == id)
            .ExecuteDeleteAsync();
    public async Task UpdateAsync(int id, Group group)=> await beatifyDataBaseContext.Groups
            .Where(group => group.Id == id)
            .ExecuteUpdateAsync(s => s
                     .SetProperty(group => group.Title, group.Title)
                     .SetProperty(group => group.UrlImage, group.UrlImage)
                     .SetProperty(group => group.Description, group.Description)
                     .SetProperty(group => group.FoundationDate, group.FoundationDate)
                     .SetProperty(group => group.Genres, group.Genres)
                     .SetProperty(group => group.Albums, group.Albums));

    public async Task <bool> ExistsByIdAsync(int id)=>
        await beatifyDataBaseContext.Groups.AnyAsync(group=> group.Id == id);

    public async Task<bool> ExistsByTitleAsync(string title)=>
        await beatifyDataBaseContext.Groups.AnyAsync(group => group.Title == title);
    
}
