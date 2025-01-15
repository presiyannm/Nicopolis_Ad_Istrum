using Nicopolis_Ad_Istrum.Models.Constants;
using Nicopolis_Ad_Istrum.Models.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nicopolis_Ad_Istrum.Models
{
    public class Exhibit
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string PhotoFileName { get; set; } = string.Empty;

        public string Origin { get; set; } = string.Empty;

        public int EraId { get; set; }

        [ForeignKey(nameof(EraId))]
        public Era Era { get; set; } = null!;
        public int CollectionId { get; set; }

        [ForeignKey(nameof(CollectionId))]
        public Collection Collection { get; set; } = null!;

        public int AcquisitionId { get; set; }

        [ForeignKey(nameof(AcquisitionId))]
        public Acquisition Acquisition { get; set; } = null!;

        public int LocationId { get; set; }

        [ForeignKey(nameof(LocationId))]
        public Location Location { get; set; } = null!;

        public string ApplicationUserId { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; } = null!;

    }
}
