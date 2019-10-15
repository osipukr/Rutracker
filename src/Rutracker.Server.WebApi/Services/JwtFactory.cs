using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.WebApi.Interfaces;
using Rutracker.Server.WebApi.Settings;

namespace Rutracker.Server.WebApi.Services
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtSettings _jwtSettings;

        public JwtFactory(IOptions<JwtSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;

            Guard.Against.Null(_jwtSettings, nameof(_jwtSettings));

            if (_jwtSettings.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(_jwtSettings.ValidFor));
            }

            Guard.Against.Null(_jwtSettings.SigningCredentials, nameof(_jwtSettings.SigningCredentials));
            Guard.Against.Null(_jwtSettings.JtiGenerator, nameof(_jwtSettings.JtiGenerator));
        }

        public async Task<string> GenerateTokenAsync(User user, IEnumerable<string> roles)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtSettings.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtSettings.IssuedAt).ToString(), ClaimValueTypes.Integer64)
            };

            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x));

            var jwt = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims.Concat(roleClaims),
                notBefore: _jwtSettings.NotBefore,
                expires: _jwtSettings.Expiration,
                signingCredentials: _jwtSettings.SigningCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private static long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        }
    }
}