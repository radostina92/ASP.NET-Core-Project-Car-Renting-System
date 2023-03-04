using CarRentingSystem.Models.Cars;

namespace CarRentingSystem.Models.Api.Cars
{
    public class AllCarsApiRequestModel
    {
        public string BrandSelcted { get; set; }
        public string SearchTerm { get; init; }
        public AllCarsSorting CarsSorting { get; init; }
      

    }
}
