namespace Course.Models
{
    public class SearchViewModel
    {
        public List<Item> Items { get; set; } = new();
        public List<Collection> Collections { get; set; } = new();
        public List<User> Users { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();
        public SearchViewModel(List<Item> items, List<Collection> collections, List<User> users, List<Tag> tags)
        {
            Items = items;
            Collections = collections;
            Users = users;
            Tags = tags;
        }
    }
}
