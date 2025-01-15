
using Nicopolis_Ad_Istrum.Models;
using Nicopolis_Ad_Istrum.Models.Constants;
using Nicopolis_Ad_Istrum.Models.Identity;

namespace Nicopolis_Ad_Istrum.Interfaces
{
    public interface IUserService
    {
        Task<List<Collection>> GetAllCollectionsAsync();
        Task<List<Location>> GetLocationsAsync();
        Task<List<Era>> GetErasAsync();
        Task<List<ApplicationUser>> GetAssociatesAsync();
        Task<List<Exhibit>> GetAllExhibitsByCollectionIdAsync(int collectionId);
        Task<List<Exhibit>> GetExhibitsAsync(int collectionId, int? eraId, string sortOrder);
    }
}
