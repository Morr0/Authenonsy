using System;
using Auth.Core.Models;
using Auth.Core.Models.Auth;
using Auth.Core.Services.TimeService;
using Auth.Core.Utilities;

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
                Password = Hasher.Hash(password),
                Email = email,
                Username = username.ToLowerInvariant()
            };

            user.Id = Guid.NewGuid().ToString();

            user.CreatedAt = _timeService.GetDateTime();

            return user;
        }
    }
}