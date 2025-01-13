using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nicopolis_Ad_Istrum.Data;
using Nicopolis_Ad_Istrum.Interfaces;
using Nicopolis_Ad_Istrum.Models.Helpers;
using Nicopolis_Ad_Istrum.Models.Identity;
using Nicopolis_Ad_Istrum.Models.ViewModels;

namespace Nicopolis_Ad_Istrum.Services
{
    public class AdminService : IAdminService
    {

        private ApplicationDbContext dbContext;
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public AdminService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<PaginatedList<ApplicationUser>> GetApplicationUsersAsync(int pageIndex, int pageSize, string sortBy, bool ascending)
        {
            var query = dbContext.ApplicationUsers.AsQueryable();

            switch (sortBy.ToLower())
            {
                case "firstname":
                    query = ascending ? query.OrderBy(u => u.FirstName) : query.OrderByDescending(u => u.FirstName);
                    break;
                case "lastname":
                    query = ascending ? query.OrderBy(u => u.LastName) : query.OrderByDescending(u => u.LastName);
                    break;
                default:
                    query = ascending ? query.OrderBy(u => u.FirstName) : query.OrderByDescending(u => u.FirstName);
                    break;
            }

            var totalCount = await query.CountAsync();

            var users = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedList<ApplicationUser>(users, totalCount, pageIndex, pageSize);
        }
        public async Task<int> GetTotalUsersCountAsync()
        {
            return await dbContext.ApplicationUsers.CountAsync();
        }
        public async Task<EditUserViewModel> GetUserViewModelAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("User cannot be null");
            }

            var userRoles = await userManager.GetRolesAsync(user);

            var allRoles = await roleManager.Roles.Select(r => r.Name).ToListAsync();

            var model = new EditUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Degree = user.Degree,
                Specialty = user.Specialty,
                City = user.City,
                Address = user.Adress,
                SelectedRoles = userRoles.ToList(),
                AllRoles = allRoles
            };

            return model;
        }
        public async Task UpdateUserByIdAsync(EditUserViewModel viewModel)
        {
            var user = await userManager.FindByIdAsync(viewModel.Id);

            if (user == null)
            {
                return;
            }

            user.FirstName = viewModel.FirstName;
            user.LastName = viewModel.LastName;
            user.Email = viewModel.Email;
            user.Degree = viewModel.Degree;
            user.Specialty = viewModel.Specialty;
            user.City = viewModel.City;
            user.Adress = viewModel.Address;

            var currentRoles = await userManager.GetRolesAsync(user);

            await userManager.RemoveFromRolesAsync(user, currentRoles);

            await userManager.AddToRolesAsync(user, viewModel.SelectedRoles);

            await userManager.UpdateAsync(user);
        }

        public async Task DeleteUserByIdAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("user cannot be null");
            }

            await userManager.DeleteAsync(user);

            await dbContext.SaveChangesAsync();
        }

    }
}

