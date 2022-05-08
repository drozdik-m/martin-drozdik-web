using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction;
using Bonsai.Models.Abstraction.Services;
using Bonsai.Models.Exceptions.CRUD;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Abstraction.Exceptions.Services.CRUD;
using MartinDrozdik.Data.Models.Authentication;
using MartinDrozdik.Web.Facades.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bonsai.Server.Controllers.BaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.Developer + "," + UserRoles.Admin)]
    public class BaseApiController<TEntity, TKey> : Controller
        where TEntity : class, IIdentifiable<TKey>
    {
        readonly ICRUDFacade<TEntity, TKey> facade;

        public BaseApiController(ICRUDFacade<TEntity, TKey> facade)
        {
            this.facade = facade;
        }

        [HttpGet]
        public async Task<ActionResult<Task<IEnumerable<TEntity>>>> GetAsync()
        {
            try
            {
                var res = await facade.GetAsync();
                return Ok(res);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> GetAsync(TKey id)
        {
            try
            {
                var res = await facade.GetAsync(id);
                return Ok(res);
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

        [HttpPost]
        public async Task<ActionResult> AddAsync(TEntity item)
        {
            try
            {
                await facade.AddAsync(item);
                return Ok();
            }
            catch (AlreadyExistsException)
            {
                return BadRequest("The item already exists");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(TKey id)
        {
            try
            {
                await facade.DeleteAsync(id);
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

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(TKey id, TEntity item)
        {
            try
            {
                await facade.UpdateAsync(id, item);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (BadRequestException)
            {
                return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }


    }
}
