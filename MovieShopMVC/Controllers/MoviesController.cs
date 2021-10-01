using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Services;
using ApplicationCore.ServiceInterfaces;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;

        }

        public IActionResult Details(int id)
        {
            return View();
        }
        // public IActionResult GetTopRevenueMovies()
        // {
        //     //var movieService = new MovieNoSQLService();
        //     var movies = _movieService.Get30HighestGrossingMovies();
        //     return View(movies);
        // }

    }
}
