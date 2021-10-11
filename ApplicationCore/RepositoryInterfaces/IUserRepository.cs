using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IUserRepository: IAsyncRepository<User>
    {
        Task<User> GetUserByEmail(string email);
        Task<IEnumerable<Review>> GetReviewsByUser(int userId);
        Review AddReview(int userId, int movieId, decimal rating, string reviewText);
        Review UpdateReview(int userId, int movieId, decimal rating, string reviewText);
        Favorite AddFavorite(int userId, int movieId);
    }
}
