﻿using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Xml.Linq;

namespace Course.Models
{
    public class CollectionEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(Resources.ErrorMessageResource))]
        [Display(Name = "Name", ResourceType = typeof(Resources.DisplayNameResource))]
        [StringLength(60, ErrorMessageResourceName = "MinMaxLengthError", ErrorMessageResourceType = typeof(Resources.ErrorMessageResource), MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.DisplayNameResource))]
        [StringLength(300, ErrorMessageResourceName = "MaxLengthError", ErrorMessageResourceType = typeof(Resources.ErrorMessageResource))]
        public string? Description { get; set; }

        [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(Resources.ErrorMessageResource))]
        [Display(Name = "Theme", ResourceType = typeof(Resources.DisplayNameResource))]
        public string Theme { get; set; }

        public IFormFile? ImageData { get; set; }        
    }
}
