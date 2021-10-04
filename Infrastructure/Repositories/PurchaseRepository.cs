using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        protected readonly MovieShopDbContext _dbContext;

        public PurchaseRepository(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Movie>> GetPurchasedMoviesByUserId(int id)
        {
            var movies = await
                (from m in _dbContext.Movies
                 join p in _dbContext.Purchases
                 on m.Id equals p.MovieId
                 where p.UserId == id
                 select m).ToListAsync();
            return movies;
        }
    }
}
