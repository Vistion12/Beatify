using Beatify.Domain;
using Microsoft.EntityFrameworkCore;

namespace Beatify.Application.interfaces;

public interface IBeatifyDbContext
{
    DbSet<Group> Groups { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
