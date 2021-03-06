using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
        // Controller returns models
        Task<IEnumerable<MovieCardResponseModel>> Get30HighestGrossingMovies();
        Task<IEnumerable<MovieCardResponseModel>> Get30HighestRatedMovies();
        Task<IEnumerable<GenreModel>> GetMoviesGenres();
        Task<IEnumerable<MovieReviewsRespondModel>> GetReviewsByMovie(int movieId);
        Task<MovieDetailsResponseModel> GetMovieAsync(int id);

    }
}

