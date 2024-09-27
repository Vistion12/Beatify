using Beatify.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Beatify.Database.Data;

public class BeatifyDataBaseContext(DbContextOptions<BeatifyDataBaseContext> options) : DbContext(options)
{
    public DbSet<Album> Albums { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Song> Songs { get; set; }
}
