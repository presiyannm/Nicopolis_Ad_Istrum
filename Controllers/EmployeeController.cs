using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nicopolis_Ad_Istrum.Interfaces;
using Nicopolis_Ad_Istrum.Models.Identity;
using Nicopolis_Ad_Istrum.Models.ViewModels;

namespace Nicopolis_Ad_Istrum.Controllers
{
    [Authorize(Roles = ("Employee, Administrator"))]
    public class EmployeeController : Controller
    {
        private IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> AddEvent()
        {
            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (currentUserId == null)
            {
                throw new Exception("User cannot be null");
            }

            var viewModel = new AddEventViewModel()
            {
                ApplicationUserId = currentUserId
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(AddEventViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await employeeService.AddEventAsync(viewModel);

                return RedirectToAction("Index", "Home");
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditEventById(int eventId)
        {
            var currentEvent = await employeeService.GetCurrentEventByIdAsync(eventId);

            var viewModel = new AddEventViewModel()
            {
                Name = currentEvent.Name,
                Description = currentEvent.Description,
                Date = currentEvent.Date,
                ApplicationUserId = currentEvent.ApplicationUserId,
                Id = currentEvent.Id
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditEventById(AddEventViewModel viewModel)
        {
            await employeeService.UpdateEvent(viewModel);

            return RedirectToAction("SeeAllEvents", "User");
        }
    }
}
