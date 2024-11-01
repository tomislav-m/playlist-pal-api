using Application.DTOs;

namespace Application.Interfaces;

public interface ISpotifyArtistService
{
    Task<IEnumerable<TrackSimpleDto>> GetArtistTopTracks(string artistId, CancellationToken cancellationToken);
    Task<IEnumerable<ArtistDto>> GetArtistsByName(string name, CancellationToken cancellationToken);
    // Task<IEnumerable<AlbumDto>> GetArtistAlbums(string artistId, CancellationToken cancellationToken);
    Task<IEnumerable<ArtistDto>> GetRelatedArtists(string artistId, CancellationToken cancellationToken);
}
