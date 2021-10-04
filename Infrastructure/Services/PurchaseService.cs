using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using ApplicationCore.RepositoryInterfaces;

namespace Infrastructure.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }
        public async Task<IEnumerable<MovieCardResponseModel>> GetPurchasedMoviesByUser(int id)
        {
            var movies = await _purchaseRepository.GetPurchasedMoviesByUserId(id);
            var purchaseRespondModel = new List<MovieCardResponseModel>();
            foreach(var movie in movies)
                purchaseRespondModel.Add(new MovieCardResponseModel { Id = movie.Id, PosterUrl = movie.PosterUrl });
            return purchaseRespondModel;
        }
    }
}
