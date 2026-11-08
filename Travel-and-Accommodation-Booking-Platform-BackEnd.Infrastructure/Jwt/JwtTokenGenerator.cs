 
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.RefreshTokensFiles;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.StateEnums;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Settings;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Jwt;

public class JwtTokenGenerator : ITokenGenerator
{
    private readonly byte[] _keyInBytes;
    private readonly JwtSettings _jwtSettings;
    private readonly RefreshTokenSettings _refreshTokenSettings;
    private readonly IGuidGenerator _guidGenerator;
    
    public JwtTokenGenerator(IOptions<JwtSettings> keyOptions,
        IOptions<RefreshTokenSettings> refreshTokenSettings,IGuidGenerator guidGenerator)
    {
        _keyInBytes = Encoding.UTF8.GetBytes(keyOptions.Value.SymmetricSecurityKey);
        _jwtSettings = keyOptions.Value;
        _refreshTokenSettings = refreshTokenSettings.Value;
        _guidGenerator = guidGenerator;
    }

    public string GenerateToken(Guid sub,List<SiteRole> roles,string email, string userName)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var roleClaims = roles
            .Select(role => new Claim(ClaimTypes.Role, role.ToString()))
            .ToList();
        
        var claims = new List<Claim>()
        {
           new(JwtRegisteredClaimNames.Jti,_guidGenerator.Generate().ToString()),
           new(JwtRegisteredClaimNames.Sub,sub.ToString()),
           new(JwtRegisteredClaimNames.Email,email),
           new(ClaimTypes.Name,userName)
        };
        
        claims.AddRange(roleClaims);

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

    public RefreshToken GenerateRefreshToken(Guid userId)
    {
        return new RefreshToken
        {
            Id = _guidGenerator.Generate(),
            Token = GenerateRefreshTokenValue(),
            UserId = userId,
            ExpiresOnUtc = DateTime.UtcNow.AddDays(_refreshTokenSettings.ExpiresInDays)
        };
        ;
    }

    public string GenerateRefreshTokenValue()
        => Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
}

 