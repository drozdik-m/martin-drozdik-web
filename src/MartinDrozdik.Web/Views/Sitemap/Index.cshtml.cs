using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bonsai.Services.LanguageDictionary.Abstraction;
using Bonsai.Services.Sitemap.Abstraction;
using Bonsai.Sitemap;
using MartinDrozdik.Web.Configuration;

namespace MartinDrozdik.Web.Views.Sitemap
{
    public class SitemapPageModel : ViewModelBase
    {
        public ISitemapNode RootSitemapNode { get; }

        public SitemapPageModel(
            ServerConfiguration serverConfiguration,
            ICultureProvider cultureProvider, 
            ILanguageDictionary languageDictionary,
            ISitemapNode rootSitemapNode)
            : base(serverConfiguration, cultureProvider, languageDictionary)
        {
            RootSitemapNode = rootSitemapNode;
        }

        public override string Description => "Hierarchický list odkazů, z jichž se tento web skládá.";

        public override string PageTitle => "Mapa stránek";

        public override string[] KeywordsList => new string[] {
            "Martin Drozdík",
            "Mapa stránek",
            "Sitemap"
        };

        public override string OgImage => "/Web/_Pages/SitemapPage/SitemapOG.png";

        public override PageId PageId => PageId.Sitemap;

       
    }
}
