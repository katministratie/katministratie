using Moq;
using Superkatten.Katministratie.Application.Utils;
using System;
using Xunit;

namespace Superkatten.Katministratie.Domain.Tests;

public class SuperkatTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(250)]
    [InlineData(500)]
    public void Ctor_WithValidNumber_InstanceCreated(int number)
    {
        // arrange
        const int YEAR = 2023;
        const int MONTH = 10;
        const int DAY = 23;

        var entered = new DateTime(YEAR, MONTH, DAY);

        // act
        var sut = new Superkat(number, entered);

        // assert
        Assert.Equal(number, sut.Number);
        Assert.Equal(new DateTime(YEAR, MONTH, DAY), sut.Entered);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(501)]
    [InlineData(234523)]
    public void Ctor_WithInvalidNumber_ThrowsOutOfRangeException(int number)
    {
        // arrange
        const int YEAR = 2023;
        const int MONTH = 10;
        const int DAY = 23;

        var entered = new DateTime(YEAR, MONTH, DAY);

        // act
        void act() => new Superkat(number, entered);

        // assert
        Assert.Throws<ArgumentOutOfRangeException>(act);
    }
}