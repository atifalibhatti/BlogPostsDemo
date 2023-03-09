using System.Net.Http.Headers;
using System.Net;

namespace LetsPost.Search.Api.Middleware;
public class RequestAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    public RequestAuthMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("Authorization", out var accessToken))
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync("UnAuthorized");
            return;
        }
        var token = accessToken.First().ToString().Replace("Bearer", "");

        HttpClient client = new();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync(_configuration["AuthHostEndpoint"]);
        bool isValid = false;
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            isValid = bool.Parse(responseContent);
        }

        if (!isValid)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync("UnAuthorized");
            return;
        }

        await _next(context);
    }
}