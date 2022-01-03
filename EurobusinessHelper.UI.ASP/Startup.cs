using System;
using EurobusinessHelper.Domain.Models.Config;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace EurobusinessHelper.UI.ASP
{
    public class Startup
    {
        private AppConfig _appConfig;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _appConfig = Configuration.GetSection(nameof(AppConfig)).Get<AppConfig>();
        }
        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            switch (_appConfig.AuthenticationType)
            {
                case AuthenticationType.Microsoft:
                    services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                        .AddMicrosoftIdentityWebApp(Configuration.GetSection("AzureAd"));
                    break;
                case AuthenticationType.Google:
                    services.AddAuthentication()
                        .AddGoogle(options =>
                        {
                            options.ClientId = Configuration["GoogleAuth:ClientId"];
                            options.ClientSecret = Configuration["GoogleAuth:ClientSecret"];
                        });
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_appConfig.AuthenticationType));
            }
            

            services.InjectDependencies(Configuration);
            
            services
                .AddControllers(options =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    options.Filters.Add(new AuthorizeFilter(policy));
                })
                .AddNewtonsoftJson();
            
            // services.AddDbContext<Entities>(b => b.UseSqlServer(Configuration.GetConnectionString("Entities")));
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "EurobusinessHelper.UI.ASP", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EurobusinessHelper.UI.ASP v1"));
            }

            loggerFactory.AddLog4Net();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
