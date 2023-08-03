using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using OrderApp.Entities;
using AutoMapper.Internal;
using Exceptions;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Repositories
{
    public class RestaurantBranchService : IFindFiveNearestBranches
    {
        private IBaseRepositoryOperations BaseRepositoryOperations { get; set; }
        private readonly IMapper mapper;
        private static int FindDistance(double latitudeA, double longitudeA, double latitudeB, double longitudeB)
        {
            int distance = (int)Math.Sqrt(Math.Pow(latitudeA - latitudeB, 2) + Math.Pow(longitudeA - longitudeB, 2));
            return distance;
        }
        private static void ValidateCoordinates(double latitude, double longitude)
        {
            Regex regex = new("^[ 0-9]{1,3},[0-9]{0,2}$|^([0-9]{1,3})$");

            if (!regex.IsMatch(latitude.ToString()) || !regex.IsMatch(longitude.ToString()))
                throw new InvalidCoordinatesException("Lütfen geçerli bir koordinat giriniz.");

        }
        public RestaurantBranchService(IBaseRepositoryOperations baseRepositoryOperations, IMapper mapper)
        {
            BaseRepositoryOperations = baseRepositoryOperations;
            this.mapper = mapper;
        }
        /// <summary>
        /// Find five nearest restaurant branch to user if exist.
        /// </summary>
        /// <param name="latitude">The x in coordinates.</param>
        /// <param name="longitude">The y in coordinates.</param>     
        public async Task<List<RestaurantBranchDto>> FindFiveNearestBranches(double latitude, double longitude)
        {
            ValidateCoordinates(latitude, longitude);   

            var restaurantBranches = await BaseRepositoryOperations.GetAllBranches();
            var nearestRestaurantBranches = restaurantBranches.OrderBy(b => FindDistance(b.Latitude, b.Longitude, latitude, longitude)).Take(5).ToList();

            if (nearestRestaurantBranches == null || nearestRestaurantBranches.Count == 0)
                throw new NoAvailableBranchesException("Konumunuz için uygun şube bulunamadı.");

            List<RestaurantBranchDto> nearestRestaurantBranchDtos = new();

            for (int i = 0; i < nearestRestaurantBranches.Count; i++)
            {
                var distance = FindDistance(nearestRestaurantBranches[i].Latitude, nearestRestaurantBranches[i].Longitude, latitude, longitude);
                if (distance <= 10)
                {
                    nearestRestaurantBranchDtos.Add(mapper.Map<RestaurantBranchDto>(nearestRestaurantBranches[i]));
                    nearestRestaurantBranchDtos[i].Distance = distance;
                }
            }
            return nearestRestaurantBranchDtos;
        }
       
    }


}
