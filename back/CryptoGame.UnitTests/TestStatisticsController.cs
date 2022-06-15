using CryptoGame.Controllers;
using CryptoGame.Managers;
using CryptoGame.Repositories;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CryptoGame.UnitTests
{
    public class TestStatisticsController
    {
        [Fact]
        public async Task TestStockMinute()
        {
            // Arrange
            IStatisticsRepository repository = new StatisticsRepository();
            IStatisticsManager manager = new StatisticsManager(repository);
            StatisticsController controller = new StatisticsController(manager);

            // Act
            var result = (OkObjectResult)await controller.GetStatistics("stock", "TSLA", "minute", DateTime.Parse("2020-01-01"), DateTime.Parse("2023-01-01"));

            // Assert
            result.StatusCode.ShouldBe(200);
        }

    }
}
