using Entities.DataTransferObjects;
using Entities.Models;

namespace Contracts
{
    public interface IFindFiveNearestBranches
    {
        Task<List<RestaurantBranchDto>> FindFiveNearestBranches(double latitude, double longitude);
    }
}