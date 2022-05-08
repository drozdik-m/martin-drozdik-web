using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction;
using Bonsai.Models.Abstraction.Services;
using MartinDrozdik.Abstraction.Entities;
using MartinDrozdik.Abstraction.Services;
using MartinDrozdik.Data.Repositories.Abstraction;
using MartinDrozdik.Web.Facades.Abstraction;
using MartinDrozdik.Web.Facades.Traits.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace MartinDrozdik.Web.Facades.Traits
{
    interface ISeedableFacadeTrait : ISeedableFacade
    {
        protected abstract ISeedableService SeedableService { get; }

        public Task TSeedAsync() => SeedableService.SeedAsync();
    }
}
