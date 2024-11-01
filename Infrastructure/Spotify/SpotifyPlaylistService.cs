using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using SpotifyAPI.Web;

namespace Infrastructure.Spotify;

public class SpotifyPlaylistService(IHttpContextAccessor contextAccessor, IMapper mapper)
    : SpotifyBaseService(contextAccessor, mapper), ISpotifyPlaylistService
{
    public async Task<PlaylistDto> CreatePlaylist(CreatePlaylistRequestDto request, CancellationToken cancellationToken)
    {
        var user = await GetCurrentUser(cancellationToken);
        var playlist = await SpotifyClient.Playlists.Create(user.Id, new PlaylistCreateRequest(request.PlaylistName)
        {
            Public = request.IsPublic,
        }, cancellationToken);

        if (request.Tracks.Any())
        {
            foreach (var tracksChunk in request.Tracks.Chunk(100))
            {
                await AddToPlaylist(playlist.Id!, tracksChunk.Select(x => x.Uri), cancellationToken);
            }
        }

        var response = new PlaylistDto
        {
            Id = playlist.Id!,
            Name = request.PlaylistName,
            Tracks = request.Tracks
        };

        return response;
    }

    public Task AddToPlaylist(string playlistId, IEnumerable<string> trackUris, CancellationToken cancellationToken)
    {
        return SpotifyClient.Playlists.AddItems(playlistId, new PlaylistAddItemsRequest(trackUris.ToList()), cancellationToken);
    }
}
