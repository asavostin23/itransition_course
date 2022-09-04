using Course.Data;
using Course.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Course.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        public LikeController(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        [HttpGet("{id}")]
        public async Task Get(int id)
        {
            Item? item = _db.Items.FirstOrDefault(item => item.Id == id);
            if (User.Identity != null && User.Identity.IsAuthenticated && item != null)
            {
                User user = await _db.Users.Where(user => user.UserName == User.Identity.Name)
                    .Include(user => user.LikedItems).FirstAsync();
                user.ToggleLikedItem(item);
                await _db.SaveChangesAsync();
            }
        }
    }
}
