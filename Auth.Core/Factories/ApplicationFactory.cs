using System;
using System.Security.Cryptography;
using System.Text;
using Auth.Core.Models;
using Auth.Core.Services.TimeService;

namespace Auth.Core.Factories
{
    public class ApplicationFactory : IDisposable
    {
        private readonly ITimeService _timeService;
        private readonly SHA1 _sha1 = SHA1.Create();

        public ApplicationFactory(ITimeService timeService)
        {
            _timeService = timeService;
        }
        
        public Application Create(string creatorId, string name, string description, string websiteUrl, string redirectUrl)
        {
            var application = new Application
            {
                CreatorId = creatorId,
                Name = name,
                Description = description,
                WebsiteUrl = websiteUrl,
                RedirectUrl = redirectUrl
            };

            application.ClientId = CreateClientId(creatorId);
            application.ClientSecret = CreateClientSecret();
            
            application.CreatedAt = _timeService.GetDateTime();

            return application;
        }

        private string CreateClientSecret()
        {
            string proposedString = Guid.NewGuid().ToString();
            return Hash(proposedString);
        }

        private string CreateClientId(string creatorId)
        {
            string proposedString = $"{Guid.NewGuid().ToString()}-{creatorId}-{_timeService.GetDateTime().Millisecond.ToString()}";
            return Hash(proposedString);
        }

        private string Hash(string proposedString)
        {
            var proposedStringBytes = Encoding.UTF8.GetBytes(proposedString);

            var hashBytes = _sha1.ComputeHash(proposedStringBytes, 0, proposedStringBytes.Length);
            return Encoding.UTF8.GetString(hashBytes);
        }

        public void Dispose()
        {
            _sha1.Dispose();
        }
    }
}