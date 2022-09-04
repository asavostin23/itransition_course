using Microsoft.AspNetCore.Identity;

namespace Course.Models
{
    public class User : IdentityUser
    {
        public bool Active { get; set; } = true;
        public string Language { get; set; } = "en";
        public string Theme { get; set; } = "light";

        public List<Collection> Collections { get; set; } = new();
        public List<Item> Items { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
        public List<Item> LikedItems { get; set; } = new();
        public List<Like> Likes { get; set; } = new();
        public void ToggleLikedItem(Item item)
        {
            if (LikedItems.Contains(item))
                LikedItems.Remove(item);
            else
                LikedItems.Add(item);
        }
    }
}
