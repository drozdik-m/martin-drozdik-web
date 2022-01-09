using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartinDrozdik.Web.Views.Home
{
    public class AboutMeModel
    {
        public AboutMeModel(DateTime programmingStart, DateTime bonsaiStart, int completedProjectsCount)
        {
            ProgrammingStart = programmingStart;
            BonsaiStart = bonsaiStart;
            CompletedProjectsCount = completedProjectsCount;
        }

        public DateTime ProgrammingStart { get; }

        public DateTime BonsaiStart { get; }

        public int CompletedProjectsCount { get; }

        public static AboutMeModel Default => new AboutMeModel(
            new DateTime(2016, 2, 1),
            new DateTime(2017, 1, 10),
            30);
    }
}
