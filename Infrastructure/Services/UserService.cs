using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using ApplicationCore.Exceptions;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMovieService _movieService;
        private readonly IAsyncRepository<Review> _reviewRepository;
        private readonly IAsyncRepository<Favorite> _favoriteRepository;

        public UserService(IUserRepository userRepository, IPurchaseRepository purchaseRepository, IMovieService movieService, 
            IAsyncRepository<Review> reviewRepository, IAsyncRepository<Favorite> favoriteRepository)
        {
            _userRepository = userRepository;
            _purchaseRepository = purchaseRepository;
            _movieService = movieService;
            _reviewRepository = reviewRepository;
            _favoriteRepository = favoriteRepository;
        }

        // Register
        public async Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel)
        {
            // first check if the email user entered exists in the database
            // if yes, throw an throw exception or send a message saying email exists
            var user = await _userRepository.GetUserByEmail(requestModel.Email);

            if (user != null)
                // email exits in the database
                throw new Exception($"Email {requestModel.Email} exists, please try to login");
            // continue
            // create a random salt and hash the password with the salt

            var salt = GenerateSalt();
            var hashedPassword = GenerateHashedPassword(requestModel.Password, salt);

            // create user entity object and call user repo to save
            var newUser = new User
            {
                Email = requestModel.Email,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                DateOfBirth = requestModel.DateOfBirth,
                Salt = salt,
                HashedPassword = hashedPassword
            };

            var createdUser = await _userRepository.AddAsync(newUser);

            var userRegisterResponseModel = new UserRegisterResponseModel
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName
            };

            return userRegisterResponseModel;
        }

        // Get movies which the user purchased
        public async Task<PurchaseResponseModel> GetPurchaseByUserId(int userId)
        {

            var purchasedMovies = await _purchaseRepository.GetPurchasedMoviesByUserId(userId);
            var movies = new PurchaseResponseModel
            {
                UserId = userId,
                PurchasedMovies = new List<MovieCardResponseModel>()
            };
            foreach(var movie in purchasedMovies)
            {
                movies.PurchasedMovies.Add(new MovieCardResponseModel
                {
                    Id = movie.MovieId,
                    Title = movie.Movie.Title,
                    PosterUrl = movie.Movie.PosterUrl,
                    //ReleaseDate = movie.Movie.ReleaseDate.GetValueOrDefault()
                });
            }
            return movies;
        }

        public async Task<MovieReviewsRespondModel> GetReviewsByUserId(int userId)
        {
            var userReviews = await _userRepository.GetReviewsByUser(userId);
            var reviews = new MovieReviewsRespondModel
            {
                UserId = userId,
                UserReviews = new List<ReviewRequestModel>()
            };
            foreach(var review in userReviews)
            {
                reviews.UserReviews.Add(new ReviewRequestModel
                {
                    MovieId = review.MovieId,
                    UserId = userId,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText
                });
            }
            return reviews;
        }


        // To purchase movie
        public async Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            // See if Movie is already purchased.
            if (await IsMoviePurchased(purchaseRequest, userId))
                throw new ConflictException("Movie already Purchased");
            // Get Movie Price from Movie Table
            var movie = await _movieService.GetMovieAsync(purchaseRequest.MovieId);

            var purchase = new Purchase
            {
                MovieId = purchaseRequest.MovieId,
                PurchaseNumber = Guid.NewGuid(),
                PurchaseDateTime = DateTime.UtcNow,
                TotalPrice = movie.Price.GetValueOrDefault(),
                UserId = userId
            };
            //  var purchase = _mapper.Map<Purchase>(purchaseRequest);
            var createdPurchase = await _purchaseRepository.AddAsync(purchase);
            return createdPurchase.Id > 0;
        }
        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            return await _purchaseRepository.GetExistsAsync(p =>
                p.UserId == userId && p.MovieId == purchaseRequest.MovieId);
        }

        public async Task AddReview(int userId, int movieId, decimal rating, string reviewText)
        {
            var review = _userRepository.AddReview(userId, movieId, rating, reviewText);
            await _reviewRepository.AddAsync(review);
        }

        public async Task UpdateReview(int userId, int movieId, decimal rating, string reviewText)
        {
            var review = _userRepository.UpdateReview(userId, movieId, rating, reviewText);
            await _reviewRepository.UpdateAsync(review);
        }

        public async Task DeleteReview(int userId, int movieId)
        {
            var review = await _reviewRepository.ListAsync(r => r.UserId == userId && r.MovieId == movieId);
            await _reviewRepository.DeleteAsync(review.First());
        }

        public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            var isfavorite = await _favoriteRepository.ListAsync(f => f.UserId == favoriteRequest.UserId && f.MovieId == favoriteRequest.UserId);
            if(isfavorite != null)
                throw new ConflictException("Movie already Favorited");
            var favorite = _userRepository.AddFavorite(favoriteRequest.UserId, favoriteRequest.UserId);
            var addfavorite = await _favoriteRepository.AddAsync(favorite);
        }

        public async Task DeleteFavorite(FavoriteRequestModel favoriteRequest)
        {
            var isfavorite = await _favoriteRepository.ListAsync(f => f.UserId == favoriteRequest.UserId && f.MovieId == favoriteRequest.UserId);
            if (isfavorite == null)
                throw new ConflictException("Movie already Unfavorited");
            await _favoriteRepository.DeleteAsync(isfavorite.First());
        }

        // Validate User & Hashed Password
        public async Task<UserLoginResponseModel> ValidateUser(string email, string password)
        {
            // get the user info from database by email
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
                // we dont have the email in the database
                return null;

            // we need to hash the user entered password along with salt from database.
            var hashedPassword = GenerateHashedPassword(password, user.Salt);

            if (hashedPassword != user.HashedPassword) return null;
            // user entered correct password
            var userLoginResponseModel = new UserLoginResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth
            };
            return userLoginResponseModel;

        }
        private string GenerateSalt()
        {
            var randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }
        private string GenerateHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                Convert.FromBase64String(salt),
                KeyDerivationPrf.HMACSHA512,
                10000,
                256 / 8));
            return hashed;
        }
        
    }
}