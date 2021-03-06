using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bonsai.Services.LanguageDictionary.Abstraction;
using MartinDrozdik.Data.Models.Blog;
using MartinDrozdik.Data.Models.CV;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Configuration;

namespace MartinDrozdik.Web.Views.Home
{
    public class BlogPageModel : ViewModelBase
    {
        public IEnumerable<ArticleTag> ArticleTags { get; }

        public BlogPageModel(
            ServerConfiguration serverConfiguration,
            ICultureProvider cultureProvider, 
            ILanguageDictionary languageDictionary,
            IEnumerable<ArticleTag> articleTags)
            : base(serverConfiguration, cultureProvider, languageDictionary)
        {
            ArticleTags = articleTags;
        }

        

        public override string Description => "Rád píšu články a sdílím tak vědomosti. Mnohdy se při jejich tvorbě i něco dozvím a upřesním. Kromě výukových materiálů na programování uvažuji o věcech jako je podnikání, práce a štěstí v životě.";

        public override string PageTitle => "Blog";

        public override IEnumerable<string> KeywordsList => new string[] { 
            "Martin Drozdík",
            "blog",
            "články",
            "papery",
            "zajímavosti",
            "aktuality",
        };

        public override string OgImage => $"{ServerConfiguration.Domain}/Web/_Pages/BlogPage/BlogPageOG.png";

        public override PageId PageId => PageId.Blog;
    }
}
