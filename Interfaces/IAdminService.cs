using Nicopolis_Ad_Istrum.Models.Helpers;
using Nicopolis_Ad_Istrum.Models.Identity;
using Nicopolis_Ad_Istrum.Models.ViewModels;

namespace Nicopolis_Ad_Istrum.Interfaces
{
    public interface IAdminService
    {
        Task<PaginatedList<ApplicationUser>> GetApplicationUsersAsync(int pageIndex, int pageSize, string sortBy, bool ascending, string searchTerm);
        Task<int> GetTotalUsersCountAsync();
        Task<EditUserViewModel> GetUserViewModelAsync(string userId);
        Task UpdateUserByIdAsync(EditUserViewModel viewModel);
        Task DeleteUserByIdAsync(string userId);
        Task DeleteCollectionByIdAsync(int collectionId);
        Task DeleteExhibitByIdAsync(int exhibitId);
    }
}
