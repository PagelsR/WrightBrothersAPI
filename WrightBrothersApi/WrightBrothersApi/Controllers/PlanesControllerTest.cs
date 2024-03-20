using Xunit;

namespace WrightBrothersApi.Tests.Controllers
{
    public class PlanesControllerTests
    {
        [Fact]
        public void Post_ReturnsCreatedAtAction()
        {
            // Arrange
            var controller = new PlanesController();
            var plane = new Plane { Id = 1, Name = "Boeing 747" };

            // Act
            var result = controller.Post(plane);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }
    }
}