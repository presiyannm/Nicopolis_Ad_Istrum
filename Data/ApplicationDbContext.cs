using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nicopolis_Ad_Istrum.Models;
using Nicopolis_Ad_Istrum.Models.Constants;
using Nicopolis_Ad_Istrum.Models.Identity;
using System.Collections.ObjectModel;

namespace Nicopolis_Ad_Istrum.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Era>().HasData(
                new Era { Id = 1, Name = "Праистория"},
                new Era { Id = 2, Name = "Древност" },
                new Era { Id = 3, Name = "Античност" },
                new Era { Id = 4, Name = "Средновековие" },
                new Era { Id = 5, Name = "Ново Време" },
                new Era { Id = 6, Name = "Съвременна епоха" }
                );

            modelBuilder.Entity<Location>().HasData(
                new Location { Id = 1, Name = "Склад" },
                new Location { Id = 2, Name = "Зала за реставрация" },
                new Location { Id = 3, Name = "Изложбена зала" }
                );

            modelBuilder.Entity<Acquisition>().HasData(
                new Location { Id = 1, Name = "Поръчка" },
                new Location { Id = 2, Name = "Археологическо откритие" },
                new Location { Id = 3, Name = "Дарение" }
                );
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Exhibit> Exhibits { get; set; }
        public DbSet<Acquisition> Acquisitions { get; set; }
        public DbSet<Era> Eras { get; set; }
        public DbSet<Location> Locations { get; set; }

    }
}
