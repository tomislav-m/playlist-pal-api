using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using SpotifyAPI.Web;

namespace Infrastructure.Spotify;

public class SpotifyArtistService(IHttpContextAccessor contextAccessor, IMapper mapper)
    : SpotifyBaseService(contextAccessor, mapper), ISpotifyArtistService
{
    public async Task<IEnumerable<ArtistSimpleDto>> GetArtistsByName(string name, CancellationToken cancellationToken)
    {
        var response = await SpotifyClient.Search.Item(new SearchRequest(SearchRequest.Types.Artist, name) { Market = MarketUS }, cancellationToken);
        return response.Artists.Items is not null
            ? Mapper.ProjectTo<ArtistSimpleDto>(response.Artists.Items.AsQueryable())
            : Array.Empty<ArtistSimpleDto>();
    }

    public async Task<IEnumerable<TrackSimpleDto>> GetArtistTopTracks(string artistId, CancellationToken cancellationToken)
    {
        var response = await SpotifyClient.Artists.GetTopTracks(artistId, new ArtistsTopTracksRequest(MarketUS), cancellationToken);
        return Mapper.ProjectTo<TrackSimpleDto>(response.Tracks.AsQueryable());
    }

    // public async Task<IEnumerable<AlbumDto>> GetArtistAlbums(string artistId, CancellationToken cancellationToken)
    // {
    //     var response = await SpotifyClient.Artists.GetAlbums(artistId, new ArtistsAlbumsRequest { Limit = 50, IncludeGroupsParam = ArtistsAlbumsRequest.IncludeGroups.Album }, cancellationToken);
    //
    //     return response.Items is not null
    //         ? Mapper.ProjectTo<AlbumDto>(response.Items.AsQueryable())
    //         : Array.Empty<AlbumDto>();
    // }

    public async Task<IEnumerable<ArtistSimpleDto>> GetRelatedArtists(string artistId, CancellationToken cancellationToken)
    {
        var response = await SpotifyClient.Artists.GetRelatedArtists(artistId, cancellationToken);

        return Mapper.ProjectTo<ArtistSimpleDto>(response.Artists.AsQueryable());
    }
}
