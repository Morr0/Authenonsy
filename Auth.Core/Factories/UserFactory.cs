using System;
using Auth.Core.Models;
using Auth.Core.Services.TimeService;

namespace Auth.Core.Factories
{
    public class UserFactory
    {
        private readonly ITimeService _timeService;

        public UserFactory(ITimeService timeService)
        {
            _timeService = timeService;
        }
        
        public User Create(string username, string name, string password, string email)
        {
            var user = new User
            {
                Name = name,
                Password = password,
                Email = email,
                Username = username.ToLowerInvariant()
            };

            user.Id = Guid.NewGuid().ToString();

            user.CreatedAt = _timeService.GetDateTime();

            return user;
        }

        public UserApplicationAccess CreateAccess(string applicationId, string userId)
        {
            return new UserApplicationAccess
            {
                UserId = userId,
                ApplicationClientId = applicationId,
                CreatedAt = _timeService.GetDateTime()
            };
        }
    }
}