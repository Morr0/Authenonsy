using System.Threading.Tasks;
using Auth.Auth.Api.Controllers.Application.Requests;
using Auth.Core.Models;

namespace Auth.Auth.Api.Services.ApplicationService
{
    public interface IApplicationService
    {
        Task<Application> Create(string creatorId, ApplicationCreateRequest applicationCreateRequest);
        Task<Application> Get(string applicationClientId);
    }
}