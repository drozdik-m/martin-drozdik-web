using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.UserIdentity;
using MartinDrozdik.Web.Facades.User.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace MartinDrozdik.Web.Facades.User
{
    public class UserFacade
    {
        readonly UserManager<AppUser> userManager;

        readonly SignInManager<AppUser> signInManager;

        const string EMAIL_REGEX = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        public UserFacade(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        /// <summary>
        /// Logs in a user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="UserException"></exception>
        public async Task LoginAsync(string email, string password)
        {
            var signInResult = await signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);

            if (!signInResult.Succeeded)
                throw new UserException(new string[] { "Login failed. Please check your email and password." });
        }

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="passwordConfirmation"></param>
        /// <returns></returns>
        /// <exception cref="UserException"></exception>
        public async Task RegisterAsync(string email, string password, string passwordConfirmation)
        {
            //Validate
            if (password != passwordConfirmation)
                throw new UserException(new string[] { "Passwords don't match" });

            //Email check
            var emailRegex = new Regex(EMAIL_REGEX, RegexOptions.IgnoreCase);
            if (string.IsNullOrWhiteSpace(email) || !emailRegex.IsMatch(email))
                throw new UserException(new string[] { "The email has wrong format" });

            //Create
            var user = new AppUser { UserName = email, Email = email };
            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new UserException(result.Errors.Select(e => e.Description));
        }

        /// <summary>
        /// Logs out the current user
        /// </summary>
        /// <returns></returns>
        public async Task LogOutAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
