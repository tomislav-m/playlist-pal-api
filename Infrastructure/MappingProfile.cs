using Application.DTOs;
using AutoMapper;
using SpotifyAPI.Web;

namespace Infrastructure;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<FullArtist, ArtistDto>()
            .ForMember(d => d.SpotifyId, o => o.MapFrom(s => s.Id));
        CreateMap<FullPlaylist, PlaylistDto>()
            .ForMember(d => d.Tracks, o => o.MapFrom(s => s.Tracks == null || s.Tracks.Items == null ? null : s.Tracks.Items.Select(x => x.Track)));
        CreateMap<FullTrack, TrackSimpleDto>()
            .ForMember(d => d.ArtistName, o => o.MapFrom(s => s.Artists.First().Name))
            .ForMember(d => d.AlbumTitle, o => o.MapFrom(s => s.Album.Name))
            .ForMember(d => d.Title, o => o.MapFrom(s => s.Name));
        // CreateMap<Image, ImageDto>();
        //
        // CreateMap<SimpleAlbum, AlbumDto>()
        //     .ForMember(d => d.SpotifyId, o => o.MapFrom(s => s.Id))
        //     .ForMember(d => d.Id, o => o.Ignore());
        // CreateMap<FullAlbum, SaveAlbumTracksRequest>()
        //     .ForMember(d => d.Tracks, o => o.MapFrom(s => s.Tracks.Items));
        // CreateMap<FullAlbum, AlbumDto>()
        //     .ForMember(d => d.Id, o => o.Ignore())
        //     .ForMember(d => d.SpotifyId, o => o.MapFrom(s => s.Id));
        
        CreateMap<SimpleArtist, ArtistDto>()
            .ForMember(d => d.SpotifyId, o => o.MapFrom(s => s.Id));

        CreateMap<SimpleTrack, TrackSimpleDto>()
            .ForMember(d => d.ArtistName, o => o.MapFrom(s => s.Artists.First().Name))
            .ForMember(d => d.Title, o => o.MapFrom(s => s.Name));

        // CreateMap<SimpleTrack, TrackDto>()
        //     .ForMember(d => d.SpotifyId, o => o.MapFrom(s => s.Id))
        //     .ForMember(d => d.Title, o => o.MapFrom(s => s.Name));
        //
        // CreateMap<FullTrack, TrackDto>()
        //     .ForMember(d => d.SpotifyId, o => o.MapFrom(s => s.Id))
        //     .ForMember(d => d.Title, o => o.MapFrom(s => s.Name))
        //     .ForMember(d => d.AlbumId, o => o.Ignore());
    }
}