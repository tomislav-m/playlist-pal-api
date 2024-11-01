namespace Application.DTOs;

public record ArtistSimpleDto
{
    public required string Name { get; init; }
    public required string SpotifyId { get; init; }

    public required string ImageUrl { get; init; }
}