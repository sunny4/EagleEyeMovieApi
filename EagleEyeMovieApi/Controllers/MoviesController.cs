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
    public class MoviesController : ControllerBase
    {
        private Repository Repository = Startup.StartUpRepo;

        [HttpGet("stats")]
        public ActionResult<List<MovieStatisticsInstance>> GetStats()
        {
            List<MovieStatisticsInstance> MovieMetaData = Repository.GetMovieStats();

            return MovieMetaData;
        }

    }
}
