using FluentAssertions;
using Moq;
using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Domain.Exceptions;
using Xunit;

namespace Superkatten.Katministratie.Domain.Tests.Entities
{
    public class SuperkatTests
    {
        [Fact]
        public void  CreateUpdatedModel_WithEmptyName_ThrowsException()
        {
            // arrange
            var sut = new Superkat(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTimeOffset>());

            // act
            Action act = () => sut.CreateUpdatedModel(string.Empty);

            // assert
            act.Should().ThrowExactly<DomainException>();
        }


    }
}