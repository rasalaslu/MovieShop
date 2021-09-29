using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieShopDbContext _movieShopDbContext;

        public MovieRepository(MovieShopDbContext dbContext)
        {
            _movieShopDbContext = dbContext;
        }
        public IEnumerable<Movie> Get30HighestGrossingMovies()
        {
            // go to database and and get data using LINQ with EF
            var movies = _movieShopDbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToList();
            return movies;

        }

        public IEnumerable<Movie> GetOneGenreMovies(int genreId)
        {
            var movies = _movieShopDbContext.Movies.
        }
    }
}
