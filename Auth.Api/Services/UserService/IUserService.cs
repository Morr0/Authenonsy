using System.Threading.Tasks;
using Auth.Api.Controllers.User.Requests;
using Auth.Core.Models;

namespace Auth.Api.Services.UserService
{
    public interface IUserService
    {
        Task<User> Create(UserCreateRequest userCreateRequest);
        Task<User> GetById(string userId);
        Task<User> GetByUsername(string username);
        Task<bool> Exists(string username, string password);
    }
}