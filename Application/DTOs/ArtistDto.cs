namespace Application.DTOs;

public record ArtistDto
{
    public string? Id { get; set; }
    public required string Name { get; set; }
    public required string SpotifyId { get; set; }
    public int? Popularity { get; set; }
    public IEnumerable<string> Genres { get; set; } = default!;
    // public IEnumerable<ImageDto> Images { get; set; }
    // public IEnumerable<AlbumDto> Albums { get; set; }
    public IEnumerable<ArtistDto> SimilarArtists { get; set; } = new List<ArtistDto>();
}