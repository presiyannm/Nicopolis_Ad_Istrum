using Microsoft.EntityFrameworkCore;
using Nicopolis_Ad_Istrum.Data;
using Nicopolis_Ad_Istrum.Interfaces;
using Nicopolis_Ad_Istrum.Models;

namespace Nicopolis_Ad_Istrum.Services
{
    public class UserService : IUserService
    {
        private ApplicationDbContext dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Collection>> GetAllCollectionsAsync()
        {
            var collections = await dbContext.Collections
                .Include(c => c.Era)
                .Include(c => c.Location)
                .Include(c => c.ApplicationUser)
                .Include(c => c.Exhibits)
                .ToListAsync();

            return collections;
        }
    }
}
