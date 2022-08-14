// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Course.Areas.Identity.Pages.Account.Manage
{
    public class DownloadPersonalDataModel : PageModel
    {

        public IActionResult OnGet()
        {
            return RedirectToPage("Email");
        }

        public IActionResult OnPostAsync()
        {
            return RedirectToPage("Email");
        }
    }
}
