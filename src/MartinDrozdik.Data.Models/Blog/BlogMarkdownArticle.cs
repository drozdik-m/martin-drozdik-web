using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.Markdown;

namespace MartinDrozdik.Data.Models.Blog
{
    public class BlogMarkdownArticle : MarkdownArticle
    {
        public override string ImagesFolderPath => $"AppData/ArticleBlogMarkdownImages/{Id}";

        public override string FilesFolderPath => $"AppData/ArticleBlogMarkdownFiles/{Id}";
    }
}
