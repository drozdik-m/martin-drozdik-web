using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Exceptions.Services.CRUD;
using Bonsai.Server.Controllers.BaseControllers;
using Bonsai.Server.Controllers.BaseControllers.Traits;
using MartinDrozdik.Data.Models.Files;
using MartinDrozdik.Data.Models.Markdown;
using MartinDrozdik.Data.Models.Markdown.Media;
using MartinDrozdik.Data.Models.Media;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Facades.Abstraction;
using MartinDrozdik.Web.Facades.Models.Markdown;
using MartinDrozdik.Web.Facades.Models.Media;
using MartinDrozdik.Web.Facades.Models.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MartinDrozdik.Web.Admin.Server.Controllers.Models.Markdown
{
    public class MarkdownArticleController<TArticle> : BaseApiController<TArticle, int>
        where TArticle : MarkdownArticle
    {
        readonly MarkdownArticleFacade<TArticle> facade;

        public MarkdownArticleController(MarkdownArticleFacade<TArticle> facade)
            : base(facade)
        {
            this.facade = facade;
        }

        [HttpPost("{id}/file")]
        public async Task<ActionResult<AddFileResponse>> UploadMediaAsync(int id, [FromBody] UploadFileData file)
        {
            if (file is null)
                return BadRequest("File not delivered");

            try
            {
                var article = await facade.GetAsync(id);
                var result = await facade.AddFileAsync(article, file.Bytes, file.FileName);
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("{id}/image")]
        public Task<ActionResult<AddImageResponse>> UploadRegularImageAsync(int id, [FromBody] UploadFileData file)
            => UploadImageAsync(id, file, ImageSize.Regular);

        [HttpPost("{id}/textwidth-image")]
        public Task<ActionResult<AddImageResponse>> UploadTextWidthImageAsync(int id, [FromBody] UploadFileData file)
            => UploadImageAsync(id, file, ImageSize.TextWidth);

        [HttpPost("{id}/wide-image")]
        public Task<ActionResult<AddImageResponse>> UploadWideImageAsync(int id, [FromBody] UploadFileData file)
            => UploadImageAsync(id, file, ImageSize.Wide);

        protected virtual async Task<ActionResult<AddImageResponse>> UploadImageAsync(int id, UploadFileData file, ImageSize imageSize)
        {
            if (file is null)
                return BadRequest("File not delivered");

            try
            {
                var article = await facade.GetAsync(id);
                var result = await facade.AddImageAsync(article, imageSize, file.Bytes, file.FileName);
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
