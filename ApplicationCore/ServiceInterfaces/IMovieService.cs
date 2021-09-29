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
        IEnumerable<MovieCardResponseModel> Get30HighestGrossingMovies();
        IEnumerable<MovieCardResponseModel> GetOneGenreMovies(int genreId);
    }
}

