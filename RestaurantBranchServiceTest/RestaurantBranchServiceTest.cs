using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using OrderApp.Entities;
using Repositories;

namespace RestaurantBranchServiceTest
{
    public class RestaurantBranchServiceTest
    {
        [Theory]
        [InlineData(30, 50)]
        public void GetFiveNearestBranches_ShouldReturnRestaurantBranchDtoList(double latitude, double longitude)
        {
            List<RestaurantBranch> restaurantBranches = new(){ new RestaurantBranch { Id = 1, Name="b1", Latitude=30, Longitude=50 },
            new RestaurantBranch { Id = 2, Name="b2", Latitude=35, Longitude=53 },new RestaurantBranch { Id = 3, Name="b3", Latitude=33, Longitude=52 },
            new RestaurantBranch { Id = 4, Name="b4", Latitude=40, Longitude=60 },new RestaurantBranch { Id = 5, Name="b5", Latitude=30, Longitude=55 },
            new RestaurantBranch { Id = 6, Name="b6", Latitude=31, Longitude=52 }};
            List<RestaurantBranchDto> restaurantBranchDtos = new() { new RestaurantBranchDto {Name="b1", Distance=0 },
            new RestaurantBranchDto {Name="b2", Distance=0 },new RestaurantBranchDto {Name="b3", Distance=0 },
            new RestaurantBranchDto {Name="b5", Distance=0 },new RestaurantBranchDto {Name="b6", Distance=0 }};
            

            var mockRepository = new Mock<IBaseRepositoryOperations>();

            var mockMapper = new Mock<IMapper>();

            var findFiveNearestBranches = new Mock<IFindFiveNearestBranches>();
            findFiveNearestBranches.Setup(x => x.FindFiveNearestBranches(latitude, longitude)).ReturnsAsync(restaurantBranchDtos);

            

            var restaurantBranchService = new RestaurantBranchService(mockRepository.Object, mockMapper.Object);

            var result = restaurantBranchService.FindFiveNearestBranches(latitude,longitude);

            Assert.IsType<Task<List<RestaurantBranchDto>>>(result);
            Assert.Equal(restaurantBranchDtos.Count, result.Result.Count);

        }
    }
}