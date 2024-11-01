namespace Application.DTOs;

public record TrackSimpleDto
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public required string ArtistName { get; set; }
    public required string Uri { get; set; }
    public required string AlbumTitle { get; set; }
}
