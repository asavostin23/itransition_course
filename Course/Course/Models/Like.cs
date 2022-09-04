namespace Course.Models
{
    public class Like
    {
        public string UserId { get; set; }
        public int ItemId { get; set; }
        public User User { get; set; }
        public Item Item { get; set; }
    }
}
