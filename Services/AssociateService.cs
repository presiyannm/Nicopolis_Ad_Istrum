using Microsoft.EntityFrameworkCore;
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
            var collection = new Collection()
            { 
                Name = viewModel.Name,
                Description = viewModel.Description,
                EraId = viewModel.EraId,
                LocationId = viewModel.LocationId,
                ApplicationUserId = viewModel.AssociateId,
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
                    PhotoFileName = fileName
                };

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
    }
}
