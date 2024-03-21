using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace WrightBrothersApi.Tests.Controllers
{
    public class PlanesControllerTests
    {
        [Fact]
        public void Post_ReturnsBadRequest_WhenPlaneWithNameAlreadyExists()
        {
            // Arrange
            var controller = new PlanesController();
            var existingPlane = new Plane { Name = "ExistingPlane" };
            controller.Planes.Add(existingPlane);
            var newPlane = new Plane { Name = "ExistingPlane" };

            // Act
            var result = controller.Post(newPlane);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Plane with this name already exists", badRequestResult.Value);
        }

        [Fact]
        public void Post_ReturnsCreatedAtAction_WhenPlaneIsAddedSuccessfully()
        {
            // Arrange
            var controller = new PlanesController();
            var newPlane = new Plane { Name = "NewPlane" };

            // Act
            var result = controller.Post(newPlane);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(PlanesController.GetById), createdAtActionResult.ActionName);
            Assert.Equal(new { id = newPlane.Id }, createdAtActionResult.RouteValues);
            Assert.Equal(newPlane, createdAtActionResult.Value);
        }
    }
}