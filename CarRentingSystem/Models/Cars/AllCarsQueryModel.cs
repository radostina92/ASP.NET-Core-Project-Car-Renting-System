using CarRentingSystem.Services.Models;
using System.ComponentModel.DataAnnotations;

namespace CarRentingSystem.Models.Cars
{
    public class AllCarsQueryModel
    {
        public string BrandSelcted { get; set; }
        public IEnumerable<string> AllBrands { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }
        public AllCarsSorting CarsSorting { get; set; }
        public IEnumerable<CarServiceModel> Cars  { get; set; } 
    }
}
