namespace Course.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public string Language { get; set; }
        public string Theme { get; set; }
        public bool IsAdmin { get; set; }
        public UserViewModel
            (string id,
            string username,
            string email,
            bool active,
            string language,
            string theme,
            bool isAdmin)
        {
            Id = id;
            Username = username;
            Email = email;
            Active = active;
            Language = language;
            Theme = theme;
            IsAdmin = isAdmin;
        }
    }

}
