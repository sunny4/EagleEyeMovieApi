using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleEyeMovieApi.Data
{
    public class MovieStatisticsInstance
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int AverageWatchDurationS { get; set; }
        public int Watched { get; set; }
        public int ReleaseYear { get; set; }
    }
}
