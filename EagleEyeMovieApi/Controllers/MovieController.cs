using EagleEyeMovieApi.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EagleEyeMovieApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private static Repository Repository { get; set; }

        public MovieController()
        {
            if (Repository == null) 
                Repository = new Repository();
        }

        [HttpGet("metadata/{movieId}")]
        public ActionResult<List<MovieMetaDataInstance>> Get(int movieId)
        {
            List<MovieMetaDataInstance> MovieMetaData = Repository.GetMovieData(movieId);

            if (MovieMetaData == null || MovieMetaData.Count == 0) return NotFound();

            return MovieMetaData;
        }

        //[HttpGet("stats")]
        //public ActionResult<List<MetaDataInstance>> GetStats()
        //{
        //    //List<MetaDataInstance> MovieMetaData = Repository.GetMovieData();

        //   // return "";// MovieMetaData;
        //}

        [HttpPost("metadata")]
        public ActionResult Add([FromBody]MovieMetaDataInstance movieData)
        {
            Repository.AddMetaData(movieData);
            return Ok();
        }

        // PUT api/<MovieController>/5
        [HttpPut("metadata")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MovieController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
