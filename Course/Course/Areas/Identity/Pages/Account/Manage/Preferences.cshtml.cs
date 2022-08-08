using Course.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Course.Areas.Identity.Pages.Account.Manage
{
    public class PreferencesModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public SelectList Languages { get; set; }
        public SelectList Themes { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Language")]
            public string Language { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Theme")]
            public string Theme { get; set; }
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public PreferencesModel(UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            Languages = new SelectList(new List<string> { "en", "pl" }, user.Language);
            Themes = new SelectList(new List<string> { "light", "dark" }, user.Theme);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            user.Language = Input.Language;
            user.Theme = Input.Theme;
            await _userManager.UpdateAsync(user);
            return RedirectToPage();
        }
    }
}
