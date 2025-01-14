using Microsoft.EntityFrameworkCore;
using Nicopolis_Ad_Istrum.Data;
using Nicopolis_Ad_Istrum.Interfaces;
using Nicopolis_Ad_Istrum.Models.Constants;

namespace Nicopolis_Ad_Istrum.Services
{
    public class AssociateService : IAssociateService
    {
        private ApplicationDbContext dbContext;

        public AssociateService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Era>> GetAllErasAsync()
        {
            var eras = await dbContext.Eras.ToListAsync();

            return eras;
        }

        public async Task<List<Location>> GetAllLocationsAsync()
        {
            var locations = await dbContext.Locations.ToListAsync();

            return locations;
        }
    }
}
