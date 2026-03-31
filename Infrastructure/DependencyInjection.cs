using Application.Interfaces;
using Infrastructure.Spotify;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(_ => {}, typeof(DependencyInjection));
        services.AddHttpContextAccessor();

        services.Configure<SpotifyOptions>(configuration.GetSection("Spotify"));
        
        services.AddScoped<ISpotifyUserService, SpotifyUserService>();
        services.AddScoped<ISpotifyPlaylistService, SpotifyPlaylistService>();
        services.AddScoped<ISpotifyArtistService, SpotifyArtistService>();
        services.AddScoped<ISpotifyTrackService, SpotifyTrackService>();
        
        return services;
    }
}