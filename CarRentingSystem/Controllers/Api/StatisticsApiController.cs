using CarRentingSystem.Data;

using CarRentingSystem.Services.Models;
using CarRentingSystem.Services.Statistics;
using Microsoft.AspNetCore.Mvc;

namespace CarRentingSystem.Controllers.Api
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsApiController(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        [HttpGet]
        public StatisticsServiceModel GetStatistics()
        {
            return this.statisticsService.Total();

        }
    }
}
