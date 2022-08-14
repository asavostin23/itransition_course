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
        public AdministrationController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
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
        public async Task<IActionResult> ToggleActive(string id)
        {
            if (id != null)
            {
                User user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    user.Active = !user.Active;
                    await _userManager.UpdateAsync(user);
                }
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ToggleAdmin(string id)
        {
            if (id != null)
            {
                User user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                        await _userManager.RemoveFromRoleAsync(user, "Admin");
                    else
                        await _userManager.AddToRoleAsync(user, "Admin");
                    await _userManager.UpdateAsync(user);
                }
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id != null)
            {
                User user = await _userManager.FindByIdAsync(id);
                if (user != null)
                    await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}

