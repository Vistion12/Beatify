using Beatify.Database.Data;
using Beatify.Database.Entities;
using Beatify.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Beatify.Database.Repositories;

public class SongRepository(BeatifyDataBaseContext beatifyDataBaseContext) : ISongRepository
{    
    public async Task<List<Song>> GetAllAsync() =>
        await beatifyDataBaseContext.Songs.ToListAsync();

    public async Task<Song> GetByIdAsync(int id) =>
        await beatifyDataBaseContext.Songs.SingleAsync(song => song.Id == id);

    public async Task AddAsync(Song song)
    {
        await beatifyDataBaseContext.AddAsync(song);
        await beatifyDataBaseContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id) =>
        await beatifyDataBaseContext.Songs
            .Where(song => song.Id == id)
            .ExecuteDeleteAsync();

    public async Task UpdateAsync(int id, Song song) => await beatifyDataBaseContext.Songs
            .Where(song => song.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(song => song.Title, song.Title)
                .SetProperty(song => song.Description, song.Description)
                .SetProperty(song => song.Albums, song.Albums));

    public async Task<bool> ExistsByIdAsync(int id) =>
        await beatifyDataBaseContext.Songs.AnyAsync(song => song.Id == id);

    public async Task<bool> ExistsByTitleAsync(string title) =>
        await beatifyDataBaseContext.Songs.AnyAsync(song => song.Title == title);
}
