using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Auth.Api.Controllers.Application.Requests;
using Auth.Core.Factories;
using Auth.Core.Models;
using Auth.Data.Repositories.Database;
using Microsoft.EntityFrameworkCore;

namespace Auth.Auth.Api.Services.ApplicationService
{
    public class ApplicationService : IApplicationService
    {
        private readonly ApplicationFactory _applicationFactory;
        private readonly DatabaseContext _context;

        public ApplicationService(ApplicationFactory applicationFactory, DatabaseContext context)
        {
            _applicationFactory = applicationFactory;
            _context = context;
        }
        
        public async Task<Application> Create(ApplicationCreateRequest dto)
        {
            var application =
                _applicationFactory.Create(dto.CreatorId, dto.Name, dto.Description, dto.WebsiteUrl, dto.RedirectUrl);

            bool anyExistingApps = await _context.Application.AsNoTracking().AnyAsync().ConfigureAwait(false);
            if (!anyExistingApps) _applicationFactory.SetFirstPartyApplication(application);

            await _context.Application.AddAsync(application).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return application;
        }

        public async Task<Application> Get(string applicationClientId)
        {
            var application = await _context.Application.AsNoTracking()
                .FirstOrDefaultAsync(x => x.ClientId == applicationClientId).ConfigureAwait(false);

            return application;
        }
    }
}