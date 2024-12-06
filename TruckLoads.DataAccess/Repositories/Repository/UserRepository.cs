using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckLoads.DataAccess.DBContext;
using TruckLoads.DataAccess.Repositories.IRepository;
using TruckLoads.Entity.Entities;

namespace TruckLoads.DataAccess.Repositories.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext; 
        }
        public async Task<long> AddUserAsync(User user)
        {
            try
            {
                await _dbContext.Users.AddAsync(user);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}
