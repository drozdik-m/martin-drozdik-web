using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bonsai.DataPersistence.Repositories.Traits.Abstraction;
using Bonsai.Models.Abstraction;
using Bonsai.Models.Abstraction.Entities;
using Bonsai.Models.Abstraction.Services;
using Bonsai.Models.Exceptions.CRUD;
using MartinDrozdik.Data.Models.Media.Files;
using MartinDrozdik.Data.Models.Media.Images;
using MartinDrozdik.Data.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Bonsai.DataPersistence.Repositories.Traits
{
    interface IMedializedRepositoryTrait<TEntity, TKey, TContext>
        : ICRUDRepository<TEntity, TKey>, ICRUDRepositoryTrait<TEntity, TKey, TContext>
        where TEntity : class, IIdentifiable<TKey>, IHideable
        where TContext : DbContext
    {
        public IQueryable<TEntity> TIncludeMediaDependency(IQueryable<TEntity> entities, Expression<Func<TEntity, MediaFile>> expression)
        {
            return entities
                .Include(expression)
                .ThenInclude(e => e.Image)
                .ThenInclude(e => e.Thumbnail);
        }

        public IQueryable<TEntity> TIncludeMediaDependency(IQueryable<TEntity> entities, Expression<Func<TEntity, Image>> expression)
        {
            return entities
                .Include(expression)
                .ThenInclude(e => e.Thumbnail);
        }

        public IQueryable<TEntity> TIncludeMediaDependency(IQueryable<TEntity> entities, Expression<Func<TEntity, ImageThumbnail>> expression)
        {
            return entities
                .Include(expression);
        }

        public void TDeleteMediaDependency(MediaFile file)
        {
            if (file == null)
                return;

            Context.Entry(file).State = EntityState.Deleted;

            if (file.Image != null)
                TDeleteMediaDependency(file.Image);
        }

        public void TDeleteMediaDependency(Image image)
        {
            if (image == null)
                return;

            Context.Entry(image).State = EntityState.Deleted;

            if (image.Thumbnail != null)
                TDeleteMediaDependency(image.Thumbnail);
        }

        public void TDeleteMediaDependency(ImageThumbnail imageThumbnail)
        {
            if (imageThumbnail == null)
                return;

            Context.Entry(imageThumbnail).State = EntityState.Deleted;
        }
    }
}
