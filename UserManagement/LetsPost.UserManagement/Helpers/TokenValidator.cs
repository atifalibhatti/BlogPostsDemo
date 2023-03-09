using LetsPost.UserManagement.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace LetsPost.UserManagement.Helpers;
public class TokenValidator : ITokenValidator
{
    private readonly AuthSettings _authSettings;
    public TokenValidator(IOptions<AuthSettings> authSettings)
    {
        _authSettings = authSettings.Value;
    }
    public async Task<bool> ValidateToken(string token)
    {

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidIssuer = _authSettings.Issuer,
            ValidateAudience = false,
            ValidAudience = _authSettings.Audience,
            ValidateIssuerSigningKey = false,
            ValidateLifetime = false,
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var result = await tokenHandler.ValidateTokenAsync(token, validationParameters);
            return result.IsValid;
        }
        catch (Exception)
        {
            //log the error here.
            return false;
        }
    }
}