
using Nicopolis_Ad_Istrum.Models;

namespace Nicopolis_Ad_Istrum.Interfaces
{
    public interface IUserService
    {
        Task<List<Collection>> GetAllCollectionsAsync();
    }
}
