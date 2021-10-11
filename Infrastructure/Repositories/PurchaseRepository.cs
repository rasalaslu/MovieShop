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
    public class PurchaseRepository : EfRepository<Purchase>, IPurchaseRepository
    {
        protected readonly MovieShopDbContext _dbContext;

        public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Purchase>> GetPurchasedMoviesByUserId(int userId)
        {
            var purchases = await _dbContext.Purchases.Include(m => m.Movie).Where(p => p.UserId == userId)
                .OrderByDescending(p => p.PurchaseDateTime).ToListAsync();
            return purchases;
        }
    }
}
