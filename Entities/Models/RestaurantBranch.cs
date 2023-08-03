using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    [Table("restaurant_branches")]

    public class RestaurantBranch
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Latitude is required")]
        public double Latitude { get; set; }
        [Required(ErrorMessage = "Longitude is required")]
        public double Longitude { get; set; }
    }
}
