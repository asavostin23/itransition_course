using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Xml.Linq;

namespace Course.Models
{
    public class CommentItemViewModel
    {
        public int ItemId { get; set; }
        [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(Resources.ErrorMessageResource))]
        [Display(Name = "CommentBody", ResourceType = typeof(Resources.DisplayNameResource))]
        public string Text { get; set; }
    }
}
