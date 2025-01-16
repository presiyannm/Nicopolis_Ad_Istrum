using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nicopolis_Ad_Istrum.Data;
using Nicopolis_Ad_Istrum.Interfaces;
using Nicopolis_Ad_Istrum.Models;
using Nicopolis_Ad_Istrum.Models.Identity;
using Nicopolis_Ad_Istrum.Models.ViewModels;

namespace Nicopolis_Ad_Istrum.Services
{
    public class EmployeeService : IEmployeeService
    {
        private ApplicationDbContext dbContext;
        public EmployeeService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddEventAsync(AddEventViewModel viewModel)
        {
            var user = await dbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == viewModel.ApplicationUserId);

            if (user == null)
            {
                throw new Exception("Employee cannot be null");
            }
            var eventToAdd = new Event()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Date = viewModel.Date,
                ApplicationUserId = viewModel.ApplicationUserId,
                ApplicationUser = user
            };

            dbContext.Events.Add(eventToAdd);

            await dbContext.SaveChangesAsync();

        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            var users = await dbContext.ApplicationUsers.ToListAsync();

            return users;
        }

        public async Task<Event> GetCurrentEventByIdAsync(int eventId)
        {
            var currentEvent = await dbContext.Events.FirstOrDefaultAsync(e => e.Id == eventId);

            if (currentEvent == null)
            {
                throw new Exception("Event cannot be null");
            }

            return currentEvent;
        }

        public async Task UpdateEvent(AddEventViewModel viewModel)
        {
            var currentEvent = await dbContext.Events.FirstOrDefaultAsync(e => e.Id == viewModel.Id);

            if(currentEvent == null)
            {
                throw new Exception("Event cannot be null");
            }

            currentEvent.Name = viewModel.Name;
            currentEvent.Description = viewModel.Description;
            currentEvent.Date = viewModel.Date;

            dbContext.Events.Update(currentEvent);

            await dbContext.SaveChangesAsync();
        }
    }
}
