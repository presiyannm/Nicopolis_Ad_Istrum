using Microsoft.AspNetCore.Mvc.Rendering;

namespace Nicopolis_Ad_Istrum.Models.ViewModels
{
    public class FilteredCollectionsViewModel
    {
        public List<Collection> Collections { get; set; } = new();

        public string SortOrder { get; set; } = string.Empty;

        public int? LocationId { get; set; }
        public int? EraId { get; set; }
        public string? AssociateId { get; set; }

        public List<SelectListItem> LocationOptions { get; set; } = new();
        public List<SelectListItem> EraOptions { get; set; } = new();
        public List<SelectListItem> AssociateOptions { get; set; } = new();
    }
}
