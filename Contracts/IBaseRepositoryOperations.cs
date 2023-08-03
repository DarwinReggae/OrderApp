using Entities.Models;

namespace Contracts
{
    public interface IBaseRepositoryOperations
    {
        Task<List<RestaurantBranch>> GetAllBranches();

    }
}
