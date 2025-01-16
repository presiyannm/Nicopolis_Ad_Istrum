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

        public async Task<List<Exhibit>> GetAllExhibitsByCollectionIdAsync(int collectionId)
        {
            var exhibits = await dbContext.Exhibits
                .Include(c => c.Collection)
                .Include(c => c.ApplicationUser)
                .Include(c => c.Location)
                .Include(c => c.Era)
                .Include(c => c.Acquisition)
                .Where(e => e.CollectionId == collectionId).ToListAsync();

            return exhibits;
        }

        public async Task<List<Exhibit>> GetExhibitsAsync(int collectionId, int? eraId, string sortOrder)
        {
            var exhibits = dbContext.Exhibits
                .Include(e => e.Collection)
                .Include(e => e.Era)
                .Include(e => e.Location)
                .Include(e => e.Acquisition)
                .Include(e => e.ApplicationUser)
                .Where(e => e.CollectionId == collectionId);

            if (eraId.HasValue && eraId > 0)
            {
                exhibits = exhibits.Where(e => e.EraId == eraId);
            }

            exhibits = sortOrder switch
            {
                "nameAsc" => exhibits.OrderBy(e => e.Name),
                "nameDesc" => exhibits.OrderByDescending(e => e.Name),
                "eraAsc" => exhibits.OrderBy(e => e.Era.Name),
                "eraDesc" => exhibits.OrderByDescending(e => e.Era.Name),
                _ => exhibits.OrderBy(e => e.Name)
            };

            return await exhibits.ToListAsync();
        }

        public Task<List<Event>> GetAllEventsAsync()
        {
            var events = dbContext.Events.Include(e => e.ApplicationUser).ToListAsync();

            return events;
        }
    }
}
