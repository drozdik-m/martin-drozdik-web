using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Text;
using Bonsai.RazorPages.User.Services.LanguageDictionary;
using MartinDrozdik.Services.FilePathProvider;

namespace Bonsai.RazorPages.User.Areas.UserPages.Pages
{
    public abstract class UserPagesLayoutModel : PageModel
    {

        public IFilePathProvider PathProvider { get; protected set; }

        public IUserLanguageDictionary LanguageDictionary { get; protected set; }

        public abstract string Title { get; }

        public abstract string Description { get; }

        public abstract string Keywords { get; }

        public abstract string OgImagePath { get; }

        public string LogoPath => PathProvider.PathTo("/Web/Global/OG/Logo.png", StaticResources.Namespace);
    }
}
