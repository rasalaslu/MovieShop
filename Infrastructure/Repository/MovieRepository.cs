using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        private readonly MovieShopDbContext _movieShopDbContext;

        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
            _movieShopDbContext = dbContext;
        }

        public async Task<IEnumerable<Movie>> Get30HighestGrossingMovies()
        {
            // async/await go as pair
            // EF , Dapper...they have both async methods and sync method
            var movies = await _movieShopDbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }

        public override async Task<Movie> GetByIdAsync(int id)
        {
            var moviedetails = await _dbContext.Movies.Include(m => m.MovieGenres).ThenInclude(m => m.Genre)
                .Include(m => m.Trailers).FirstOrDefaultAsync(m => m.Id == id);
            if (moviedetails == null) throw new Exception($"NO Movie Found for this {id}");
            // get average rating
            //  var rating = await _dbContext.Reviews.where(r => r.MovieId == id).DefaultIfEmpty().AverageAsync( r => r==null? 0: r.Rating);
            // moviedetails.Rating = rating;

            return moviedetails;
        }
    }
}
