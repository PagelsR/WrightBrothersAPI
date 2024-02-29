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
        public void Put_UpdatesPlane()
        {
            // Arrange
            var id = 1;
            var updatedPlane = new Plane
            {
                Id = 1,
                Name = "Updated Plane",
                Year = 2022,
                Description = "An updated plane.",
                RangeInKm = 1000
            };

            // Act
            var result = _planesController.Put(id, updatedPlane);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public void Put_ReturnsNotFound_WhenPlaneDoesNotExist()
        {
            // Arrange
            var id = 3;
            var updatedPlane = new Plane
            {
                Id = 3,
                Name = "Updated Plane",
                Year = 2022,
                Description = "An updated plane.",
                RangeInKm = 1000
            };

            // Act
            var result = _planesController.Put(id, updatedPlane);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        

    }
}