using Microsoft.AspNetCore.Identity;

namespace Course.Models
{
    public class User : IdentityUser
    {
        public bool? Active { get; set; }

        public ICollection<Collection> Collections { get; set; }
        public ICollection<Item> Items { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public User()
        {
            Active = true;
        }
    }
}
