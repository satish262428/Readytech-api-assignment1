using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

[TestFixture]
public class FifthCallHandlerTests
{
    [Test]
    public void HandleRequest_Returns503Result_WhenShouldReturn503()
    {
        // Arrange
        var callCountServiceMock = new Mock<ICallCountService>();
        callCountServiceMock.Setup(service => service.ShouldReturn503()).Returns(true);

        var fifthCallHandler = new FifthCallHandler(callCountServiceMock.Object);

        // Act
        var result = fifthCallHandler.HandleRequest() as StatusCodeResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(503, result.StatusCode);
    }

    [Test]
    public void HandleRequest_ReturnsNotFoundResult_WhenShouldNotReturn503()
    {
        // Arrange
        var callCountServiceMock = new Mock<ICallCountService>();
        callCountServiceMock.Setup(service => service.ShouldReturn503()).Returns(false);

        var fifthCallHandler = new FifthCallHandler(callCountServiceMock.Object);

        // Act
        var result = fifthCallHandler.HandleRequest() as NotFoundResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(404, result.StatusCode); // Assuming 404 for NotFoundResult
    }
}
