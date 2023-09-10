using Superkatten.Katministratie.Application.Services;
using Xunit;

namespace Superkatten.Katministratie.Application.Tests.Services;

public class UserServiceTests
{
    [Fact]
    public void Get_UsernameExsist_ReturnsUser()
    {
        // arrange
        const string USERNAME = "Johan";

        var sut = new UserService();

        // act
        var user = sut.Get(USERNAME);

        // assert
        Assert.NotNull(user);
    }

    [Fact]
    public void Get_UsernameDoesNotExsist_ReturnsNull()
    {
        // arrange
        const string USERNAME = "Peter";

        var sut = new UserService();

        // act
        var user = sut.Get(USERNAME);

        // assert
        Assert.Null(user);
    }
}
