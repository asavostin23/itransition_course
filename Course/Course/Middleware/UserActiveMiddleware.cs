using Course.Models;
using Microsoft.AspNetCore.Identity;

namespace Course.Middleware
{
    public class UserActiveMiddleware
    {
        private readonly RequestDelegate _next;

        public UserActiveMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<User> userManager)
        {
            User currentUser = await userManager.GetUserAsync(context.User);
            if (currentUser != null && !currentUser.Active
                && context.Request.Path != "/Identity/Account/Lockout"
                && context.Request.Path != "/Identity/Account/Logout")
                context.Response.Redirect("/Identity/Account/Lockout");
            else
                await _next.Invoke(context);
        }
    }
}