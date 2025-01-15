using Nicopolis_Ad_Istrum.Models;
using Nicopolis_Ad_Istrum.Models.Constants;
using Nicopolis_Ad_Istrum.Models.ViewModels;

namespace Nicopolis_Ad_Istrum.Interfaces
{
    public interface IAssociateService
    {
        Task<List<Era>> GetAllErasAsync();

        Task<List<Location>> GetAllLocationsAsync();

        Task AddCollectionAsync(AddCollectionViewModel viewModel);

        Task<List<Acquisition>> GetAllAcquisitionAsync();

        Task<List<Collection>> GetAllCollectionsAsync();

        Task AddExhibitAsync(AddExhibitViewModel viewModel);

        Task<Collection> GetCollectionByIdAsync(int collectionId);

        Task UpdateCollection(AddCollectionViewModel viewModel);

        Task<Exhibit> GetExhibitByIdAsync(int exhibitId);

        Task UpdateExhibit(AddExhibitViewModel viewModel);
    }
}
