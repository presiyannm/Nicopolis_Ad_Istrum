using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nicopolis_Ad_Istrum.Interfaces;
using Nicopolis_Ad_Istrum.Models.ViewModels;

namespace Nicopolis_Ad_Istrum.Controllers
{
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
                }).ToList()
            };

            return View(model);
        }

    }
}
