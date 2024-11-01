using AutoMapper;
using Microsoft.AspNetCore.Http;
using SpotifyAPI.Web;

namespace Infrastructure.Spotify;

public abstract class SpotifyBaseService
{
    private const string SpotifyTokenHeader = "spotify-token";
    
    protected const string MarketUS = "HR";

    protected readonly IMapper Mapper;
    protected readonly ISpotifyClient SpotifyClient;

    protected SpotifyBaseService(IHttpContextAccessor contextAccessor, IMapper mapper)
    {
        Mapper = mapper;

        if (contextAccessor.HttpContext is not null && contextAccessor.HttpContext.Request.Headers.TryGetValue(SpotifyTokenHeader, out var header))
        {
            SpotifyClient = new SpotifyClient(GetTokenConfig(header!));
        }
        else
        {
            SpotifyClient = new SpotifyClient(GetConfig());
        }
    }

    protected Task<PrivateUser> GetCurrentUser(CancellationToken cancellationToken)
    {
        return SpotifyClient.UserProfile.Current(cancellationToken);
    }

    private static SpotifyClientConfig GetConfig()
    {
        var config = SpotifyClientConfig.CreateDefault()
            .WithRetryHandler(new SimpleRetryHandler { TooManyRequestsConsumesARetry = true, RetryTimes = 2 })
            .WithAuthenticator(new ClientCredentialsAuthenticator("c1ebf6c270e7494190ade10285721a01", "878c4ab18b6346d384ce312224aa98c6"));

        return config;
    }

    private static SpotifyClientConfig GetTokenConfig(string token)
    {
        var config = SpotifyClientConfig.CreateDefault()
            .WithRetryHandler(new SimpleRetryHandler { TooManyRequestsConsumesARetry = true, RetryTimes = 2 })
            .WithToken(token);

        return config;
    }
}