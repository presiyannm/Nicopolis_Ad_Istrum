using Nicopolis_Ad_Istrum.Models.Helpers;
using Nicopolis_Ad_Istrum.Models.Identity;

namespace Nicopolis_Ad_Istrum.Interfaces
{
    public interface IAdminService
    {
        Task<PaginatedList<ApplicationUser>> GetApplicationUsersAsync(int pageIndex, int pageSize, string sortBy, bool ascending);
        Task<int> GetTotalUsersCountAsync();
    }
}
