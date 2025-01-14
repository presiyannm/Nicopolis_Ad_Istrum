using Nicopolis_Ad_Istrum.Models.Constants;

namespace Nicopolis_Ad_Istrum.Interfaces
{
    public interface IAssociateService
    {
        Task<List<Era>> GetAllErasAsync();

        Task<List<Location>> GetAllLocationsAsync();
    }
}
