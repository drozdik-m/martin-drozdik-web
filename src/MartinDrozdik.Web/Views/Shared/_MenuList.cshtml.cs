using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Web.Views.Home;

namespace MartinDrozdik.Web.Views.Shared
{
    public class MenuListModel
    {
        public MenuListModel(PageId pageId)
        {
            PageId = pageId;
        }

        public PageId PageId { get; }

        public static MenuListModel Default(PageId pageId) => new(pageId);
    }

}
