using Microsoft.AspNetCore.Identity;

namespace Course.Models
{
    public class User : IdentityUser
    {
        public bool Active { get; set; } = true;
        public string Language { get; set; } = "en";
        public string Theme { get; set; } = "light";

        public ICollection<Collection> Collections { get; set; }
        public ICollection<Item> Items { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
