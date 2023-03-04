using CarRentingSystem.Data;
using CarRentingSystem.Models;
using CarRentingSystem.Models.Cars;
using CarRentingSystem.Models.Home;
using CarRentingSystem.Services.Statistics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarRentingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatisticsService statisticsService;
        private readonly CarRentalDbContext data;

        public HomeController(IStatisticsService statisticsService,
            CarRentalDbContext data)
        {
            this.statisticsService = statisticsService;
            this.data = data;
        }

        public IActionResult Index()
        {
            var totalCars = this.data.Cars.Count();

            var totalUsers = this.data.Users.Count();

            var cars = this.data
                .Cars
                .OrderByDescending(c => c.Id)
                .Select(
                c => new CarIndexViewModel
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    ImageUrl = c.ImageUrl,
                    Year = c.Year

                })
                .Take(3)
                .ToList();

            var statistics = this.statisticsService.Total();

            return View(new IndexViewModel
            {
                TotalCars = statistics.TotalCars,
                TotalUsers = statistics.TotalUsers,
                TotalRents = statistics.TotalRents,
                Cars = cars
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}