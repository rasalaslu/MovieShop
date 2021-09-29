using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;

// Repository returns entities
namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> Get30HighestGrossingMovies();
        IEnumerable<Movie> GetOneGenreMovies(int genreId);
    }
}
