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
            var sut = new Superkat(
                It.IsAny<int>(), 
                It.IsAny<string>(), 
                It.IsAny<DateTimeOffset>(), 
                It.IsAny<int>()
            );

            // act
            Action act = () => sut.CreateUpdatedModel(string.Empty, It.IsAny<int>());

            // assert
            act.Should().ThrowExactly<DomainException>();
        }


    }
}