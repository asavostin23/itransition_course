using Course.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace Course.Areas.Identity.Pages.Account.Manage
{
    public class PreferencesModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IOptions<RequestLocalizationOptions> _localizationOptions;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        public SelectList Languages { get; set; }
        public SelectList Themes { get; set; }
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(Resources.ErrorMessageResource))]
            [DataType(DataType.Text)]
            [Display(Name = "Language", ResourceType = typeof(Resources.DisplayNameResource))]
            public string Language { get; set; }

            [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(Resources.ErrorMessageResource))]
            [DataType(DataType.Text)]
            [Display(Name = "Theme", ResourceType = typeof(Resources.DisplayNameResource))]
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
            _localizationOptions = localizationOptions;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(_sharedLocalizer["Unable to load user with ID"] + _userManager.GetUserId(User) + ".");
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
                return NotFound(_sharedLocalizer["Unable to load user with ID"] + _userManager.GetUserId(User) + ".");
            }
            user.Language = Input.Language;
            user.Theme = Input.Theme;
            await _userManager.UpdateAsync(user);

            HttpContext.Response.Cookies.Append("Theme", user.Theme,
                        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return RedirectToAction("SetLanguage", "Home", new { culture = user.Language, returnUrl = HttpContext.Request.Path });
        }
    }
}
