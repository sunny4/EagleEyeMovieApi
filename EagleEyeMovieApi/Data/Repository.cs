using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace EagleEyeMovieApi.Data
{

    public class Repository
    {
        private List<MovieMetaDataInstance> MetaData { get; set; }
        private List<StatsDataInstance> StatsData { get; set; }

        public Repository()
        {
            LoadCsvMovieData();
            LoadCsvStatsData();
        }

        public List<MovieStatisticsInstance> GetMovieStats(string language = "EN")
        {
            List<StatsDataInstance> summarisedStats = StatsData.GroupBy(x => x.MovieId)
                                       .Select(x => new StatsDataInstance
                                       {
                                           MovieId = x.First().MovieId,
                                           WatchDurationMs = x.Sum(x => x.WatchDurationMs),
                                           MovieWatchCount = x.Count(),
                                           AverageWatchTimeS = (int)(x.Sum(x => x.WatchDurationMs) / x.Count()) / 1000
                                       }).ToList();

            List<MovieStatisticsInstance> AllMovieStats = new List<MovieStatisticsInstance>();
            foreach (StatsDataInstance movieStatsSummary in summarisedStats)
            {
                MovieMetaDataInstance movieInfo = GetMovieData(movieStatsSummary.MovieId).Where(x => x.Language == language).FirstOrDefault();
                if (movieInfo != null)
                {
                    MovieStatisticsInstance m = new MovieStatisticsInstance();
                    m.MovieId = movieStatsSummary.MovieId;
                    m.Title = movieInfo.Title;
                    m.AverageWatchDurationS = movieStatsSummary.AverageWatchTimeS;
                    m.Watches = movieStatsSummary.MovieWatchCount;
                    m.ReleaseYear = movieInfo.ReleaseYear;
                    AllMovieStats.Add(m);
                }
            }
            return AllMovieStats;
        }

        private int NextId()
        {
            return MetaData.Max(x => x.Id) + 1;
        }

        public List<MovieMetaDataInstance> GetAllMovieData()
        {
            return MetaData;
        }

        public List<MovieMetaDataInstance> GetMovieData(int movieId)
        {
            List<MovieMetaDataInstance> MovieMetaData = MetaData
                .Where(x => x.MovieId == movieId)
                .OrderByDescending(x => x.Id) // Put's last changed film/language instance at top
                .GroupBy(x => new { x.MovieId, x.Language })
                .Select(x => x.FirstOrDefault())
                .ToList();

            return MovieMetaData;
        }

        public void AddMetaData(MovieMetaDataInstance metaDataInstance)
        {
            metaDataInstance.Id = NextId();
            MetaData.Add(metaDataInstance);
        }

        private void LoadCsvMovieData()
        {
            try
            {
                MetaData = new List<MovieMetaDataInstance>();
                TextFieldParser parser = new TextFieldParser("Data\\MovieData\\metadata.csv");
                parser.HasFieldsEnclosedInQuotes = true;
                parser.SetDelimiters(",");
                int lineNumber = 0;
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    lineNumber++;
                    if (lineNumber > 1 && fields.Length == 6) //check all data fields are present (ignore non-present data)
                    {
                        MovieMetaDataInstance metaDataInstance = new MovieMetaDataInstance();
                        metaDataInstance.Id = Convert.ToInt32(fields[0]);
                        metaDataInstance.MovieId = Convert.ToInt32(fields[1]);
                        metaDataInstance.Title = Convert.ToString(fields[2]);
                        metaDataInstance.Language = Convert.ToString(fields[3]);
                        metaDataInstance.Duration = Convert.ToString(fields[4]);
                        metaDataInstance.ReleaseYear = Convert.ToInt32(fields[5]);

                        MetaData.Add(metaDataInstance);
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        private void LoadCsvStatsData()
        {
            try
            {
                StatsData = new List<StatsDataInstance>();
                TextFieldParser parser = new TextFieldParser("Data\\MovieData\\stats.csv");
                parser.SetDelimiters(",");
                int lineNumber = 0;
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    lineNumber++;
                    if (lineNumber > 1 && fields.Length == 2) //check all data fields are present (ignore non-present data)
                    {
                        StatsDataInstance StatsInstance = new StatsDataInstance();
                        StatsInstance.MovieId = Convert.ToInt32(fields[0]);
                        StatsInstance.WatchDurationMs = Convert.ToInt32(fields[1]);
                        StatsData.Add(StatsInstance);
                    }
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
