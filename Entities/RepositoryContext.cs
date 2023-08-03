using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace OrderApp.Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<RestaurantBranch>? RestaurantBranches { get; set; }
    }

}
