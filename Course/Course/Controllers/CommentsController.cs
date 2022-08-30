using Course.Data;
using Course.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Course.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public CommentsController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            List<Comment> itemComments = await _db.Comments.Where(comment => comment.ItemId == id).ToListAsync();
            await _db.Users.Where(user => itemComments.Select(comment => comment.UserId).Contains(user.Id)).LoadAsync();
            return new JsonResult(itemComments.Select(comment => new
            {
                Id = comment.Id,
                UserId = comment.UserId,
                ItemId = comment.ItemId,
                Text = comment.Text,
                CreatedDate = comment.CreatedDate.ToString("r"),
                UserName = comment.User.UserName
            }));
        }
    }
}
