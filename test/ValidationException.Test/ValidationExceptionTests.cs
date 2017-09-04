using System;
using System.Linq;
using Xunit;

namespace Enable.Common
{
    public class ValidationExceptionTests
    {
        [Fact]
        public void Should_Have_Default_Constructor()
        {
            // Act
            var result = new ValidationException();

            // Assert
            Assert.NotNull(result);
        }
    }
}
