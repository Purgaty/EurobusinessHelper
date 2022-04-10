﻿using EurobusinessHelper.Application.Common.Interfaces;
using EurobusinessHelper.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EurobusinessHelper.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection InjectInfrastructureDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
                .AddDbContext<IApplicationDbContext, ApplicationDbContext>(b =>
                    b.UseSqlite(configuration.GetConnectionString("GameDb")!))
            ;
    }
}