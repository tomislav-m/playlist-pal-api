using Application.DTOs;

namespace Application.Interfaces;

public interface ISpotifyArtistService
{
    Task<IEnumerable<TrackSimpleDto>> GetArtistTopTracks(string artistId, CancellationToken cancellationToken);
    Task<IEnumerable<ArtistSimpleDto>> GetArtistsByName(string name, CancellationToken cancellationToken);
    Task<IEnumerable<ArtistSimpleDto>> GetRelatedArtists(string artistId, CancellationToken cancellationToken);
}
