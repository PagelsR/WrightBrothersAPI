// BEGIN: TestDelete
[TestMethod]
public void TestDelete()
{
    // Arrange
    var controller = new PlanesController();
    var existingPlane = new Plane { Id = 1 };
    controller.Planes.Add(existingPlane);

    // Act
    var result = controller.Delete(1);

    // Assert
    Assert.IsInstanceOfType(result, typeof(NoContentResult));
    Assert.IsFalse(controller.Planes.Contains(existingPlane));
}
// END: TestDelete