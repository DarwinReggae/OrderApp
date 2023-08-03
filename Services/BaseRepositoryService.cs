using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using OrderApp.Entities;

namespace Services
{
    public class BaseRepositoryService : IBaseRepositoryOperations
    {
        protected RepositoryContext RepositoryContext { get; set; }
        public BaseRepositoryService(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }
        public async Task<List<RestaurantBranch>> GetAllBranches()
        {
            return await RepositoryContext.Set<RestaurantBranch>().AsQueryable().AsNoTracking().ToListAsync();
        }
    }
}