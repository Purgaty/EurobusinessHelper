﻿using System.Net;
using EurobusinessHelper.Application;
using EurobusinessHelper.Application.Identities.Security;
using EurobusinessHelper.Domain.Config;
using EurobusinessHelper.Infrastructure;
using Microsoft.AspNetCore.Authentication;
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
                .RegisterMappings()
                .SetUpAuthentication(configuration)
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo {Title = "EurobusinessHelper", Version = "v1"});
                });
            ;
    }
    
    
    private static IServiceCollection SetUpAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var config = GetAppConfig(configuration);
        var builder = CreateAuthenticationBuilder(services);
        
        if (config.ActiveAuthenticationTypes.Contains(AuthenticationType.Google))
            AddGoogleAuthentication(configuration, builder);
        if (config.ActiveAuthenticationTypes.Contains(AuthenticationType.Microsoft))
            AddMicrosoftAuthentication(configuration, builder);
        if (config.ActiveAuthenticationTypes.Contains(AuthenticationType.Facebook))
            AddFacebookAuthentication(configuration, builder);

        return services;
    }

    private static void AddFacebookAuthentication(IConfiguration configuration, AuthenticationBuilder builder)
    {
        builder.AddFacebook(FacebookDefaults.AuthenticationScheme, options =>
        {
            options.AppId = configuration["Authentication:Facebook:AppId"];
            options.AppSecret = configuration["Authentication:Facebook:AppSecret"];
        });
    }

    private static void AddMicrosoftAuthentication(IConfiguration configuration, AuthenticationBuilder builder)
    {
        builder.AddMicrosoftAccount(MicrosoftAccountDefaults.AuthenticationScheme, options =>
        {
            options.ClientId = configuration["Authentication:Microsoft:ClientId"];
            options.ClientSecret = configuration["Authentication:Microsoft:ClientSecret"];
        });
    }

    private static void AddGoogleAuthentication(IConfiguration configuration, AuthenticationBuilder builder)
    {
        builder.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
        {
            options.ClientId = configuration["Authentication:Google:ClientId"];
            options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
        });
    }

    private static AuthenticationBuilder CreateAuthenticationBuilder(IServiceCollection services)
    {
        return services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                    return Task.CompletedTask;
                };
            });
    }

    private static AppConfig GetAppConfig(IConfiguration configuration)
    {
        var config = new AppConfig();
        configuration.GetSection(nameof(AppConfig)).Bind(config);
        return config;
    }
}