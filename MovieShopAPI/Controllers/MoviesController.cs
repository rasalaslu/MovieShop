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
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            // Along with data you also need to return HTTP status code
            var movies = await _movieService.Get30HighestGrossingMovies();
            if(!movies.Any())
            {
                return NotFound("No Movie Found");
            }
            return Ok(movies);
        }

        [Route("toprated")]
        [HttpGet]
        public async Task<IActionResult> GetTopRatingMovies()
        {
            var movies = await _movieService.Get30HighestRatedMovies();
            if (!movies.Any())
            {
                return NotFound("No Movie Found");
            }
            return Ok(movies);
        }

        [Route("genre/{genreid:int}")]
        [HttpGet]
        public async Task<IActionResult> GetMovieGenres()
        {
            var genres = await _movieService.GetMoviesGenres();
            if (!genres.Any())
            {
                return NotFound("No Movie Found");
            }
            return Ok(genres);
        }

        [Route("{id:int}/review")]
        [HttpGet]
        public async Task<IActionResult> GetReviewsByMovie(int id)
        {
            // Along with data you also need to return HTTP status code
            var reviews = await _movieService.GetReviewsByMovie(id);
            if (!reviews.Any())
            {
                return NotFound("No Review for the movie Found");
            }
            return Ok(reviews);
        }


        [HttpGet]
        [Route("{id:int}", Name = "GetMovie")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieAsync(id);
            return Ok(movie);
        }
    }
}
