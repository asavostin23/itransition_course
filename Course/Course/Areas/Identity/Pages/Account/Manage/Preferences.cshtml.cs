using Course.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace Course.Areas.Identity.Pages.Account.Manage
{
    public class PreferencesModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IOptions<RequestLocalizationOptions> _localizationOptions;
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
        public PreferencesModel
            (
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IOptions<RequestLocalizationOptions> localizationOptions
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _localizationOptions = localizationOptions;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            Languages = new SelectList(_localizationOptions.Value.SupportedCultures, new System.Globalization.CultureInfo(user.Language));
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
            return RedirectToAction("SetLanguage", "Home", new { culture = user.Language, returnUrl = HttpContext.Request.Path });
        }
    }
}
