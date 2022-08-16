using System.ComponentModel.DataAnnotations;

namespace Course.Models
{
    public class CollectionNewViewModel
    {
        [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(Resources.ErrorMessageResource))]
        [Display(Name = "Name", ResourceType = typeof(Resources.DisplayNameResource))]
        public string Name { get; set; }
        [Display(Name = "Description", ResourceType = typeof(Resources.DisplayNameResource))]
        public string Description { get; set; }
        [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(Resources.ErrorMessageResource))]
        [Display(Name = "Theme", ResourceType = typeof(Resources.DisplayNameResource))]
        public string Theme { get; set; }
        public string[] FieldTypes { get; set; } = new string[3];
        [Display(Name = "FieldName", ResourceType = typeof(Resources.DisplayNameResource))]
        public string[] FieldNames { get; set; } = new string[3];
        public IFormFile? ImageData { get; set; }
    }
}
