using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Nicopolis_Ad_Istrum.Data;
using Nicopolis_Ad_Istrum.Interfaces;
using Nicopolis_Ad_Istrum.Models;
using Nicopolis_Ad_Istrum.Models.Constants;
using Nicopolis_Ad_Istrum.Models.ViewModels;

namespace Nicopolis_Ad_Istrum.Services
{
    public class AssociateService : IAssociateService
    {
        private ApplicationDbContext dbContext;

        public AssociateService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddCollectionAsync(AddCollectionViewModel viewModel)
        {
            var era = await dbContext.Eras.FirstOrDefaultAsync(e => e.Id == viewModel.EraId);

            if(era is null)
            {
                throw new Exception("Era cannot be null");
            }

            var location = await dbContext.Locations.FirstOrDefaultAsync(l => l.Id ==  viewModel.LocationId);

            if (location is null)
            {
                throw new Exception("Location cannot be null");
            }

            var user = await dbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == viewModel.AssociateId);

            if (user is null)
            {
                throw new Exception("User cannot be null");
            }

            var collection = new Collection()
            { 
                Name = viewModel.Name,
                Description = viewModel.Description,
                EraId = viewModel.EraId,
                LocationId = viewModel.LocationId,
                ApplicationUserId = viewModel.AssociateId,
                Era = era,
                Location = location,
                ApplicationUser = user,
            };
            
            dbContext.Collections.Add(collection);

            await dbContext.SaveChangesAsync();
        }

        public async Task AddExhibitAsync(AddExhibitViewModel viewModel)
        {
            if (viewModel.PhotoFileName != null && viewModel.PhotoFileName.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(viewModel.PhotoFileName.FileName);

                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                var filePath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await viewModel.PhotoFileName.CopyToAsync(stream);
                }

                var era = await dbContext.Eras.FirstOrDefaultAsync(e => e.Id == viewModel.EraId);
                var location = await dbContext.Locations.FirstOrDefaultAsync(e => e.Id == viewModel.LocationId);
                var acquisition = await dbContext.Acquisitions.FirstOrDefaultAsync(e => e.Id == viewModel.AcquisitionId);
                var collection = await dbContext.Collections.FirstOrDefaultAsync(e => e.Id == viewModel.CollectionId);
                var user = await dbContext.ApplicationUsers.FirstOrDefaultAsync(e => e.Id == viewModel.AssociateId);

                if (era is null || location is null || acquisition is null || collection is null || user is null)
                {
                    throw new NullReferenceException();
                }

                var exhibit = new Exhibit()
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    Origin = viewModel.Origin,
                    EraId = viewModel.EraId,
                    LocationId = viewModel.LocationId,
                    ApplicationUserId = viewModel.AssociateId,
                    AcquisitionId = viewModel.AcquisitionId,
                    CollectionId = viewModel.CollectionId,
                    PhotoFileName = fileName,
                    Era = era,
                    Location = location,
                    Acquisition = acquisition,
                    Collection = collection,
                    ApplicationUser = user,
                };

                collection.Exhibits.Add(exhibit);

                dbContext.Exhibits.Add(exhibit);

                await dbContext.SaveChangesAsync();
            }
            else
            {
                var exhibit = new Exhibit()
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    Origin = viewModel.Origin,
                    EraId = viewModel.EraId,
                    LocationId = viewModel.LocationId,
                    ApplicationUserId = viewModel.AssociateId,
                    AcquisitionId = viewModel.AcquisitionId,
                    CollectionId = viewModel.CollectionId
                };

                dbContext.Exhibits.Add(exhibit);

                await dbContext.SaveChangesAsync();
            }
        }


        public async Task<List<Acquisition>> GetAllAcquisitionAsync()
        {
            var acquisitions = await dbContext.Acquisitions.ToListAsync();

            return acquisitions;
        }

        public async Task<List<Collection>> GetAllCollectionsAsync()
        {
            var collections = await dbContext.Collections.ToListAsync();

            return collections;
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

        public async Task<Collection> GetCollectionByIdAsync(int collectionId)
        {
            var collection = await dbContext.Collections.FirstOrDefaultAsync(c => c.Id == collectionId);

            if (collection is null)
            {
                throw new Exception("Collection cannot be null");
            }

            return collection;
        }

        public async Task<Exhibit> GetExhibitByIdAsync(int exhibitId)
        {
            var exhibit = await dbContext.Exhibits
                .Include(c => c.ApplicationUser)
                .Include(c => c.Era)
                .Include(c => c.Location)
                .Include(c => c.Acquisition)
                .Include(c => c.Collection)
                .FirstOrDefaultAsync(c => c.Id == exhibitId);

            if (exhibit is null)
            {
                throw new Exception("Exhibit cannot be null");
            }

            return exhibit;
        }

        public async Task UpdateCollection(AddCollectionViewModel viewModel)
        {
            var collection = await dbContext.Collections
                .Include(c => c.ApplicationUser)
                .Include(c => c.Era)
                .Include(c => c.Location)
                .FirstOrDefaultAsync(c => c.Id == viewModel.Id);

            if (collection is null)
            {
                throw new Exception("Collection cannot be null");
            }

            collection.Name = viewModel.Name;

            collection.Description = viewModel.Description;

            var newEra = await dbContext.Eras.FirstOrDefaultAsync(e => e.Id == viewModel.EraId);

            if (newEra is null)
            {
                throw new Exception("Era cannot be null");
            }

            var newLocation = await dbContext.Locations.FirstOrDefaultAsync(l => l.Id == viewModel.LocationId);

            if (newLocation is null)
            {
                throw new Exception("Location cannot be null");
            }

            collection.Location = newLocation;

            collection.Era = newEra;

            collection.EraId = viewModel.EraId;

            collection.LocationId = viewModel.LocationId;

            await dbContext.SaveChangesAsync();
        }
        public async Task UpdateExhibit(AddExhibitViewModel viewModel)
        {
            var exhibit = await GetExhibitByIdAsync(viewModel.Id);

            if (exhibit == null)
            {
                throw new ArgumentException($"Exhibit with ID {viewModel.AssociateId} not found.");
            }

            exhibit.Name = viewModel.Name;
            exhibit.Description = viewModel.Description;
            exhibit.Origin = viewModel.Origin;
            exhibit.EraId = viewModel.EraId;
            exhibit.CollectionId = viewModel.CollectionId;
            exhibit.AcquisitionId = viewModel.AcquisitionId;
            exhibit.LocationId = viewModel.LocationId;

            if (viewModel.PhotoFileName != null)
            {
                var uniqueFileName = $"{Guid.NewGuid()}_{viewModel.PhotoFileName.FileName}";

                var uploadsFolder = Path.Combine("wwwroot", "images"); 

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await viewModel.PhotoFileName.CopyToAsync(fileStream);
                }

                exhibit.PhotoFileName = $"{uniqueFileName}";
            }

            dbContext.Exhibits.Update(exhibit);

            await dbContext.SaveChangesAsync();
        }
    }
}
