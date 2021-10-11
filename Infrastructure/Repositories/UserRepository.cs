using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<IEnumerable<Review>> GetReviewsByUser(int userId)
        {
            var reviews = await _dbContext.Reviews.Include(r => r.Movie).Where(r => r.UserId == userId).ToListAsync();
            return reviews;
        }

        public Review AddReview(int userId, int movieId, decimal rating, string reviewText)
        {
            var review = new Review { UserId = userId, MovieId = movieId, Rating = rating, ReviewText = reviewText};
            return review;
        }

        public Review UpdateReview(int userId, int movieId, decimal rating, string reviewText)
        {
            var review = new Review { UserId = userId, MovieId = movieId, Rating = rating, ReviewText = reviewText };
            return review;
        }

        public Favorite AddFavorite(int userId, int movieId)
        {
            var favorite = new Favorite { UserId = userId, MovieId = movieId };
            return favorite;
        }
    }
}