using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Interfaces.Auth;
using TravelBookingPlatform.Domain.Model;

namespace TravelBookingPlatform.Infrastructure.Auth;

public class JwtTokenGenerator : IJwtTokenGenerator
{
  private readonly JwtAuthConfig _jwtAuthConfig;

  public JwtTokenGenerator(IOptions<JwtAuthConfig> jwtAuthConfig)
  {
    _jwtAuthConfig = jwtAuthConfig.Value;
  }

  public JwtToken Generate(User user)
  {
    var claims = new List<Claim>
    {
      new("sub", user.Id.ToString()),
      new("firstName", user.FirstName),
      new("lastName", user.LastName),
      new("email", user.Email)
    };

    claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.UserRole.ToString())));

    var signingCredentials = new SigningCredentials(
      new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtAuthConfig.SecretKey)),
      SecurityAlgorithms.HmacSha256);

    var jwtSecurityToken = new JwtSecurityToken(
      _jwtAuthConfig.Issuer,
      _jwtAuthConfig.Audience,
      claims,
      DateTime.UtcNow,
      DateTime.UtcNow.AddMinutes(_jwtAuthConfig.TokenLifetimeMinutes),
      signingCredentials
    );

    var token = new JwtSecurityTokenHandler()
      .WriteToken(jwtSecurityToken);

    return new JwtToken(token);
  }
}