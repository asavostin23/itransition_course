using System.ComponentModel.DataAnnotations;

namespace Course.Models
{
    public class CollectionViewModel
    {
        [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(Resources.ErrorMessageResource))]
        [Display(Name = "Name", ResourceType = typeof(Resources.DisplayNameResource))]
        public string Name { get; set; }
        [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(Resources.ErrorMessageResource))]
        [Display(Name = "Description", ResourceType = typeof(Resources.DisplayNameResource))]
        public string Description { get; set; }
        [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(Resources.ErrorMessageResource))]
        [Display(Name = "Theme", ResourceType = typeof(Resources.DisplayNameResource))]
        public string Theme { get; set; }
        public byte[]? ImageData { get; set; }
    }
}
