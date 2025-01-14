using Nicopolis_Ad_Istrum.Models.Constants;
using Nicopolis_Ad_Istrum.Models.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nicopolis_Ad_Istrum.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int EraId { get; set; }

        [ForeignKey(nameof(EraId))]
        public Era Era { get; set; }

        public int LocationId { get; set; }

        [ForeignKey(nameof(LocationId))]
        public Location Location { get; set; }

        public List<Exhibit> Exhibits { get; set; } = new();

        public string ApplicationUserId { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; } = null!;
    }
}
