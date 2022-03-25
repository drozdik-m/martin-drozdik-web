using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bonsai.RazorPages.User.Services.LanguageDictionary;
using MartinDrozdik.Services.FilePathProvider;

namespace Bonsai.RazorPages.User.Areas.UserPages.Pages
{
    public class DeniedModel : UserPagesLayoutModel
    {
        public DeniedModel(IUserLanguageDictionary languageDictionary, IFilePathProvider pathProvider)
        {
            LanguageDictionary = languageDictionary;
            PathProvider = pathProvider;
        }

        public override string Title => LanguageDictionary.GetContent(UserLanguageDictionary.DeniedHeading);

        public override string Description => "Denied page";

        public override string Keywords => "Denied";

        public string Heading => LanguageDictionary.GetContent(UserLanguageDictionary.DeniedHeading);

        public string Button => LanguageDictionary.GetContent(UserLanguageDictionary.LoginButton);

        public string EmailLabel => LanguageDictionary.GetContent(UserLanguageDictionary.Email);
        public string EmailPlaceholder => LanguageDictionary.GetContent(UserLanguageDictionary.EmailPlaceholder);
        public string EmailWrongFormat => LanguageDictionary.GetContent(UserLanguageDictionary.EmailFormatError);
        public string EmailNotEmpty => LanguageDictionary.GetContent(UserLanguageDictionary.EmailNotEmpty);

        public string PasswordLabel => LanguageDictionary.GetContent(UserLanguageDictionary.Password);
        public string PasswordNotEmpty => LanguageDictionary.GetContent(UserLanguageDictionary.PasswordNotEmpty);

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl = "")
        {
            ReturnUrl = returnUrl;
        }
    }
}
