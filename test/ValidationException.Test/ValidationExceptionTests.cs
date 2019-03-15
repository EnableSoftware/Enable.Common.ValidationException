using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using AutoFixture;
using Xunit;

namespace Enable.Common
{
    public class ValidationExceptionTests
    {
        public ValidationExceptionTests()
        {
            Fixture = new Fixture();
        }

        public IFixture Fixture { get; private set; }

        [Fact]
        public void Should_Be_An_Exception_Type()
        {
            // Act
            var sut = new ValidationException();

            // Assert
            Assert.IsAssignableFrom<Exception>(sut);
        }

        [Fact]
        public void Should_Have_Default_Constructor()
        {
            // Act
            var sut = new ValidationException();

            // Assert
            Assert.NotNull(sut);
        }

        [Fact]
        public void Can_Be_Initialised_With_Exception_Message()
        {
            // Arrange
            var message = Fixture.Create<string>();

            // Act
            var sut = new ValidationException(message);

            // Assert
            Assert.Equal(message, sut.Message);
        }

        [Fact]
        public void Can_Be_Initialised_With_Exception_Message_And_Inner_Exception()
        {
            // Arrange
            var message = Fixture.Create<string>();
            var innerException = Fixture.Create<Exception>();

            // Act
            var sut = new ValidationException(message, innerException);

            // Assert
            Assert.Equal(message, sut.Message);
            Assert.Equal(innerException, sut.InnerException);
        }

        [Fact]
        public void Can_Be_Initialised_With_Validation_Messages()
        {
            // Arrange
            var validationMessages = Fixture.Create<IEnumerable<string>>();

            // Act
            var sut = new ValidationException(validationMessages);

            // Assert
            Assert.Equal(validationMessages, sut.ValidationMessages);
        }

        [Fact]
        public void Can_Be_Initialised_With_Message_And_Validation_Messages()
        {
            // Arrange
            var message = Fixture.Create<string>();
            var validationMessages = Fixture.Create<IEnumerable<string>>();

            // Act
            var sut = new ValidationException(message, validationMessages);

            // Assert
            Assert.Equal(message, sut.Message);
            Assert.Equal(validationMessages, sut.ValidationMessages);
        }

        [Fact]
        public void Validation_Messages_Should_Not_Be_Null_Using_Default_Ctor()
        {
            // Act
            var sut = new ValidationException();

            // Assert
            Assert.NotNull(sut.ValidationMessages);
        }

        [Fact]
        public void Validation_Messages_Should_Be_Empty_Using_Default_Ctor()
        {
            // Act
            var sut = new ValidationException();

            // Assert
            Assert.Empty(sut.ValidationMessages);
        }

        [Fact]
        public void Validation_Messages_Should_Be_Null_Using_Message_Ctor()
        {
            // Arrange
            var message = Fixture.Create<string>();

            // Act
            var sut = new ValidationException(message);

            // Assert
            Assert.NotNull(sut.ValidationMessages);
        }

        [Fact]
        public void Validation_Messages_Should_Be_Empty_Using_Message_Ctor()
        {
            // Arrange
            var message = Fixture.Create<string>();

            // Act
            var sut = new ValidationException(message);

            // Assert
            Assert.Empty(sut.ValidationMessages);
        }

        [Fact]
        public void Validation_Messages_Should_Be_Null_Using_Message_And_Inner_Exception_Ctor()
        {
            // Arrange
            var message = Fixture.Create<string>();
            var innerException = Fixture.Create<Exception>();

            // Act
            var sut = new ValidationException(message, innerException);

            // Assert
            Assert.NotNull(sut.ValidationMessages);
        }

        [Fact]
        public void Validation_Messages_Should_Be_Empty_Using_Message_And_Inner_Exception_Ctor()
        {
            // Arrange
            var message = Fixture.Create<string>();
            var innerException = Fixture.Create<Exception>();

            // Act
            var sut = new ValidationException(message, innerException);

            // Assert
            Assert.Empty(sut.ValidationMessages);
        }

        [Fact]
        public void ToString_Result_Should_Contain_Message_And_ValidationMessages()
        {
            // Arrange
            var message = Fixture.Create<string>();
            var validationMessages = Fixture.Create<IEnumerable<string>>();
            var sut = new ValidationException(message, validationMessages);

            // Act
            var result = sut.ToString();

            // Assert
            Assert.Contains(message, result);

            foreach (var validationMessage in validationMessages)
            {
                Assert.Contains(validationMessage, result);
            }
        }

        [Fact]
        public void Should_Roundtrip_Message_During_Serialization()
        {
            // Arrange
            var formatter = new BinaryFormatter();
            var message = Fixture.Create<string>();
            var sut = new ValidationException(message);

            // Act
            ValidationException result;

            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, sut);
                stream.Position = 0;
                result = (ValidationException)formatter.Deserialize(stream);
            }

            // Assert
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void Should_Roundtrip_ValidationMessages_During_Serialization()
        {
            // Arrange
            var formatter = new BinaryFormatter();

            var validationMessages = new[]
            {
                Fixture.Create<string>(),
                Fixture.Create<string>()
            };

            var sut = new ValidationException(validationMessages);

            // Act
            ValidationException result;

            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, sut);
                stream.Position = 0;
                result = (ValidationException)formatter.Deserialize(stream);
            }

            // Assert
            Assert.Equal(validationMessages, result.ValidationMessages);
        }
    }
}
