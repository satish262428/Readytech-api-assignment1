using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

[TestFixture]
public class RequestHandlerTests
{
    [Test]
    public void HandleRequest_ReturnsNotFoundResult_ByDefault()
    {
        // Arrange
        var handlerMock = new Mock<IRequestHandler>();
        handlerMock.Setup(handler => handler.HandleRequest()).Returns(new NotFoundResult());
        var handler = handlerMock.Object;


        // Act
        var result = handler.HandleRequest();

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public void SetSuccessor_ReturnsSuccessor_WhenSet()
    {
        // Arrange
        var handlerMock = new Mock<IRequestHandler>();
        var successorMock = new Mock<IRequestHandler>();
        handlerMock.Setup(handler => handler.SetSuccessor(successorMock.Object)).Returns(successorMock.Object);
        var handler = handlerMock.Object;

        // Act
        var result = handler.SetSuccessor(successorMock.Object);

        // Assert
        Assert.AreSame(successorMock.Object, result);
    }
}
