using Application.DTOs;

namespace Application.Interfaces;

public interface ISpotifyPlaylistService
{
    Task<PlaylistDto> CreatePlaylist(CreatePlaylistRequestDto request, CancellationToken cancellationToken);
    Task AddToPlaylist(string playlistId, IEnumerable<string> trackUri, CancellationToken cancellationToken);
}
