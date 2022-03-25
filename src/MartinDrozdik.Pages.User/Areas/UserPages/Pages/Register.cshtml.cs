using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Bonsai.RazorPages.User.Services.LanguageDictionary;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using MartinDrozdik.Services.FilePathProvider;

namespace Bonsai.RazorPages.User.Areas.UserPages.Pages
{
    [Authorize]
    public class RegisterModel : UserPagesLayoutModel
    {
        public RegisterModel(IUserLanguageDictionary languageDictionary, IFilePathProvider pathProvider)
        {
            LanguageDictionary = languageDictionary;
            PathProvider = pathProvider;
        }

        public override string Title => LanguageDictionary.GetContent(UserLanguageDictionary.LoginHeading);

        public override string Description => "Register page";

        public override string Keywords => "Register";

        public override string OgImagePath => PathProvider.PathTo("/Images/OG/OgRegistration.jpg", StaticResources.Namespace);

        public string Heading => LanguageDictionary.GetContent(UserLanguageDictionary.LoginHeading);

        public string RegisterButton => LanguageDictionary.GetContent(UserLanguageDictionary.RegisterButton);

        public string EmailLabel => LanguageDictionary.GetContent(UserLanguageDictionary.Email);
        public string EmailPlaceholder => LanguageDictionary.GetContent(UserLanguageDictionary.EmailPlaceholder);
        public string EmailWrongFormat => LanguageDictionary.GetContent(UserLanguageDictionary.EmailFormatError);
        public string EmailNotEmpty => LanguageDictionary.GetContent(UserLanguageDictionary.EmailNotEmpty);

        public string PasswordLabel => LanguageDictionary.GetContent(UserLanguageDictionary.Password);
        public string PasswordNotEmpty => LanguageDictionary.GetContent(UserLanguageDictionary.PasswordNotEmpty);

        public string ConfirmPassword => LanguageDictionary.GetContent(UserLanguageDictionary.PasswordConfirm);
        public string ConfirmPasswordSame => LanguageDictionary.GetContent(UserLanguageDictionary.PasswordConfirmSame);

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl = "")
        {
            ReturnUrl = returnUrl;
        }
    }
}
