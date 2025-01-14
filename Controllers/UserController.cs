using Microsoft.AspNetCore.Mvc;
using Nicopolis_Ad_Istrum.Interfaces;

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
        public async Task<IActionResult> SeeAllCollections()
        {
            var collections = await userService.GetAllCollectionsAsync();

            return View(collections);
        }
    }
}
