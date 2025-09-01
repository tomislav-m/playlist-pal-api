using Application.DTOs;
using AutoMapper;
using SpotifyAPI.Web;

namespace Infrastructure;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<FullArtist, ArtistSimpleDto>()
            .ForMember(d => d.SpotifyId, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.Images.Count > 0 ? s.Images.MinBy(x => x.Height)!.Url : string.Empty));

        CreateMap<FullPlaylist, PlaylistDto>()
            .ForMember(d => d.Tracks, o => o.MapFrom(s => s.Tracks == null || s.Tracks.Items == null ? null : s.Tracks.Items.Select(x => x.Track)));
        CreateMap<FullTrack, TrackSimpleDto>()
            .ForMember(d => d.ArtistName, o => o.MapFrom(s => s.Artists.First().Name))
            .ForMember(d => d.AlbumTitle, o => o.MapFrom(s => s.Album.Name))
            .ForMember(d => d.AlbumCover, o => o.MapFrom(s => s.Album.Images[0].Url))
            .ForMember(d => d.Title, o => o.MapFrom(s => s.Name));
        
        CreateMap<SimpleArtist, ArtistSimpleDto>()
            .ForMember(d => d.SpotifyId, o => o.MapFrom(s => s.Id));

        CreateMap<SimpleTrack, TrackSimpleDto>()
            .ForMember(d => d.ArtistName, o => o.MapFrom(s => s.Artists.First().Name))
            .ForMember(d => d.Title, o => o.MapFrom(s => s.Name));
    }
}