using System.Net.Http.Headers;
using System.Text;
using Infrastructure.Spotify;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API.Controllers;

public class AuthController(
    IMediator mediator,
    IOptions<SpotifyOptions> spotifyOptions,
    IHttpClientFactory httpClientFactory) : ApiControllerBase(mediator)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();

    [HttpPost("token")]
    public async Task<IActionResult> ExchangeToken([FromBody] TokenRequest req)
    {
        var credentials = Convert.ToBase64String(
            Encoding.UTF8.GetBytes($"{spotifyOptions.Value.ClientId}:{spotifyOptions.Value.ClientSecret}")
        );

        var body = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["grant_type"] = "authorization_code",
            ["code"] = req.Code,
            ["redirect_uri"] = spotifyOptions.Value.RedirectUri,
        });

        var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token")
        {
            Content = body
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);

        var response = await _httpClient.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            return BadRequest(json);
        }

        return Content(json, "application/json");
    }
}

public record TokenRequest(string Code);