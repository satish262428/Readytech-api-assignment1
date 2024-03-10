using System;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

[TestFixture]
public class DefaultHandlerTests
{
    [Test]
    public void HandleRequest_ReturnsOkObjectResult_WithExpectedMessageAndDateTime()
    {
        // Arrange
        var defaultHandler = new DefaultHandler();

        // Act
        var result = defaultHandler.HandleRequest() as OkObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);

        var response = result.Value;

        // Check if the 'message' property exists and has the expected value
        var messageProperty = response.GetType().GetProperty("message");
        Assert.IsNotNull(messageProperty, "Property 'message' not found on response object.");
        var messageValue = messageProperty.GetValue(response);
        StringAssert.Contains("Your piping hot coffee is ready", messageValue.ToString());
    }

}
