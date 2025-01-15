using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nicopolis_Ad_Istrum.Interfaces;
using Nicopolis_Ad_Istrum.Models.ViewModels;

namespace Nicopolis_Ad_Istrum.Controllers
{
    public class UserController : Controller
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> SeeAllCollections(int? LocationId, int? EraId, string? AssociateId, string? sortOrder)
        {
            var collections = await userService.GetAllCollectionsAsync();

            if (LocationId.HasValue)
            {
                collections = collections.Where(c => c.LocationId == LocationId.Value).ToList();
            }
            if (EraId.HasValue)
            {
                collections = collections.Where(c => c.EraId == EraId.Value).ToList();
            }
            if (!string.IsNullOrEmpty(AssociateId))
            {
                collections = collections.Where(c => c.ApplicationUserId == AssociateId).ToList();
            }

            collections = sortOrder switch
            {
                "Name" => collections.OrderBy(c => c.Name).ToList(),
                "Name_Desc" => collections.OrderByDescending(c => c.Name).ToList(),
                "Description" => collections.OrderBy(c => c.Description).ToList(),
                "Description_Desc" => collections.OrderByDescending(c => c.Description).ToList(),
                "Era" => collections.OrderBy(c => c.Era.Name).ToList(),
                "Era_Desc" => collections.OrderByDescending(c => c.Era.Name).ToList(),
                "Location" => collections.OrderBy(c => c.Location.Name).ToList(),
                "Location_Desc" => collections.OrderByDescending(c => c.Location.Name).ToList(),
                _ => collections
            };

            var model = new FilteredCollectionsViewModel
            {
                Collections = collections,
                SortOrder = sortOrder ?? "",
                LocationOptions = (await userService.GetLocationsAsync())
                    .Select(l => new SelectListItem { Text = l.Name, Value = l.Id.ToString() }).ToList(),
                EraOptions = (await userService.GetErasAsync())
                    .Select(e => new SelectListItem { Text = e.Name, Value = e.Id.ToString() }).ToList(),
                AssociateOptions = (await userService.GetAssociatesAsync())
                    .Select(a => new SelectListItem { Text = a.FirstName, Value = a.Id }).ToList()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SeeAllExhibitsByCollections()
        {
            var collections = await userService.GetAllCollectionsAsync();

            return View(collections);
        }

        [HttpGet]
        public async Task<IActionResult> SeeAllExhibitsByCollectionId(int collectionId)
        {
            var exhibits = await userService.GetAllExhibitsByCollectionIdAsync(collectionId);

            return View(exhibits);
        }

    }
}
