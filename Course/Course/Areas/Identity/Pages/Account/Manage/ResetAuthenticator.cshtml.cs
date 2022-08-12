// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Course.Models;

namespace Course.Areas.Identity.Pages.Account.Manage
{
    public class ResetAuthenticatorModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            return RedirectToPage("Email");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return RedirectToPage("Email");
        }
    }
}
