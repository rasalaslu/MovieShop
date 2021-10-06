using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    // Attribute Routing
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // api/movies/toprevenue
        [Route("toprevenue")]
        [HttpGet]
        public async Task<IActionResult> GetTopRevenurMovies()
        {
            // Along with data you also need to return HTTP status code
            var movies = await _movieService.Get30HighestGrossingMovies();
            if(!movies.Any())
            {
                return NotFound("No Moviee Found");
            }
            return Ok(movies);
        }
    }
}
