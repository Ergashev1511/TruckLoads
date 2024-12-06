using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckLoads.Entity.Entities;

namespace TruckLoads.DataAccess.Repositories.IRepository
{
    public interface IUserRepository
    {
        Task<long> AddUserAsync(User user);
    }
}
