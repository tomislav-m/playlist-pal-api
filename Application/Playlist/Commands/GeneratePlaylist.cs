using Application.DTOs;
using Application.Interfaces;
using Domain.Helpers;
using MediatR;

namespace Application.Playlist.Commands;

public class GeneratePlaylist
{
    public record Command : IRequest<PlaylistDto>
    {
        public required string PlaylistName { get; init; }
        public bool IsPrivate { get; init; }
        public int Size { get; init; } = 20;
        public required GeneratePlaylistFilter Filter { get; init; }
    }

    public class Handler(
        ISpotifyPlaylistService spotifyPlaylistService,
        ISpotifyUserService spotifyUserService,
        ISpotifyTrackService spotifyTrackService,
        ISpotifyArtistService spotifyArtistService)
        : IRequestHandler<Command, PlaylistDto>
    {
        public async Task<PlaylistDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var tracks = await GetTracks(request.Filter, request.Size, cancellationToken);
            
            var playlist = await spotifyPlaylistService.CreatePlaylist(new CreatePlaylistRequestDto
            {
                PlaylistName = request.PlaylistName,
                IsPublic = !request.IsPrivate,
                Tracks = tracks
            }, cancellationToken);

            return playlist;
        }

        private async Task<IEnumerable<TrackSimpleDto>> GetTracks(GeneratePlaylistFilter? filter, int playlistSize, CancellationToken cancellationToken)
        {
            var playlistTracks = new HashSet<TrackSimpleDto>();
                
            var history = (await spotifyUserService.GetUserArtistsHistory(filter?.TimeRange, 50, cancellationToken)).ToList();
            
            var ids = history.Select(x => x.SpotifyId).Shuffle().ToList();

            var random = new Random();

            var artistTopTracks = new Dictionary<string, List<TrackSimpleDto>>();
            
            while (playlistTracks.Count < playlistSize)
            {
                var id = ids[random.Next(ids.Count - 1)];

                if (playlistTracks.Count >= playlistSize / 2)
                {
                    await GetRecommendedTracks(playlistTracks, playlistSize, cancellationToken);
                    break;
                }

                if (!artistTopTracks.TryGetValue(id, out var tracks))
                {
                    tracks = (await spotifyArtistService
                            .GetArtistTopTracks(id, cancellationToken))
                        .ToList();
                    artistTopTracks[id] = tracks;
                }

                playlistTracks.Add(tracks[random.Next(tracks.Count - 1)]);
            }

            return playlistTracks.Shuffle().Take(playlistSize);
        }

        private async Task GetRecommendedTracks(HashSet<TrackSimpleDto> playlistTracks, int playlistSize, CancellationToken cancellationToken)
        {
            var allTrackIds = playlistTracks.Select(x => x.Id).Shuffle().ToList();
            
            while (playlistTracks.Count < playlistSize)
            {
                foreach (var tracksIds in allTrackIds.Chunk(5))
                {
                    var recommendedTracks = await spotifyTrackService.GetRecommendations(tracksIds, limit: 100, cancellationToken: cancellationToken);
            
                    playlistTracks.UnionWith(recommendedTracks);

                    if (playlistTracks.Count >= playlistSize)
                    {
                        break;
                    }
                }
            }
        }
    }
}