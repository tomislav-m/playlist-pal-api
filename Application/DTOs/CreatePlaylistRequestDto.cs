namespace Application.DTOs;

public record CreatePlaylistRequestDto
{
    public required string PlaylistName { get; set; }
    public bool IsPublic { get; set; }
    public required ICollection<TrackSimpleDto> Tracks { get; set; }
}