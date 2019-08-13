using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.WebApi.Interfaces;
using Rutracker.Server.WebApi.Settings;
using Rutracker.Shared.Models.ViewModels.Accounts;

namespace Rutracker.Server.WebApi.Services
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtSettings _jwtOptions;

        public JwtFactory(IOptions<JwtSettings> jwtOptions)
        {
            if (jwtOptions == null)
            {
                throw new ArgumentNullException(nameof(jwtOptions));
            }

            _jwtOptions = jwtOptions.Value;

            ThrowIfInvalidOptions(_jwtOptions);
        }

        public async Task<JwtToken> GenerateTokenAsync(User user, IEnumerable<string> roles)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64)
            };

            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x));

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims.Concat(roleClaims),
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtToken
            {
                Token = encodedJwt,
                ExpiresIn = (long)_jwtOptions.ValidFor.TotalSeconds
            };
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date) =>
            (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        private static void ThrowIfInvalidOptions(JwtSettings options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(options.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(options.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(options.JtiGenerator));
            }
        }
    }
}