using CarRentingSystem.Models.Cars;
using Microsoft.AspNetCore.Mvc;
using CarRentingSystem.Data;
using CarRentingSystem.Data.Models;

namespace CarRentingSystem.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarRentalDbContext data;

        public CarsController(CarRentalDbContext data) => this.data = data;


        public IActionResult Add() => View(new AddCarFormModel
        {
            Categories = this.GetCarCategories()
        });

        public IActionResult All(string brand, string searchTerm, AllCarsSorting sorting)
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

            carsQuery = sorting switch
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
                c => new CarListingViewModel
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    ImageUrl = c.ImageUrl,
                    Year = c.Year,
                    Category = c.Category.Name

                })
                .ToList();

            var carBrands = this.data
                .Cars
                .Select(c => c.Brand)
                .Distinct()
                .ToList();

            return View(new AllCarsQueryModel
            {
                BrandSelcted = brand,
                AllBrands = carBrands,
                SearchTerm = searchTerm,
                CarsSorting = sorting,
                Cars = cars
            });
        }

        [HttpPost]
        public IActionResult Add(AddCarFormModel carFormModel)
        {


            if(!this.data.Categories.Any(c => c.Id == carFormModel.CategoryId))
            {
                this.ModelState.AddModelError(nameof(carFormModel.CategoryId), "Category does not exist!");
            }



            if (!ModelState.IsValid)
            {
                carFormModel.Categories = this.GetCarCategories();
                return View(carFormModel);
            }



            var car = new Car
            {
                Brand = carFormModel.Brand,
                Model = carFormModel.Model,
                Description = carFormModel.Description,
                ImageUrl = carFormModel.ImageUrl,
                Year = carFormModel.Year,
                CategoryId = carFormModel.CategoryId
            };

            this.data.Cars.Add(car);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        } 

        private IEnumerable<CarCategoryViewModel> GetCarCategories()
        => this.data
            .Categories
            .Select(c => new CarCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();
    }
}
