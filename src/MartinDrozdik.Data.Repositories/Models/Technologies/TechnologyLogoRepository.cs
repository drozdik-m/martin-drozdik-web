using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using MartinDrozdik.Data.Repositories.Traits;
using MartinDrozdik.Data.Models.Media;
using MartinDrozdik.Data.Models.Media.Images;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Technologies;
using MartinDrozdik.Data.Repositories.Models.Media.Images;
using MartinDrozdik.Data.DbContexts;

namespace MartinDrozdik.Data.Repositories.Models.Technologies
{
    public class TechnologyLogoRepository : ImageRepository<TechnologyLogo>
    {
        public TechnologyLogoRepository(AppDb context)
            : base(context)
        {

        }

        protected override DbSet<TechnologyLogo> EntitySet => Context.TechnologyLogos;
    }
}
