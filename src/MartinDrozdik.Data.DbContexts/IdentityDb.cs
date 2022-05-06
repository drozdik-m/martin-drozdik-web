using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.UserIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace MartinDrozdik.Data.DbContexts
{
    public class IdentityDb : IdentityDbContext<AppUser>
    {
        public DbSet<AppUser> AppUser { get; set; }

        public IdentityDb(DbContextOptions<IdentityDb> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
