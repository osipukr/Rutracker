﻿using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Rutracker.Server.Settings
{
    public class JwtSettings
    {
        // ToDo: get this from somewhere secure
        private const string SecretKey = "yfm273rj31v1v2nv1m1ddl1ksxaj012";

        public static readonly SymmetricSecurityKey SigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromDays(10);

        public DateTime Expiration => IssuedAt.Add(ValidFor);
        public DateTime NotBefore => DateTime.UtcNow;
        public DateTime IssuedAt => DateTime.UtcNow;

        public Func<Task<string>> JtiGenerator => () => Task.FromResult(Guid.NewGuid().ToString());
        public SigningCredentials SigningCredentials => new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256);
    }
}