namespace Beatify.Database.Entities;

public class Album
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; } 
    public required DateOnly ReleaseDate { get; set; }
    public required List<Group> Groups { get; set; }
    public required List<Song> Songs { get; set; }
} 
