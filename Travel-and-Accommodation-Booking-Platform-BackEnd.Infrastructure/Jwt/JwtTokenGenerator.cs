 
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Jwt;

public class JwtTokenGenerator : ITokenGenerator
{
    private readonly byte[] _keyInBytes;
    private readonly JwtSettings _jwtSettings;
    public JwtTokenGenerator(IOptions<JwtSettings> keyOptions)
    {
        _keyInBytes = Encoding.UTF8.GetBytes(keyOptions.Value.SymmetricSecurityKey);
        _jwtSettings = keyOptions.Value;
    }

    public string GenerateToken(Guid sub,string role,string email,string userName)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
         

        var claims = new List<Claim>()
        {
           new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
           new(JwtRegisteredClaimNames.Sub,sub.ToString()),
           new(JwtRegisteredClaimNames.Email,email),
           new(ClaimTypes.Role,role),
           new(ClaimTypes.Name,userName)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = 
                new SigningCredentials(new SymmetricSecurityKey(_keyInBytes)
                    ,SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }
}

 