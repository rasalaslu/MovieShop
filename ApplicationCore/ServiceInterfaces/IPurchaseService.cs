using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IPurchaseService
    {
        Task<IEnumerable<MovieCardResponseModel>> GetPurchasedMoviesByUser(int id);
    }
}
