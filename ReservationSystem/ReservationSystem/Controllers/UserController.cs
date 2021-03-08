using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Models;
using ReservationSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Controllers
{
    public class UserController : Controller
    {

        private readonly AppDbContext context;
        private readonly SignInManager<user> signInManager;
        private readonly UserManager<user> userManager;
        public UserController(AppDbContext context , SignInManager<user> signInManager, UserManager<user> userManager)
        {
            this.context = context;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }


        //Actions
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<user>>> List()
        {
            var result = await context.users.ToListAsync();
            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult<user>> Details(int id)
        {
            var user = await context.users.FindAsync(id);
            if (user is null)
            {
                return NotFound();
            }
            return View(user);
        }


        // external login

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            UserLoginViewModel model = new UserLoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return View(model);

        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }


        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            UserLoginViewModel model = new UserLoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            if (remoteError != null)
            {
                ModelState.AddModelError("", $"Error from external provider : {remoteError}");
                return View("Login", model);
            }
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError("", $"Error loading external login information");
                return View("Login", model);
            }
            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                                     info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                //var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                //if (email != null)
                //{
                //    var user = await userManager.FindByEmailAsync(email);
                //    if (user == null)
                //    {
                //        user = new AppUser
                //        {
                //            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                //            Email = info.Principal.FindFirstValue(ClaimTypes.Email)

                //        };

                //        await userManager.CreateAsync(user);
                //    }

                //    await userManager.AddLoginAsync(user, info);
                //    await signInManager.SignInAsync(user, isPersistent: false);
                //    return LocalRedirect(returnUrl);
                //}

                //ViewBag.ErrorTitle = $"Email claim not received from : {info.LoginProvider}";
                //ViewBag.ErrorMessage = "Please contact support on sana.bengannoune@gmail.com";

                return View("Error");
            }





        }








    }
}
