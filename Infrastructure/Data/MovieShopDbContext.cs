using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entities;

namespace Infrastructure.Data
{
    public class MovieShopDbContext: DbContext
    {   
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options): base(options)
        {

        }

        public DbSet<Genre> Genres { get; set; }
    }
}
