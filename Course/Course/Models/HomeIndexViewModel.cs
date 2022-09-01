using System.Text;

namespace Course.Models
{
    public class HomeIndexViewModel
    {
        public List<Collection> TopCollections { get; set; } = new();
        public List<Item> LastAddedItems { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();
        public string GetTagNames()
        {
            return "[" +
                string.Join(",", Tags.Select(tag => "\"" + tag.Name + "\"").ToArray())
                + "]";
        }
    }
}
