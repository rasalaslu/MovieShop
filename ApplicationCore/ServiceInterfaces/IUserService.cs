using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.Entities;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel);
        Task<UserLoginResponseModel> ValidateUser(string email, string password);
        Task<PurchaseResponseModel> GetPurchaseByUserId(int userId);
        Task<MovieReviewsRespondModel> GetReviewsByUserId(int userId);
        Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId);
        Task AddReview(int userId, int movieId, decimal rating, string reviewText);
        Task UpdateReview(int userId, int movieId, decimal rating, string reviewText);
        Task DeleteReview(int userId, int movieId);
        Task AddFavorite(FavoriteRequestModel favoriteRequest);
        Task DeleteFavorite(FavoriteRequestModel favoriteRequest);
    }
}
