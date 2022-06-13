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
    public class ConfirmLogoutModel : UserPagesLayoutModel
    {
        public ConfirmLogoutModel(IUserLanguageDictionary languageDictionary, IFilePathProvider pathProvider)
        {
            LanguageDictionary = languageDictionary;
            PathProvider = pathProvider;
        }

        public override string Title => LanguageDictionary.GetContent(UserLanguageDictionary.LogoutSuccessHeading);

        public override string Description => "Logout success page";

        public override string Keywords => "Logout success";

        public string Heading => LanguageDictionary.GetContent(UserLanguageDictionary.LogoutSuccessHeading);

        public string WebButton => LanguageDictionary.GetContent(UserLanguageDictionary.WebButton);
        public string LoginButton => LanguageDictionary.GetContent(UserLanguageDictionary.LoginButton);

        public string LogoutSuccessText => LanguageDictionary.GetContent(UserLanguageDictionary.LogoutSuccessText);
    }
}
