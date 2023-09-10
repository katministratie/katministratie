using Superkatten.Katministratie.Domain.Exceptions;
using Xunit;

namespace Superkatten.Katministratie.Domain.Tests;

public class UserTests
{
    [Fact]
    public void Ctor_WithValidParameters_InstanceCreated()
    {
        // arrange
        const string VALID_USER_NAME = "Johan";
        const string VALID_PASSWORD_HASH = "1234567890";

        // act
        var sut = new User(VALID_USER_NAME, VALID_PASSWORD_HASH);

        // assert
        Assert.NotNull(sut);
        Assert.Equal("Johan", sut.Username);
        Assert.Equal("1234567890", sut.PasswordHash);
        Assert.Empty(sut.Name);
        Assert.Empty(sut.Email);
    }

    [Fact]
    public void Update_WithValidParameters_InstanceUpdated()
    {
        // arrange
        const string VALID_USER_NAME = "Johan";
        const string VALID_PASSWORD_HASH = "1234567890";

        // act
        var sut = new User(VALID_USER_NAME, VALID_PASSWORD_HASH);

        // assert
        Assert.NotNull(sut);
        Assert.Equal("Johan", sut.Username);
        Assert.Equal("1234567890", sut.PasswordHash);
        Assert.Empty(sut.Name);
        Assert.Empty(sut.Email);
    }

    [Theory]
    [InlineData("", "123123")]
    [InlineData(null, "123123")]
    public void Ctor_WithInvalidUsername_ThrowsEmptyUsernameException(string username, string passwordHash)
    {
        // arrange

        // act
        var act = () => new User(username, passwordHash);

        // assert
        Assert.Throws<EmptyUsernameException>(act);
    }

    [Theory]
    [InlineData("Johan", "")]
    [InlineData("Johan", null)]
    public void Ctor_WithInvalidPassword_ThrowsEmptyPasswordException(string username, string passwordHash)
    {
        // arrange

        // act
        var act = () => new User(username, passwordHash);

        // assert
        Assert.Throws<EmptyPasswordException>(act);
    }
}
