namespace Infrastructure.Spotify;

public record SpotifyOptions
{
    public required string ClientId { get; init; }
    public required string ClientSecret { get; init; }
    public required string RedirectUri { get; init; }
}