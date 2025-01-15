using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Nicopolis_Ad_Istrum.Models.ViewModels
{
    public class AddExhibitViewModel
    {
        [Required]
        public string AssociateId {  get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Upload)]
        public IFormFile PhotoFileName { get; set; }

        [Required]
        public string Origin { get; set; } = string.Empty;

        [Required]
        public int EraId { get; set; }

        [Required]
        public int CollectionId { get; set; }

        [Required]
        public int AcquisitionId { get; set; }

        [Required]
        public int LocationId { get; set; }

        public ICollection<SelectListItem> Eras { get; set; } = new List<SelectListItem>();
        public ICollection<SelectListItem> Locations { get; set; } = new List<SelectListItem>();
        public ICollection<SelectListItem> Collections { get; set; } = new List<SelectListItem>();
        public ICollection<SelectListItem> Acquisitions { get; set; } = new List<SelectListItem>();
    }
}
