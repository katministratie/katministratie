using Superkatten.Katministratie.Domain.Location;
using Xunit;

namespace Superkatten.Katministratie.Domain.Tests.Location;

public class RefugeTests
{

    [Fact]
    public void Ctor_CreateInstance_InstanceHasCorrectType()
    {
        // arrange

        // act
        var sut = new Refuge();

        // assert
        Assert.True(sut.Type == Shared.LocationType.Refuge);
    }
}
