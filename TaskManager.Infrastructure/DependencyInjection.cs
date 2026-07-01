using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Repositories;
using TaskManager.Application.Interfaces;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure (this IServiceCollection services,string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString)
            );

            services.AddScoped<ITaskRepository,TaskRepository>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();

            return services;
        }
    }
}
