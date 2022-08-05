namespace Course.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ItemId { get; set; }
        public string Text { get; set; }

        public User User { get; set; }
        public Item Item { get; set; }

        public Comment(string text, string userId, int itemId)
        {
            UserId = userId;
            ItemId = itemId;
            Text = text;
        }
    }
}
