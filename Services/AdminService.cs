using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nicopolis_Ad_Istrum.Data;
using Nicopolis_Ad_Istrum.Interfaces;
using Nicopolis_Ad_Istrum.Models.Helpers;
using Nicopolis_Ad_Istrum.Models.Identity;

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

            // Apply sorting based on the selected field and order
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
    }
}

