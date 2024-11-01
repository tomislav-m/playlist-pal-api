using Application.Interfaces;
using Infrastructure.Spotify;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DependencyInjection));
        services.AddHttpContextAccessor();
        
        services.AddScoped<ISpotifyUserService, SpotifyUserService>();
        services.AddScoped<ISpotifyPlaylistService, SpotifyPlaylistService>();
        services.AddScoped<ISpotifyArtistService, SpotifyArtistService>();
        services.AddScoped<ISpotifyTrackService, SpotifyTrackService>();
        
        return services;
    }
}