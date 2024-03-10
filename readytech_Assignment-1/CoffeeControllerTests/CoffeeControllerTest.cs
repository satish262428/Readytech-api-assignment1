using Moq;
using NUnit.Framework;
using CoffeeNameSpace;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeNameSpace.Tests
{
    [TestFixture]
    public class CoffeeControllerTests
    {
        [Test]
        public void BrewCoffee_ReturnsOkResult()
        {
            // Arrange
            var handlerMock = new Mock<IRequestHandler>();
            handlerMock.Setup(h => h.HandleRequest()).Returns(new OkResult());
            var controller = new CoffeeController(handlerMock.Object);

            // Act
            var result = controller.BrewCoffee();

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void BrewCoffee_ReturnsNotFoundResult()
        {
            // Arrange
            var handlerMock = new Mock<IRequestHandler>();
            handlerMock.Setup(h => h.HandleRequest()).Returns(new NotFoundResult());
            var controller = new CoffeeController(handlerMock.Object);

            // Act
            var result = controller.BrewCoffee();

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void BrewCoffee_ReturnsInternalServerError()
        {
            // Arrange
            var handlerMock = new Mock<IRequestHandler>();
            handlerMock.Setup(h => h.HandleRequest()).Returns(new StatusCodeResult(500));
            var controller = new CoffeeController(handlerMock.Object);

            // Act
            var result = controller.BrewCoffee();

            // Assert
            Assert.IsInstanceOf<StatusCodeResult>(result);
            Assert.AreEqual(500, ((StatusCodeResult)result).StatusCode);
        }

        // Add more test cases for different scenarios
    }
}
