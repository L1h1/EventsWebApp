using EventsWebApp.Domain.Interfaces;
using EventsWebApp.Infrastructure.Data;
using EventsWebApp.Infrastructure.Repositories;
using EventsWebApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connStr = configuration.GetConnectionString("SqliteConnection");
            string dataDirectory = String.Empty;
            connStr = String.Format(connStr, dataDirectory);

            var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(connStr).Options;
            
            services.AddScoped<AppDbContext>(s => new AppDbContext(options));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IEventCategoryRepository, EventCategoryRepository>();
            services.AddScoped<IEventParticipantRepository, EventParticipantRepository>();
            services.AddTransient<IPhotoService,PhotoService>();

            return services;
        }
    }
}
