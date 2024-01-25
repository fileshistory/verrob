using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Shared;

namespace Infrastructure.Helpers.Auth;

public class JwtHelpers
{
    private readonly SigningCredentials _signingCredentials;
    private readonly JwtSecurityTokenHandler _jwtTokenHandler;

    public JwtHelpers(SymmetricSecurityKey securityKey)
    {
        _signingCredentials = new SigningCredentials(securityKey, AppSettings.Auth.SecurityAlgorithm);
        _jwtTokenHandler = new JwtSecurityTokenHandler();
    }

    public string SerializeToken(string id, string roles)
    {
        var descriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = _signingCredentials,
            Expires = DateTime.UtcNow.Add(TimeSpan.FromDays(7)),
            Subject = new ClaimsIdentity(new Claim[]
            {
                new("id", id),
                new("roles", roles)
            }),
        };

        var token = _jwtTokenHandler.CreateToken(descriptor);

        return _jwtTokenHandler.WriteToken(token);
    }
}