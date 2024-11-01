using Application.Playlist.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DependencyInjection));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GeneratePlaylist>());
        
        return services;
    }
}