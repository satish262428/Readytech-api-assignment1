using Moq;
using NUnit.Framework;
using StackExchange.Redis;
using System;

[TestFixture]
public class CallCountServiceTests
{
    [Test]
    public void ShouldReturn503_OnFifthCall()
    {
        // Arrange
        var redisConnectionMock = new Mock<IConnectionMultiplexer>();
        var redisDatabaseMock = new Mock<IDatabase>();

        redisConnectionMock.Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(redisDatabaseMock.Object);

        var callCountService = new CallCountService(redisConnectionMock.Object);

        // Act and Assert
        for (int i = 1; i <= 4; i++)
        {
            Assert.IsFalse(callCountService.ShouldReturn503());
            //Assert.AreEqual(i, callCountService.GetBrewCoffeeCallCount());
        }

        Assert.IsTrue(callCountService.ShouldReturn503());
        //  Assert.AreEqual(0, callCountService.GetBrewCoffeeCallCount());
    }

    [Test]
    public void GetBrewCoffeeCallCount_ReturnsCorrectCountFromRedis()
    {
        // Arrange
        var redisConnectionMock = new Mock<IConnectionMultiplexer>();
        var redisDatabaseMock = new Mock<IDatabase>();


        redisConnectionMock.Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(redisDatabaseMock.Object);
        // Use this (explicitly providing values for all parameters):
        redisDatabaseMock
            .Setup(x => x.StringGet(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
            .Returns((RedisKey key, CommandFlags flags) => new RedisValue("42"));

        var callCountService = new CallCountService(redisConnectionMock.Object);

        // Act
        int result = callCountService.GetBrewCoffeeCallCount();

        // Assert
        Assert.AreEqual(42, result);
    }

    [Test]
    public void GetBrewCoffeeCallCount_ThrowsExceptionWhenInvalidValueInRedis()
    {
        // Arrange
        var redisConnectionMock = new Mock<IConnectionMultiplexer>();
        var redisDatabaseMock = new Mock<IDatabase>();


        redisConnectionMock.Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(redisDatabaseMock.Object);
        redisDatabaseMock.Setup(x => x.StringGet(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
            .Returns(new RedisValue("invalid"));

        var callCountService = new CallCountService(redisConnectionMock.Object);

        // Act and Assert
        Assert.Throws<InvalidOperationException>(() => callCountService.GetBrewCoffeeCallCount());
    }
}
