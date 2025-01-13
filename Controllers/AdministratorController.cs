using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nicopolis_Ad_Istrum.Interfaces;
using Nicopolis_Ad_Istrum.Models.Helpers;
using Nicopolis_Ad_Istrum.Models.Identity;
using Nicopolis_Ad_Istrum.Models.ViewModels;
using Nicopolis_Ad_Istrum.Services;

namespace Nicopolis_Ad_Istrum.Controllers
{
    [Authorize(Roles=("Administrator"))]
    public class AdministratorController : Controller
    {
        private IAdminService adminService;

        private UserManager<ApplicationUser> userManager;
        public AdministratorController(IAdminService adminService, UserManager<ApplicationUser> userManager)
        {
            this.adminService = adminService;
            this.userManager = userManager;
        }
        public async Task<IActionResult> SeeAllUsers(int page = 1, int pageSize = 2, string sortBy = "FirstName", bool ascending = true)
        {
            var users = await adminService.GetApplicationUsersAsync(page, pageSize, sortBy, ascending);

            var totalUsers = await adminService.GetTotalUsersCountAsync();

            ViewData["SortBy"] = sortBy;
            ViewData["SortOrder"] = ascending;
            ViewData["SortOrderFirstName"] = sortBy == "FirstName" ? ascending : (bool?)null;
            ViewData["SortOrderLastName"] = sortBy == "LastName" ? ascending : (bool?)null;

            var model = new PaginatedList<ApplicationUser>(users, totalUsers, page, pageSize);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditUserById(string userId)
        {
            var model = await adminService.GetUserViewModelAsync(userId);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserById(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                await adminService.UpdateUserByIdAsync(model);

                return RedirectToAction("SeeAllUsers");
            }

            return View(model);
        }

    }
}
