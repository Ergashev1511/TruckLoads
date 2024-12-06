using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckLoads.Entity.Entities;

namespace TruckLoads.DataAccess.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<User> Users { get; set; }
    }
}
