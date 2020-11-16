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

            List<MetaDataInstance> MovieMetaData = Repository.MetaData
                .Where(x => x.MovieId == movieId)
                .OrderByDescending(x => x.Id) // Put's last changed film/language instance at top
                .GroupBy(x => new { x.MovieId, x.Language })
                .Select(x => x.FirstOrDefault())
                .ToList();

            if (MovieMetaData == null || MovieMetaData.Count == 0) return NotFound();

            return MovieMetaData;
        }

        // POST api/<MovieController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MovieController>/5
        [HttpPut("{id}")]
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
