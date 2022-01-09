using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartinDrozdik.Web.Views.Home
{
    public class HeroHeaderModel
    {
        public HeroHeaderModel(bool freeForWork)
        {
            FreeForWork = freeForWork;
        }

        public bool FreeForWork { get; }

        public static HeroHeaderModel Default => new HeroHeaderModel(true);
    }
}
