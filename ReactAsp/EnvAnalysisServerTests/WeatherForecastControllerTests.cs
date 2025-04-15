// File: WeatherForecastControllerTests.cs
using EnvAnalysisApp.Server.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace EnvAnalysisApp.Tests.Controllers
{
    [TestFixture]
    public class WeatherForecastControllerTests
    {
        private WeatherForecastController _controller;
        private Mock<ILogger<WeatherForecastController>> _mockLogger;

        [SetUp]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger<WeatherForecastController>>();
            _controller = new WeatherForecastController(_mockLogger.Object);
        }

        [Test]
        public void Get_ReturnsFiveWeatherForecasts()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.NotNull(result);
            var forecasts = result.ToArray();
            Assert.That(forecasts.Length, Is.EqualTo(5));

            foreach (var forecast in forecasts)
            {
                Assert.That(forecast.TemperatureC, Is.InRange(-20, 55));
                Assert.That(forecast.Summary, Is.AnyOf(
                    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
                ));
            }
        }
    }
}
