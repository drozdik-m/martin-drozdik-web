using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.Markdown;

namespace MartinDrozdik.Data.Models.Projects
{
    public class ProjectMarkdownArticle : MarkdownArticle
    {
        public override string ImagesFolderPath => $"AppData/ProjectMarkdownArticleImages/{Id}";
    }
}
