using Course.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Course.Controllers
{
    [Authorize]
    public class InitiationController : Controller
    {
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public InitiationController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> InitAdmin(string password)
        {
            if (String.IsNullOrWhiteSpace(password))
                return RedirectToAction("Index", "Home");
            User courseUser;
            if (await _userManager.FindByNameAsync("Course") != null)
                courseUser = await _userManager.FindByNameAsync("Course");
            else
            {
                courseUser = new User() { Email = "course.asavostin@mail.ru", UserName = "Course" };
                await _userManager.CreateAsync(courseUser, password);
            }
            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            if (!await _userManager.IsInRoleAsync(courseUser, "Admin"))
                await _userManager.AddToRoleAsync(courseUser, "Admin");
            return RedirectToAction("Index", "Home");
        }
    }
}
