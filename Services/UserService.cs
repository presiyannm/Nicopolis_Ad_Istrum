using Microsoft.EntityFrameworkCore;
using Nicopolis_Ad_Istrum.Data;
using Nicopolis_Ad_Istrum.Interfaces;
using Nicopolis_Ad_Istrum.Models;
using Nicopolis_Ad_Istrum.Models.Constants;
using Nicopolis_Ad_Istrum.Models.Identity;

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
        public async Task<List<Location>> GetLocationsAsync()
        {
            return await dbContext.Locations.ToListAsync();
        }

        public async Task<List<Era>> GetErasAsync()
        {
            return await dbContext.Eras.ToListAsync();
        }

        public async Task<List<ApplicationUser>> GetAssociatesAsync()
        {
            return await dbContext.ApplicationUsers
                .Where(u => u.Position == "Science Associate")
                .ToListAsync();
        }
    }
}
