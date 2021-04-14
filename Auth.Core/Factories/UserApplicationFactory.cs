using System.Collections.Generic;
using Auth.Core.Models.Auth;
using Auth.Core.Services.RandomStringService;
using Auth.Core.Services.TimeService;

namespace Auth.Core.Factories
{
    public class UserApplicationFactory
    {
        private readonly ITimeService _timeService;
        private readonly IRandomStringService _randomStringService;

        public UserApplicationFactory(ITimeService timeService, IRandomStringService randomStringService)
        {
            _timeService = timeService;
            _randomStringService = randomStringService;
        }

        public UserApplicationAccess CreateAccess(string userId, string applicationId, List<string> scopes)
        {
            return new UserApplicationAccess
            {
                Scopes = scopes,
                CreatedAt = _timeService.GetDateTime(),
                RefreshToken = _randomStringService.NextValue(),
                UserId = userId,
                ApplicationClientId = applicationId
            };
        }
        
        public UserApplicationCodeRequest CreateCode(UserApplicationAccess applicationAccess)
        {
            var datetime = _timeService.GetDateTime();
            return new UserApplicationCodeRequest
            {
                ApplicationAccess = applicationAccess,
                Code = _randomStringService.NextValue(),
                CreatedAt = datetime,
                ExpiresAt = datetime.AddMinutes(10)
            };
        }

        public UserApplicationSession CreateSession(UserApplicationAccess applicationAccess)
        {
            var datetime = _timeService.GetDateTime();
            return new UserApplicationSession
            {
                ApplicationAccess = applicationAccess,
                AccessToken = _randomStringService.NextValue(),
                CreatedAt = datetime,
                ExpiresAt = datetime.AddHours(1)
            };
        }
    }
}