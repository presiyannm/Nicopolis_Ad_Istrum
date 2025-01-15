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

        public async Task<PaginatedList<ApplicationUser>> GetApplicationUsersAsync(
            int pageIndex,
            int pageSize,
            string sortBy,
            bool ascending,
            string searchTerm)
        {
            var query = dbContext.ApplicationUsers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(u => u.FirstName.Contains(searchTerm) || u.LastName.Contains(searchTerm));
            }

            var usersWithRoles = await query
                .Select(u => new
                {
                    User = u,
                    Position = userManager.GetRolesAsync(u).Result.FirstOrDefault() ?? "No Role"
                })
                .ToListAsync();

            switch (sortBy.ToLower())
            {
                case "firstname":
                    usersWithRoles = ascending
                        ? usersWithRoles.OrderBy(u => u.User.FirstName).ToList()
                        : usersWithRoles.OrderByDescending(u => u.User.FirstName).ToList();
                    break;
                case "lastname":
                    usersWithRoles = ascending
                        ? usersWithRoles.OrderBy(u => u.User.LastName).ToList()
                        : usersWithRoles.OrderByDescending(u => u.User.LastName).ToList();
                    break;
                case "position":
                    usersWithRoles = ascending
                        ? usersWithRoles.OrderBy(u => u.Position).ToList()
                        : usersWithRoles.OrderByDescending(u => u.Position).ToList();
                    break;
                default:
                    usersWithRoles = ascending
                        ? usersWithRoles.OrderBy(u => u.User.FirstName).ToList()
                        : usersWithRoles.OrderByDescending(u => u.User.FirstName).ToList();
                    break;
            }

            var users = usersWithRoles
                .Select(u =>
                {
                    u.User.Position = u.Position;
                    return u.User;
                })
                .ToList();

            var paginatedUsers = users
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PaginatedList<ApplicationUser>(paginatedUsers, users.Count, pageIndex, pageSize);
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
            user.Position = viewModel.SelectedRoles.First();

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

        public async Task DeleteCollectionByIdAsync(int collectionId)
        {
            var collection = await dbContext.Collections.FirstOrDefaultAsync(c => c.Id == collectionId);

            if (collection is null)
            {
                throw new Exception("Collection cannot be null");
            }

            dbContext.Collections.Remove(collection);

            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteExhibitByIdAsync(int exhibitId)
        {
            var exhibit = await dbContext.Exhibits.FirstOrDefaultAsync(e => e.Id == exhibitId);

            if (exhibit is null)
            {
                throw new Exception("Exhibit cannot be null");
            }

            dbContext.Exhibits.Remove(exhibit);

            await dbContext.SaveChangesAsync();
        }
    }
}

