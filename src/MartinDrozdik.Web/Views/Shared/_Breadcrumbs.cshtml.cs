using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Views.Home;

namespace MartinDrozdik.Web.Views.Shared
{
    public class BreadcrumbsModel
    {
        public BreadcrumbsModel(IEnumerable<BreadcrumbModel> breadcrumbs)
        {
            Breadcrumbs = breadcrumbs;
        }

        public IEnumerable<BreadcrumbModel> Breadcrumbs { get; }

    }

    public class BreadcrumbModel
    {
        public BreadcrumbModel(string text, string uri, BreadcrumbType type, BreadcrumbScheme scheme)
        {
            Text = text;
            Uri = uri;
            Type = type;
            Scheme = scheme;
        }

        public string Text { get; }
        public string Uri { get; }
        public BreadcrumbType Type { get; }
        public BreadcrumbScheme Scheme { get; }

        public static string SchemeToClass(BreadcrumbScheme scheme)
        {
            return scheme switch
            {
                BreadcrumbScheme.Black => "black",
                BreadcrumbScheme.Dark => "dark",
                BreadcrumbScheme.Red => "red",
                BreadcrumbScheme.DarkRed => "darkRed",
                BreadcrumbScheme.DarkerRed => "darkerRed",
                BreadcrumbScheme.White => "white",
                _ => throw new Exception("Unknown scheme"),
            };
        }

        public static string TypeToClass(BreadcrumbType scheme)
        {
            return scheme switch
            {
                BreadcrumbType.Home => "home",
                BreadcrumbType.Projects => "projects",
                BreadcrumbType.Blog => "blog",
                BreadcrumbType.Article => "anArticle",
                _ => throw new Exception("Unknown type"),
            };
        }
    }

    public enum BreadcrumbScheme
    {
        Black,
        Dark,
        Red,
        DarkRed,
        DarkerRed,
        White,
    }
    public enum BreadcrumbType
    {
        Home,
        Projects,
        Blog,
        Article,
    }
}
