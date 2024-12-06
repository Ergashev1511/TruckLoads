using Microsoft.EntityFrameworkCore;
using TruckLoads.DataAccess.DBContext;
using TruckLoads.DataAccess.Repositories.IRepository;
using TruckLoads.DataAccess.Repositories.Repository;

namespace TruckLoads.DependencyExtensions
{
    public static class ServiceExtensions
    {
        public static void AddDbContextes(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnectionString")));
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }

    }
}
