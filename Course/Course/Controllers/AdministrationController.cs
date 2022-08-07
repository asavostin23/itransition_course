using Course.Data;
using Course.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Course.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public AdministrationController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager) : base()
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            List<UserViewModel> usersModel = _userManager.Users
                .Select(u => new UserViewModel(u.Id, u.UserName, u.Email, u.Active, u.Language, u.Theme, false))
                .ToList();
            foreach (UserViewModel userModel in usersModel)
                userModel.IsAdmin =
                    await _userManager.IsInRoleAsync(await _userManager.FindByIdAsync(userModel.Id), "Admin");
            return View(usersModel);
        }
    }
}

