﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartinDrozdik.Web.Admin.Client.Pages
{
    [Authorize]
    public class RootPage : ComponentBase
    {
    }
}
