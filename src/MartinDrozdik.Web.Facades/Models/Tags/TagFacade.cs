using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonsai.Models.Abstraction.Localization;
using MartinDrozdik.Data.Models.Tags;
using MartinDrozdik.Data.Repositories.Abstraction;
using MartinDrozdik.Data.Repositories.Models.Tags;
using MartinDrozdik.Web.Facades.Traits;
using Microsoft.AspNetCore.Hosting;

namespace MartinDrozdik.Web.Facades.Models.Tags
{
    public class TagFacade<TTag> : CRUDFacade<TTag, int>,
        IOrderableFacadeTrait<TTag, int>
        where TTag : Tag
    {

        readonly IOrderableFacadeTrait<TTag, int> orderableTrait;

        readonly TagRepository<TTag> repository;

        public TagFacade(TagRepository<TTag> repository)
            : base(repository)
        {
            this.repository = repository;

            orderableTrait = this;
        }


        #region Orderable trait

        IOrderableRepository<TTag, int> IOrderableFacadeTrait<TTag, int>.OrderableRepository => repository;

        public Task ReorderAsync(IEnumerable<int> newOrder) => orderableTrait.TReorderAsync(newOrder);

        #endregion

    }
}
