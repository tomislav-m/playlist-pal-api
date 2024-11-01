namespace Application.DTOs;

public class TrackDto
{
    public required string Id { get; init; }

    public required string Title { get; init; }

    public required int DurationMs { get; init; }

    public required int? TrackNumber { get; init; }

    public required int? DiscNumber { get; init; }

    public required string SpotifyId { get; init; }

    public required int Popularity { get; init; }

    public required Guid AlbumId { get; init; }
}