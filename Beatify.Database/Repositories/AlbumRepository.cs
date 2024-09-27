using Beatify.Database.Data;
using Beatify.Database.Entities;
using Beatify.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Beatify.Database.Repositories;

public class AlbumRepository(BeatifyDataBaseContext beatifyDataBaseContext) : IAlbumRepository
{
    public async Task<List<Album>> GetAllAsync() =>
        await beatifyDataBaseContext.Albums.ToListAsync();

    public async Task<Album> GetByIdAsync(Guid id) =>
        await beatifyDataBaseContext.Albums.SingleAsync(album => album.Id == id);

    public async Task AddAsync(Album album)
    {
        await beatifyDataBaseContext.AddAsync(album);
        await beatifyDataBaseContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(Guid id) =>
        await beatifyDataBaseContext.Albums
            .Where(album => album.Id == id)
            .ExecuteDeleteAsync();

    public async Task UpdateAsync(Guid id, Album album) =>
        await beatifyDataBaseContext.Albums
            .Where(album => album.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(album => album.Title, album.Title)
                .SetProperty(album => album.Description, album.Description)
                .SetProperty(album => album.ReleaseDate, album.ReleaseDate)
                .SetProperty(album => album.Groups, album.Groups)
                .SetProperty(album => album.Songs, album.Songs));

    public async Task<bool> ExistsByIdAsync(Guid id) =>
        await beatifyDataBaseContext.Albums.AnyAsync(album => album.Id == id);

    public async Task<bool> ExistsByTitleAsync(string title) =>
        await beatifyDataBaseContext.Albums.AnyAsync(album => album.Title == title);
}

