namespace Beatify.Database.Entities;

public class Song
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required List<Album> Albums { get; set; }
}
