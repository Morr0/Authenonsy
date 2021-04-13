using System.Collections.Generic;
using System.Threading.Tasks;
using Auth.Auth.Api.Controllers.User.Requests;
using Auth.Core.Factories;
using Auth.Core.Models;

namespace Auth.Auth.Api.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserFactory _userFactory;
        private readonly Dictionary<string, User> _usersById;
        private readonly Dictionary<string, User> _usersByUsername;

        public UserService(UserFactory userFactory)
        {
            _userFactory = userFactory;

            _usersById = new Dictionary<string, User>();
            _usersByUsername = new Dictionary<string, User>();
        }

        public async Task<User> Create(UserCreateRequest dto)
        {
            var user = _userFactory.Create(dto.Username, dto.Name, dto.Password, dto.Email);
            
            _usersById.Add(user.Id, user);
            _usersByUsername.Add(user.Username, user);

            return user;
        }

        public async Task<User> GetById(string userId)
        {
            bool exists = _usersById.TryGetValue(userId, out var user);

            return exists ? user : null;
        }

        public async Task<User> GetByUsername(string username)
        {
            bool exists = _usersByUsername.TryGetValue(username, out var user);

            return exists ? user : null;
        }

        public async Task<bool> Exists(string username, string password)
        {
            bool exists = _usersByUsername.TryGetValue(username, out var user);
            if (!exists) return false;

            bool samePassword = user.Password == password;

            return samePassword;
        }
    }
}