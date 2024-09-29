namespace Beatify.Domain;

public class Group
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string UrlImage { get; set; }
    public string? Description { get; set; }
    public required DateOnly FoundationDate { get; set; }
    public required List<Genre> Genres { get; set; }
    public required List<Album> Albums { get; set; }
}
