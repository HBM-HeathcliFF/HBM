using HBM.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HBM.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<HbmDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });
            services.AddScoped<IHbmDbContext>(provider => provider.GetService<HbmDbContext>());
            return services;
        }
    }
}