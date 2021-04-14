using System.Linq;
using System.Threading.Tasks;
using Auth.Auth.Api.Controllers.User.Requests;
using Auth.Core.Factories;
using Auth.Core.Models;
using Auth.Data.Repositories.Database;
using Microsoft.EntityFrameworkCore;

namespace Auth.Auth.Api.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserFactory _userFactory;
        private readonly DatabaseContext _context;

        public UserService(UserFactory userFactory, DatabaseContext context)
        {
            _userFactory = userFactory;
            _context = context;
        }

        public async Task<User> Create(UserCreateRequest dto)
        {
            var user = _userFactory.Create(dto.Username, dto.Name, dto.Password, dto.Email);

            await _context.AddAsync(user).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return user;
        }

        public async Task<User> GetById(string userId)
        {
            var user = await _context.User.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId).ConfigureAwait(false);

            return user;
        }

        public async Task<User> GetByUsername(string username)
        {
            var user = await _context.User.AsNoTracking().FirstOrDefaultAsync(x => x.Username == username).ConfigureAwait(false);

            return user;
        }

        public bool IsCorrectPassword(User user, string password)
        {
            // TODO implement with hashing
            return user.Password == password;
        }
    }
}