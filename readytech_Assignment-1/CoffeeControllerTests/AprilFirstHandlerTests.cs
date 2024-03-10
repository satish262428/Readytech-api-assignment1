using System;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace YourNamespace.Tests
{
    [TestFixture]
    public class AprilFirstHandlerTests
    {
        [Test]
        public void HandleRequest_ReturnsNotFoundResult_OnOtherDays()
        {
            // Arrange
            var aprilFirstHandler = new AprilFirstHandler();

            // Mocking a date that is not April 1st
            SystemDateTime.SetCustomDateTime(new DateTime(DateTime.UtcNow.Year, 5, 1));

            try
            {
                // Act
                var result = aprilFirstHandler.HandleRequest();

                // Assert
                Assert.IsNotNull(result);
                Assert.IsInstanceOf<NotFoundResult>(result);
            }
            finally
            {
                // Reset the mocked date
                SystemDateTime.ResetCustomDateTime();
            }
        }

        // Add more test cases as needed
    }
}

public static class SystemDateTime
{
    private static DateTime? customDateTime;

    public static DateTime UtcNow => customDateTime ?? DateTime.UtcNow;

    public static void SetCustomDateTime(DateTime customValue)
    {
        customDateTime = customValue;
    }

    public static void ResetCustomDateTime()
    {
        customDateTime = null;
    }
}