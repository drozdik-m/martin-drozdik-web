using System;
using System.Collections.Generic;
using System.Text;
using Bonsai.RazorPages.User.Services.LanguageDictionary;
using Bonsai.Services.LanguageDictionary.Static.Languages;

namespace Bonsai.RazorPages.Error.Services.LanguageDictionary.Languages
{
    public class EnglishUserLanguageDictionary : EnglishStaticLanguageDictionary, IUserLanguageDictionary
    {

        public EnglishUserLanguageDictionary()
            :base(dictionary)
        {

        }

        public static readonly Dictionary<string, string> dictionary = new()
        {
            { UserLanguageDictionary.RegisterHeading, "Registration" },
            { UserLanguageDictionary.RegisterButton, "Register" },
            { UserLanguageDictionary.LoginHeading, "Login" },
            { UserLanguageDictionary.LoginButton, "Login" },
            { UserLanguageDictionary.Email, "Email" },
            { UserLanguageDictionary.EmailPlaceholder, "john@smith.com" },
            { UserLanguageDictionary.EmailFormatError, "Incorrect email format" },
            { UserLanguageDictionary.EmailNotEmpty, "Email is required" },
            { UserLanguageDictionary.Password, "Password" },
            { UserLanguageDictionary.PasswordNotEmpty, "Password is required" },
            { UserLanguageDictionary.PasswordConfirm, "Password confirmation" },
            { UserLanguageDictionary.PasswordConfirmSame, "Passwords must be the same" },
            { UserLanguageDictionary.DeniedHeading, "Access denied" },
            { UserLanguageDictionary.DeniedText, "You do not have access to this resource" },
            { UserLanguageDictionary.ReturnButton, "Back" },
            { UserLanguageDictionary.WebButton, "To the web" },
            { UserLanguageDictionary.ChangePasswordHeading, "Password change" },
            { UserLanguageDictionary.ChangePasswordButton, "Change password" },
            { UserLanguageDictionary.OldPassword, "Old password" },
            { UserLanguageDictionary.OldPasswordNotEmpty, "Old password is required" },
            { UserLanguageDictionary.NewPasswordLabel, "New password" },
            { UserLanguageDictionary.NewPasswordNotEmpty, "New password is required" },
            { UserLanguageDictionary.ConfirmNewPasswordLabel, "New password confirmation" },
            { UserLanguageDictionary.ConfirmNewPasswordSame, "Passwords must match" },
            { UserLanguageDictionary.LogoutSuccessHeading, "Log out successful" },
            { UserLanguageDictionary.LogoutSuccessText, "You are now safely logged out of your account" },
        };
    }
}
