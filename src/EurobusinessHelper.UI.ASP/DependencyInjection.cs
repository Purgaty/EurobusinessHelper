using System.Net;
using EurobusinessHelper.Application;
using EurobusinessHelper.Application.Identities.Security;
using EurobusinessHelper.Domain.Config;
using EurobusinessHelper.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.OpenApi.Models;

namespace EurobusinessHelper.UI.ASP;

public static class DependencyInjection
{
    public static IServiceCollection InjectDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        return services
                .Configure<AppConfig>(options => configuration.GetSection(nameof(AppConfig)).Bind(options))
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddScoped<ISecurityContext, SecurityContext>()
                .InjectApplicationDependencies()
                .InjectInfrastructureDependencies(configuration)
                .SetUpAuthentication(configuration)
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo {Title = "EurobusinessHelper", Version = "v1"});
                });
            ;
    }
    
    
    private static IServiceCollection SetUpAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                    return Task.CompletedTask;
                };
            })
            .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
            {
                options.ClientId = configuration["Authentication:Google:ClientId"];
                options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
            })
            .AddMicrosoftAccount(MicrosoftAccountDefaults.AuthenticationScheme, options =>
            {
                options.ClientId = configuration["Authentication:Microsoft:ClientId"];
                options.ClientSecret = configuration["Authentication:Microsoft:ClientSecret"];
            })
            .AddFacebook(FacebookDefaults.AuthenticationScheme, options =>
            {
                options.AppId = configuration["Authentication:Facebook:AppId"];
                options.AppSecret = configuration["Authentication:Facebook:AppSecret"];
            });

        return services;
    }
}