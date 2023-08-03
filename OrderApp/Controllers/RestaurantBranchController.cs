using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace OrderApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantBranchController : ControllerBase
    {

        private readonly IFindFiveNearestBranches service;
        public RestaurantBranchController(IFindFiveNearestBranches service)
        {
            this.service = service;
        }

        [HttpGet(Name = "get-five-nearest-branches")]
        public async Task<IActionResult> Get(double latitude, double longitude)
        {
            try
            {
                var fiveNearestBranches = await service.FindFiveNearestBranches(latitude, longitude);
                return Ok(fiveNearestBranches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}