using CarRentingSystem.Models.Cars;
using CarRentingSystem.Services.Models;

namespace CarRentingSystem.Services.Cars
{
    public interface ICarService
    {
        CarQueryServiceModel All(string brand,
            string searchTerm,
            AllCarsSorting carsSorting);

        IEnumerable<string> AllCarBrands();
    }
}
