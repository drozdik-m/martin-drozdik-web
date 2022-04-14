using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.DataPersistence.DbContexts;
using Bonsai.Models.Abstraction.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Bonsai.Models.Abstraction.Localization;
using MartinDrozdik.Data.Repositories;
using MartinDrozdik.Data.Models.Tags;
using Bonsai.Models.Abstraction.Entities;
using MartinDrozdik.Data.Repositories.Traits;
using MartinDrozdik.Data.Models.Media;
using MartinDrozdik.Data.Models.Media.Images;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Repositories.Models.Media.Images;

namespace MartinDrozdik.Data.Repositories.Models.People
{
    public abstract class PersonImageRepository<TMedia> : ImageRepository<TMedia>
        where TMedia : PersonImage
    {
        public PersonImageRepository(AppDb context)
            : base(context)
        {

        }
    }
}
