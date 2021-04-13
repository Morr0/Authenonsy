using System.Collections.Generic;
using Auth.Core.Models;
using Auth.Core.Services.RandomStringService;
using Auth.Core.Services.TimeService;

namespace Auth.Core.Factories
{
    public class ApplicationFactory
    {
        private readonly ITimeService _timeService;
        private readonly IRandomStringService _randomStringService;

        public ApplicationFactory(ITimeService timeService, IRandomStringService randomStringService)
        {
            _timeService = timeService;
            _randomStringService = randomStringService;
        }
        
        public Application Create(string creatorId, string name, string description, string websiteUrl, string redirectUrl, 
            List<string> scopes = null)
        {
            var application = new Application
            {
                CreatorId = creatorId,
                Name = name,
                Description = description,
                WebsiteUrl = websiteUrl,
                RedirectUrl = redirectUrl,
                Scopes = scopes
            };

            application.ClientId = _randomStringService.NextValue();
            application.ClientSecret = _randomStringService.NextValue();
            
            application.CreatedAt = _timeService.GetDateTime();

            return application;
        }

        public void SetFirstPartyApplication(Application application)
        {
            application.FirstParty = true;
        }
    }
}