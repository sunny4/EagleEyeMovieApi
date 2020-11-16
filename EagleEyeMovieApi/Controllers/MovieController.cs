using EagleEyeMovieApi.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EagleEyeMovieApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private Repository Repository { get; set; }
        public MovieController()
        {
            Repository = new Repository();
        }

        // GET: api/<MovieController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("metadata/{movieId}")]
        public ActionResult<List<MetaDataInstance>> Get(int movieId)
        {
            List<MetaDataInstance> MovieMetaData = Repository.GetMovieData(movieId);

            if (MovieMetaData == null || MovieMetaData.Count == 0) return NotFound();

            return MovieMetaData;
        }

        // POST api/<MovieController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPost("metadata")]
        public ActionResult Add([FromBody]MetaDataInstance movieData)
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
