using WrightBrothersApi.Controllers;
using WrightBrothersApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WrightBrothersApi.Tests.Controllers
{
    public class PlanesControllerTests
    {
        private readonly ILogger<PlanesController> _logger;
        private readonly PlanesController _planesController;

        public PlanesControllerTests()
        {
            _logger = Substitute.For<ILogger<PlanesController>>();
            _planesController = new PlanesController(_logger);
        }

        [Fact]
        public void GetAll_ReturnsListOfPlanes()
        {
            // Act
            var result = _planesController.GetAll();

            // Assert
            var okObjectResult = (OkObjectResult)result.Result!;
            var returnedPlanes = (List<Plane>)okObjectResult.Value!;
            returnedPlanes.Should().NotBeEmpty();
        }

        [Fact]
        public void Post_AddsPlaneAndReturnsCreated()
        {
            // Arrange
            var newPlane = new Plane
            {
                Id = 3,
                Name = "Test Plane",
                Year = 2022,
                Description = "A test plane.",
                RangeInKm = 1000
            };

            // Act
            var result = _planesController.Post(newPlane);

            // Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();

            var createdAtActionResult = (CreatedAtActionResult)result.Result!;
            var returnedPlane = (Plane)createdAtActionResult.Value!;
            returnedPlane.Should().BeEquivalentTo(newPlane);
        }

        [Fact]
        public void GetById_ReturnsPlane()
        {
            // Arrange
            var id = 1;

            // Act
            var result = _planesController.GetById(id);

            // Assert
            var okObjectResult = (OkObjectResult)result.Result!;
            var returnedPlane = (Plane)okObjectResult.Value!;
            returnedPlane.Should().NotBeNull();
        }

        [Fact]
        public void GetById_ReturnsNotFound()
        {
            // Arrange
            var id = 100;

            // Act
            var result = _planesController.GetById(id);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

// Search by name term  | Amount of results | Test Description
// Wright Flyer II      | 1                 | Specific search
// Wright               | 3                 | General search
// wright flyer         | 2                 | Case insensitive
//  Wright  flyer       | 2                 | Extra spaces

        [Theory]
        [InlineData("Wright Flyer II", 1)]
        [InlineData("Wright", 3)]
        [InlineData("wright flyer", 2)]
        [InlineData(" Wright  flyer ", 2)]
        public void Search_ReturnsPlanes(string term, int expectedAmountOfResults)
        {
            // Act
            var result = _planesController.SearchByName(term);

            // Assert
            var okObjectResult = (OkObjectResult)result.Result!;
            var returnedPlanes = (List<Plane>)okObjectResult.Value!;
            returnedPlanes.Should().HaveCount(expectedAmountOfResults);
        }

    }
}