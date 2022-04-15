using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Exceptions.Services.CRUD;
using Bonsai.Server.Controllers.BaseControllers;
using Bonsai.Server.Controllers.BaseControllers.Traits;
using MartinDrozdik.Data.Models.Files;
using MartinDrozdik.Data.Models.Media;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Facades.Abstraction;
using MartinDrozdik.Web.Facades.Models.Media;
using MartinDrozdik.Web.Facades.Models.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MartinDrozdik.Web.Admin.Server.Controllers.Models.Media
{
    public abstract class MediaBaseController<TMedia> : BaseApiController<TMedia, int>
        where TMedia : MediaBase
    {
        readonly MediaBaseFacade<TMedia> facade;

        public MediaBaseController(MediaBaseFacade<TMedia> facade)
            : base(facade)
        {
            this.facade = facade;
        }

        [HttpPost("{id}/media")]
        public async Task<ActionResult> UploadMediaAsync(int id, [FromBody] UploadFileData file)
        {
            if (file is null)
                return BadRequest("File not delivered");

            try
            {
                var media = await facade.GetAsync(id);
                await facade.AddMediaAsync(media, file.Bytes, file.FileName);
                return Ok();
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

        [HttpDelete("{id}/media")]
        public async Task<ActionResult> DeleteMediaAsync(int id)
        {
            try
            {
                var media = await facade.GetAsync(id);
                await facade.DeleteMediaAsync(media);
                return Ok();
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
