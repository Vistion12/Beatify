using Beatify.Database.Data;
using Beatify.Database.Entities;
using Beatify.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Beatify.Database.Repositories;

public class GenreRepository(BeatifyDataBaseContext beatifyDataBaseContext):IGenreRepository
{
    
    public async Task<List<Genre>> GetAllAsync() =>
        await beatifyDataBaseContext.Genres.ToListAsync();
    public async Task<Genre> GetByIdAsync(int id) =>
        await beatifyDataBaseContext.Genres.SingleAsync(genre => genre.Id == id);

    public async Task AddAsync(Genre genre)
    {
        await beatifyDataBaseContext.AddAsync(genre);
        await beatifyDataBaseContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id) =>
        await beatifyDataBaseContext.Genres
            .Where(genre => genre.Id == id)
            .ExecuteDeleteAsync();

    public async Task UpdateAsync(int id, Genre genre) => await beatifyDataBaseContext.Genres
            .Where(genre => genre.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(genre => genre.Title, genre.Title)
                .SetProperty(genre => genre.Groups, genre.Groups));

    public async Task<bool> ExistsByIdAsync(int id) =>
        await beatifyDataBaseContext.Genres.AnyAsync(genre => genre.Id == id);

    public async Task<bool> ExistsByTitleAsync(string title) =>
        await beatifyDataBaseContext.Genres.AnyAsync(genre => genre.Title == title);

}
