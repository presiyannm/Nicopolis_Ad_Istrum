using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Nicopolis_Ad_Istrum.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Degree { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Adress { get; set; } = string.Empty;

        public List<Collection> Collections { get; set; }

        public List<Exhibit> Exhibits { get; set; }

        public List<Event> Events { get; set; }

    }
}
