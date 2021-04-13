using Auth.Auth.Api.Services.ApplicationService;
using Auth.Auth.Api.Services.TimeService;
using Auth.Auth.Api.Services.TokenService;
using Auth.Auth.Api.Services.UserService;
using Auth.Core.Factories;
using Auth.Core.Services.TimeService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Auth.Auth.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddHybridModelBinder();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Auth.Auth.Api", Version = "v1"}); });

            services.AddSingleton<UserFactory>();
            services.AddSingleton<ApplicationFactory>();
            services.AddSingleton<TokenFactory>();
            
            services.AddSingleton<ITimeService, TimeService>();
            
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IApplicationService, ApplicationService>();
            services.AddSingleton<ITokenService, TokenService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth.Auth.Api v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}