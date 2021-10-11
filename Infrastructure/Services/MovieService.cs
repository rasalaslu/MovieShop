using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<MovieCardResponseModel>> Get30HighestGrossingMovies()
        {
            // list of movie entites 
            var movies = await _movieRepository.Get30HighestGrossingMovies();

            var moviesCardResponseModel = new List<MovieCardResponseModel>();

            // mapping entites to models data so that services always return models mot entites
            foreach (var movie in movies)
                moviesCardResponseModel.Add(new MovieCardResponseModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title, Revenue = movie.Revenue });

            // return list of movieresponse models
            return moviesCardResponseModel;
        }

        public async Task<IEnumerable<MovieCardResponseModel>> Get30HighestRatedMovies()
        {
            var movies = await _movieRepository.Get30HighestRatingMovies();

            var moviesCardResponseModel = new List<MovieCardResponseModel>();

            // mapping entites to models data so that services always return models mot entites
            foreach (var movie in movies)
                moviesCardResponseModel.Add(new MovieCardResponseModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title, Revenue = movie.Revenue });

            // return list of movieresponse models
            return moviesCardResponseModel;
        }

        public async Task<IEnumerable<GenreModel>> GetMoviesGenres()
        {
            var genres = await _movieRepository.GetMoviesGenres();
            var genreModel = new List<GenreModel>();
            foreach (var genre in genres)
                genreModel.Add(new GenreModel { Id = genre.Id, Name = genre.Name });
            return genreModel;
        }

        public async Task<IEnumerable<MovieReviewsRespondModel>> GetReviewsByMovie(int movieId)
        {
            var reviews = await _movieRepository.GetReviewsByMovie(movieId);
            var reviewResponseModel = new List<MovieReviewsRespondModel>();
            foreach (var review in reviews)
                reviewResponseModel.Add(new MovieReviewsRespondModel { MovieId = movieId, UserId = review.UserId, Rating = review.Rating, ReviewText = review.ReviewText });
            return reviewResponseModel;
        }

        public async Task<MovieDetailsResponseModel> GetMovieAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie == null) throw new Exception($"No Movie found for {id}");
            var movieDetails = new MovieDetailsResponseModel
            {
                Id = movie.Id,
                Budget = movie.Budget,
                Overview = movie.Overview,
                Price = movie.Price,
                PosterUrl = movie.PosterUrl,
                Revenue = movie.Revenue,
                ReleaseDate = movie.ReleaseDate.GetValueOrDefault(),
                Rating = movie.Rating,
                Tagline = movie.Tagline,
                Title = movie.Title,
                RunTime = movie.RunTime,
                BackdropUrl = movie.BackdropUrl,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl
            };

            foreach (var movieGenre in movie.MovieGenres)
                movieDetails.Genres.Add(new GenreModel
                {
                    Id = movieGenre.Genre.Id,
                    Name = movieGenre.Genre.Name
                });

            foreach (var movieCast in movie.MovieCasts)
                movieDetails.Casts.Add(new CastResponseModel
                {
                    Id = movieCast.Cast.Id,
                    Name = movieCast.Cast.Name,
                    Character = movieCast.Character,
                    Gender = movieCast.Cast.Gender,
                    ProfilePath = movieCast.Cast.ProfilePath,
                    TmdbUrl = movieCast.Cast.TmdbUrl
                });

            foreach (var trailer in movie.Trailers)
                movieDetails.Trailers.Add(new TrailerResponseModel
                {
                    Id = trailer.Id,
                    Name = trailer.Name,
                    TrailerUrl = trailer.TrailerUrl,
                    MovieId = trailer.MovieId
                });
            return movieDetails;

        }

    }
}
