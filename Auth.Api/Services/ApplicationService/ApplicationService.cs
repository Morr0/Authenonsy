using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Api.Controllers.Application.Requests;
using Auth.Core.Factories;
using Auth.Core.Models;

namespace Auth.Api.Services.ApplicationService
{
    public class ApplicationService : IApplicationService
    {
        private readonly ApplicationFactory _applicationFactory;
        private readonly Dictionary<string, Application> _applications;

        public ApplicationService(ApplicationFactory applicationFactory)
        {
            _applicationFactory = applicationFactory;

            _applications = new Dictionary<string, Application>();
        }
        
        public async Task<Application> Create(string creatorId, ApplicationCreateRequest dto)
        {
            var application =
                _applicationFactory.Create(creatorId, dto.Name, dto.Description, dto.WebsiteUrl, dto.RedirectUrl);

            if (!_applications.Any()) _applicationFactory.SetFirstPartyApplication(application);
            
            _applications.Add(application.ClientId, application);

            return application;
        }

        public async Task<Application> Get(string applicationClientId)
        {
            bool exists = _applications.TryGetValue(applicationClientId, out var application);

            return exists ? application : null;
        }
    }
}