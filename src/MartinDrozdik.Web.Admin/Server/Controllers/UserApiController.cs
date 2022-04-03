using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.Authentication;
using MartinDrozdik.Data.Models.UserIdentity;
using MartinDrozdik.Web.Admin.Server.Models.User;
using MartinDrozdik.Web.Facades.User;
using MartinDrozdik.Web.Facades.User.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MartinDrozdik.Web.Admin.Server.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize(Roles = UserRoles.Developer + "," + UserRoles.Admin)]
    public class UserApiController : Controller
    {
        //TODO
    }
}
