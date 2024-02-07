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
            result.Value.Should().NotBeEmpty();
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
            result.Value.Should().NotBeNull();
            result.Value!.Id.Should().Be(id);
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

        [Fact]
        public void GetById_ReturnsBadRequest()
        {
            // Arrange
            var id = 0;

            // Act
            var result = _planesController.GetById(id);

            // Assert
            result.Result.Should().BeOfType<BadRequestResult>();
        }

// Search by name term    | Amount of results | Description
// Wright Plane 1 | 1                 | Specific search
// Wright Plane   | 3                 | General search
// wright plane   | 3                 | Case insensitive
//  Wright  Plane | 3                 | Extra spaces

        [Fact]
        public void SearchByName_ReturnsOneResult()
        {
            // Arrange
            var term = "Wright Plane 1";

            // Act
            var result = _planesController.GetByName(term);

            // Assert
            result.Value.Should().HaveCount(1);
        }

        [Fact]
        public void SearchByName_ReturnsThreeResults()
        {
            // Arrange
            var term = "Wright Plane";

            // Act
            var result = _planesController.SearchByName(term);

            // Assert
            result.Value.Should().HaveCount(3);
        }

        [Fact]
        public void SearchByName_ReturnsThreeResultsCaseInsensitive()
        {
            // Arrange
            var term = "wright plane";

            // Act
            var result = _planesController.SearchByName(term);

            // Assert
            result.Value.Should().HaveCount(3);
        }

        [Fact]
        public void SearchByName_ReturnsThreeResultsExtraSpaces()
        {
            // Arrange
            var term = "  Wright  Plane  ";

            // Act
            var result = _planesController.SearchByName(term);

            // Assert
            result.Value.Should().HaveCount(3);
        }
    }
}