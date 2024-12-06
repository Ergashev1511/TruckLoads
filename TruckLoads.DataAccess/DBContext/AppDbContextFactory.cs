using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckLoads.DataAccess.DBContext
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Loyihaning to'liq yo'lini olish
            var path = Directory.GetCurrentDirectory();

            // appsettings.json faylini topish
            // D:\C#.net\repos\TruckLoadsBot\TruckLoadsBot.DataAccess
            if (!File.Exists(Path.Combine(path, "appsettings.json")))
            {
                path = Path.Combine(path, "..", "TruckLoads");
            }

            // appsettings.json faylini o'qish
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(path)  // to'g'ri yo'lni sozlash
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnectionString");

            optionsBuilder.UseNpgsql(connectionString); // PostgreSQL uchun

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
