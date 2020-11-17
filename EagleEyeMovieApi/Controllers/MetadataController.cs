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
    public class MetadataController : ControllerBase
    {
        private Repository Repository = Startup.StartUpRepo;

        [HttpGet]
        public ActionResult<List<MovieMetaDataInstance>> GetAll(int movieId)
        {
            List<MovieMetaDataInstance> MovieMetaData = Repository.GetAllMovieData();

            if (MovieMetaData == null || MovieMetaData.Count == 0) return NotFound();

            return MovieMetaData;
        }

        [HttpGet("{movieId}")]
        public ActionResult<List<MovieMetaDataInstance>> Get(int movieId)
        {
            List<MovieMetaDataInstance> MovieMetaData = Repository.GetMovieData(movieId);

            if (MovieMetaData == null || MovieMetaData.Count == 0) return NotFound();

            return MovieMetaData;
        }

        [HttpPost]
        public ActionResult Add([FromBody] MovieMetaDataInstance movieData)
        {
            Repository.AddMetaData(movieData);
            return Ok();
        }
    }
}
