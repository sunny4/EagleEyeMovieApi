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
        [DataMember]
        private List<MetaDataInstance> MetaData { get; set; }

        public Repository()
        {
            LoadCsvData();
        }

        private int NextId()
        {
            return MetaData.Max(x => x.Id) + 1;
        }

        public List<MetaDataInstance> GetAll(int movieId)
        {
            return MetaData;
        }

        public List<MetaDataInstance> GetMovieData(int movieId)
        {
            List<MetaDataInstance> MovieMetaData = MetaData
                .Where(x => x.MovieId == movieId)
                .OrderByDescending(x => x.Id) // Put's last changed film/language instance at top
                .GroupBy(x => new { x.MovieId, x.Language })
                .Select(x => x.FirstOrDefault())
                .ToList();

            return MovieMetaData;
        }

        public void AddMetaData(MetaDataInstance metaDataInstance)
        {
            metaDataInstance.Id = NextId();
            MetaData.Add(metaDataInstance);
        }

        private void LoadCsvData()
        {
            try
            {
                MetaData = new List<MetaDataInstance>();
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
                        MetaDataInstance metaDataInstance = new MetaDataInstance();
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
    }
}
