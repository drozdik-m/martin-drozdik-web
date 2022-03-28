using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.Authentication;
using MartinDrozdik.Data.Models.UserIdentity;
using MartinDrozdik.Web.Admin.Server.Models.User;
using MartinDrozdik.Web.Facades.User;
using MartinDrozdik.Web.Facades.User.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MartinDrozdik.Web.Admin.Server.Controllers
{

    public class UserController : Controller
    {
        readonly UserManager<AppUser> userManager;

        readonly UserFacade facade;

        public UserController(UserFacade facade,
            UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
            this.facade = facade;
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserForm form)
        {
            try
            {
                await facade.LoginAsync(form.Email, form.Password);
                if (!string.IsNullOrEmpty(form.ReturnUrl))
                    return LocalRedirect(form.ReturnUrl);
                return LocalRedirect("/");
            }
            catch (UserException e)
            {
                TempData["ErrorMessages"] = e.Errors.ToList();
                TempData["UserIdentityEmail"] = form.Email;
                if (string.IsNullOrEmpty(form.ReturnUrl))
                    return LocalRedirect("/user/login");
                return LocalRedirect($"/user/login?returnUrl={form.ReturnUrl}");
            }
        }

        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRoles.Developer + "," + UserRoles.Admin)]
        public async Task<IActionResult> Register(CreateUserForm form)
        {
            try
            {
                await facade.RegisterAsync(form.Email, form.Password, form.ConfirmPassword);
                if (!string.IsNullOrEmpty(form.ReturnUrl))
                    return LocalRedirect(form.ReturnUrl);
                return LocalRedirect("/");
            }
            catch (UserException e)
            {
                TempData["ErrorMessages"] = e.Errors.ToList();
                TempData["UserIdentityEmail"] = form.Email;

                if (string.IsNullOrEmpty(form.ReturnUrl))
                    return LocalRedirect("/user/register");
                return LocalRedirect($"/user/register?returnUrl={form.ReturnUrl}");
            }
        }

        [Authorize]
        [HttpPost("change-password")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordUserForm form)
        {
            try
            {
                var userName = User?.Identity?.Name;
                if (userName == null)
                    throw new Exception("Could not retrieve the users' name");

                await facade.ChangePassword(userName, form.OldPassword, form.NewPassword, form.ConfirmNewPassword);
                if (!string.IsNullOrEmpty(form.ReturnUrl))
                    return LocalRedirect(form.ReturnUrl);
                return LocalRedirect("/");
            }
            catch (UserException e)
            {
                TempData["ErrorMessages"] = e.Errors.ToList();

                if (string.IsNullOrEmpty(form.ReturnUrl))
                    return LocalRedirect("/user/change-password");
                return LocalRedirect($"/user/change-password?returnUrl={form.ReturnUrl}");
            }
        }

        [Authorize]
        [HttpPost("/logout")]
        public async Task<IActionResult> LogOutAsync()
        {
            await facade.LogOutAsync();
            return LocalRedirect("/user/confirm-logout");
        }
    }
}
