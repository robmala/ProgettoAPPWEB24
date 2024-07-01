// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgettoAPPWEB24.Areas.Identity.Data;
using ProgettoAPPWEB24.Models;

namespace ProgettoAPPWEB24.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ProgettoAPPWEB24User> _userManager;
        private readonly SignInManager<ProgettoAPPWEB24User> _signInManager;

        public IndexModel(
            UserManager<ProgettoAPPWEB24User> userManager,
            SignInManager<ProgettoAPPWEB24User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Nome")]
            public string Nome { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Cognome")]
            public string Cognome { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Indirizzo")]
            public string Indirizzo { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Citta")]
            public string Citta { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "CAP")]
            public string CAP { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Provincia")]
            public string Provincia { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Role")]
            public string Role { get; set; }
        }

        private async Task LoadAsync(ProgettoAPPWEB24User user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                Nome = user.Nome,
                Cognome = user.Cognome,
                Indirizzo = user.Indirizzo,
                Citta = user.Citta,
                CAP = user.CAP,
                Provincia = user.Provincia,
                PhoneNumber = phoneNumber,
                Role = user.Role
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if(Input.Nome != user.Nome)
            {
                user.Nome = Input.Nome;
            }
            if(Input.Cognome != user.Cognome)
            {
                user.Cognome = Input.Cognome;
            }
            if(Input.Indirizzo != user.Indirizzo)
            {
                user.Indirizzo = Input.Indirizzo;
            }
            if(Input.Citta != user.Citta)
            {
                user.Citta = Input.Citta;
            }
            if(Input.CAP != user.CAP)
            {
                user.CAP = Input.CAP;
            }
            if(Input.Provincia != user.Provincia)
            {
                user.Provincia = Input.Provincia;
            }
            if(Input.Role != user.Role)
            {
                user.Role = Input.Role;
            }

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
