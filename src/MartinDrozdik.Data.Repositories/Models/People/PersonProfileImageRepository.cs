﻿using System;
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

namespace MartinDrozdik.Data.Repositories.Models.People
{
    public abstract class PersonProfileImageRepository<TMedia> : CRUDRepository<TMedia, int, AppDb>
        where TMedia : PersonProfileImage
    {
        public PersonProfileImageRepository(AppDb context)
            : base(context)
        {

        }
    }
}
