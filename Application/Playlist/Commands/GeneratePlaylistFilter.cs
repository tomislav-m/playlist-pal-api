namespace Application.Playlist.Commands;

public record GeneratePlaylistFilter
{
    public string? TimeRange { get; set; }
    public IEnumerable<string> Genres { get; init; } = [];
    public IEnumerable<string> Artists { get; init; } = [];
}