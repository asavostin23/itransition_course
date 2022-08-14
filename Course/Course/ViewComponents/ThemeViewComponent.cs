using Microsoft.AspNetCore.Mvc;

namespace Course.ViewComponents
{
    public class ThemeViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            if (Request.Cookies.ContainsKey("Theme") && Request.Cookies["Theme"] == "dark")
                return View("Dark");
            return View("Light");
        }
    }
}
