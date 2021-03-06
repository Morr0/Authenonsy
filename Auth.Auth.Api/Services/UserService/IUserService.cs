using System.Threading.Tasks;
using Auth.Auth.Api.Controllers.User.Requests;
using Auth.Core.Models;

namespace Auth.Auth.Api.Services.UserService
{
    public interface IUserService
    {
        Task<User> Create(UserCreateRequest userCreateRequest);
        Task<User> GetById(string userId);
        Task<User> GetByUsername(string username);
        bool IsCorrectPassword(User user, string password);
    }
}