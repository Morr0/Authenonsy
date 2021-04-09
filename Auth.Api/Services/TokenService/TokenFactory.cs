using System;
using System.Security.Cryptography;
using System.Text;
using Auth.Api.Services.TokenService.Models;
using Auth.Core.Services.TimeService;

namespace Auth.Api.Services.TokenService
{
    public class TokenFactory : IDisposable
    {
        private readonly ITimeService _timeService;
        private readonly MD5 _md5 = new MD5CryptoServiceProvider(); 

        public TokenFactory(ITimeService timeService)
        {
            _timeService = timeService;
        }

        public AccessToken Create()
        {
            var now = _timeService.GetDateTime();
            var expiry = now.AddHours(1);
            return new AccessToken
            {
                Token = Token(),
                CreatedAt = now,
                ExpiresAt = expiry
            };
        }

        private string Token()
        {
            string proposedToken = Guid.NewGuid().ToString();
            var hashBytes = _md5.ComputeHash(Encoding.UTF8.GetBytes(proposedToken));
            return Convert.ToBase64String(hashBytes);
        }

        public void Dispose()
        {
            _md5.Dispose();
        }
    }
}