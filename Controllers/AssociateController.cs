using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nicopolis_Ad_Istrum.Interfaces;
using Nicopolis_Ad_Istrum.Models.ViewModels;

namespace Nicopolis_Ad_Istrum.Controllers
{
    [Authorize(Roles = "Science Associate, Administrator")]
    public class AssociateController : Controller
    {
        private IAssociateService associateService;
        public AssociateController(IAssociateService associateService)
        {
            this.associateService = associateService;
        }

        [HttpGet]
        public async Task<IActionResult> AddCollection()
        {
            var eras = await associateService.GetAllErasAsync();

            var locations = await associateService.GetAllLocationsAsync();

            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var model = new AddCollectionViewModel
            {
                Eras = eras.Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Name
                }).ToList(),

                Locations = locations.Select(l => new SelectListItem
                {
                    Value = l.Id.ToString(),
                    Text = l.Name
                }).ToList(),

                AssociateId = currentUserId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCollection(AddCollectionViewModel viewModel)
        {
            await associateService.AddCollectionAsync(viewModel);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> AddExhibit()
        {
            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var collections = await associateService.GetAllCollectionsAsync();

            var locations = await associateService.GetAllLocationsAsync();

            var eras = await associateService.GetAllErasAsync();

            var acquisitions = await associateService.GetAllAcquisitionAsync();

            var model = new AddExhibitViewModel
            {
                Collections = collections.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList(),

                Locations = locations.Select(l => new SelectListItem
                {
                    Value = l.Id.ToString(),
                    Text = l.Name
                }).ToList(),

                Eras = eras.Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Name
                }).ToList(),

                Acquisitions = acquisitions.Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                }).ToList(),

                AssociateId = currentUserId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddExhibit(AddExhibitViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await associateService.AddExhibitAsync(viewModel);

                return RedirectToAction("Index", "Home");
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditCollectionById(int collectionId)
        {
            var collection = await associateService.GetCollectionByIdAsync(collectionId);

            var eras = await associateService.GetAllErasAsync();
            var locations = await associateService.GetAllLocationsAsync();

            var erasSelectList = eras.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Name
            }).ToList();

            var locationsSelectList = locations.Select(l => new SelectListItem
            {
                Value = l.Id.ToString(),
                Text = l.Name
            }).ToList();

            var viewModel = new AddCollectionViewModel
            {
                Id = collectionId,
                AssociateId = collection.ApplicationUserId,
                Name = collection.Name,
                Description = collection.Description,
                EraId = collection.EraId,
                LocationId = collection.LocationId,
                Eras = erasSelectList,
                Locations = locationsSelectList
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditCollectionById(AddCollectionViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                await associateService.UpdateCollection(viewModel);

                return RedirectToAction("SeeAllCollections", "User");
            }

            return View(viewModel);
        }

    }
}
