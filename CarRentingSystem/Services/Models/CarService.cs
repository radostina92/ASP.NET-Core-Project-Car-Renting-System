using CarRentingSystem.Data;
using CarRentingSystem.Models.Cars;
using CarRentingSystem.Services.Cars;

namespace CarRentingSystem.Services.Models
{
    public class CarService : ICarService
    {
        private readonly CarRentalDbContext data;

        public CarService(CarRentalDbContext data)
        {
            this.data = data;
        }

        public CarQueryServiceModel All(string brand,
            string searchTerm,
            AllCarsSorting carsSorting)
        {
            var carsQuery = this.data.Cars.AsQueryable();

            if (!string.IsNullOrWhiteSpace(brand))
            {
                carsQuery = carsQuery.
                    Where(c => c.Brand == brand);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                carsQuery = carsQuery.Where(
                   c => c.Brand.ToLower().Contains(searchTerm.ToLower())
                   || c.Model.ToLower().Contains(searchTerm.ToLower())
                   || c.Description.ToLower().Contains(searchTerm.ToLower())
                );
            }

            carsQuery = carsSorting switch
            {
                AllCarsSorting.DateCreated => carsQuery.OrderByDescending(c => c.Id),
                AllCarsSorting.Year => carsQuery.OrderByDescending(c => c.Year),
                AllCarsSorting.Brand => carsQuery.OrderByDescending(c => c.Brand),
                AllCarsSorting.Model => carsQuery.OrderByDescending(c => c.Model),
                _ => carsQuery.OrderByDescending(c => c.Id)
            };

            var cars = this.data
                .Cars
                .OrderByDescending(c => c.Id)
                .Select(
                c => new CarServiceModel
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    ImageUrl = c.ImageUrl,
                    Year = c.Year,
                    Category = c.Category.Name

                })
                .ToList();

            return new CarQueryServiceModel
            {
                Cars = cars,
                TotalCars = carsQuery.Count()
            };
        }

        public IEnumerable<string> AllCarBrands()
        {
           return this.data
                .Cars
                .Select(c => c.Brand)
                .Distinct()
                .ToList();
        }
    }
}
