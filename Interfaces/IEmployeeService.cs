using Nicopolis_Ad_Istrum.Models;
using Nicopolis_Ad_Istrum.Models.Identity;
using Nicopolis_Ad_Istrum.Models.ViewModels;

namespace Nicopolis_Ad_Istrum.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<ApplicationUser>> GetAllUsersAsync();

        Task AddEventAsync(AddEventViewModel viewModel);

        Task<Event> GetCurrentEventByIdAsync(int eventId);

        Task UpdateEvent(AddEventViewModel viewModel);
    }
}
