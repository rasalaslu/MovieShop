using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Service_Interfaces
{
    public interface IMovieService
    {
        // Models
        IEnumerable<MovieCardResponseModel> Get30HighestGrossingMovies();
    }
}
}
