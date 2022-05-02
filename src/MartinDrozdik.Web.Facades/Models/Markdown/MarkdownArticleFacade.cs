using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Localization;
using Bonsai.Utils.String;
using Markdig;
using MartinDrozdik.Data.Models.Markdown;
using MartinDrozdik.Data.Models.Media;
using MartinDrozdik.Data.Models.Tags;
using MartinDrozdik.Data.Repositories.Abstraction;
using MartinDrozdik.Data.Repositories.Models.Media;
using MartinDrozdik.Data.Repositories.Models.Tags;
using MartinDrozdik.Web.Facades.Traits;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MartinDrozdik.Web.Facades.Models.Markdown
{
    public class MarkdownArticleFacade<TArticle> : ContentFacade<TArticle, int>
        where TArticle : MarkdownArticle
    {
        readonly IHostEnvironment hostEnvironment;
        readonly MarkdownArticleRepository<TArticle> repository;

        static readonly MarkdownPipeline markdownPipeline =
            new MarkdownPipelineBuilder().Build();
        //.UseAdvancedExtensions()
        /*BlockParsers = new OrderedList<BlockParser>
        {
            new ThematicBreakParser(),
            new HeadingBlockParser(),
            new QuoteBlockParser(),
            new ListBlockParser(),
            new HtmlBlockParser(),
            new FencedCodeBlockParser(),
            new IndentedCodeBlockParser(),
            new ParagraphBlockParser()
        };
        InlineParsers = new OrderedList<InlineParser>
        {
            new HtmlEntityParser(),
            new LinkInlineParser(),
            new EscapeInlineParser(),
            new EmphasisInlineParser(),
            new CodeInlineParser(),
            new AutolinkInlineParser(),
            new LineBreakInlineParser()
        };*/


        public MarkdownArticleFacade(IHostEnvironment hostEnvironment,
            MarkdownArticleRepository<TArticle> repository)
            : base(hostEnvironment, repository)
        {
            this.hostEnvironment = hostEnvironment;
            this.repository = repository;
        }

        /// <summary>
        /// Converts a markdown string into a HTML string
        /// </summary>
        /// <param name="markdown"></param>
        /// <returns></returns>
        protected virtual string MarkdownToHTML(string markdown)
        {
            return Markdig.Markdown.ToHtml(markdown, markdownPipeline);
        }

        /// <inheritdoc />
        public override async Task UpdateAsync(int id, TArticle item)
        {
            item.HTML = MarkdownToHTML(item.Markdown);
            await base.UpdateAsync(id, item);
        }

    }
}
