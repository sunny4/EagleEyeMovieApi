using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleEyeMovieApi.Data
{
    public class MovieMetaDataInstance
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string Duration { get; set; }
        public int ReleaseYear { get; set; }
    }

    public class StatsDataInstance
    {
        public int MovieId { get; set; }
        public long WatchDurationMs { get; set; }
        public int MovieWatchCount { get; set; }
        public int AverageWatchTimeS { get; set; }
    }
}
