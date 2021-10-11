using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;

// Repository returns entities
namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository: IAsyncRepository<Movie>
    {

        Task<IEnumerable<Movie>> Get30HighestGrossingMovies();
        Task<IEnumerable<Movie>> Get30HighestRatingMovies();
        Task<IEnumerable<Genre>> GetMoviesGenres();
        Task<IEnumerable<Review>> GetReviewsByMovie(int movieId);
        //Task<Review> PutReviewByMovie(int movieId, int rating, string reviewText);

    }
}
