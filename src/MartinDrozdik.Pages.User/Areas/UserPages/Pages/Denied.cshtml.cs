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

        public string ReturnButton => LanguageDictionary.GetContent(UserLanguageDictionary.ReturnButton);
        public string WebButton => LanguageDictionary.GetContent(UserLanguageDictionary.WebButton);
        public string LoginButton => LanguageDictionary.GetContent(UserLanguageDictionary.LoginButton);

        public string DeniedText => LanguageDictionary.GetContent(UserLanguageDictionary.DeniedText);

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl = "")
        {
            ReturnUrl = returnUrl;
        }
    }
}
