using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace OrderApp
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<RestaurantBranch, RestaurantBranchDto>();
        }
    }
}
