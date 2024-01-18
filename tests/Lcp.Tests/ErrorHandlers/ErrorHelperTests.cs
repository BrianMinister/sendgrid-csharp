using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Lcp.Application.ErrorHandlers.Helpers;
using Lcp.Application.ErrorHandlers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Lcp.Tests.ExceptionHandlers
{
    /// <summary>
    ///     Provides the unit tests for the
    ///     error helpers.
    /// </summary>
    public class ErrorHelperTests
    {
        [Fact]
        public void FormatErrors_WithValidationResult_ReturnsErrors()
        {
            //Arrange
            var errors = new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("Name", "Validation Error", "test")
                {
                    ErrorCode = "VALIDATION_ERROR",
                    Severity = Severity.Error
                }
            });

            //Act
            var result = errors.FormatErrors().First();

            //Assert
            result.Message.Should().Be("Validation Error");
            result.Extensions.ErrorCode.Should().Be("VALIDATION_ERROR");
            result.Extensions.PropertyName.Should().Be("Name");
            result.Extensions.AttemptedValue.Should().Be("test");
            result.Extensions.Severity.Should().Be("Error");
        }

        [Fact]
        public void GetIdNotFound_WithInputId_ReturnsErrors()
        {
            //Arrange
            var id = Guid.NewGuid();

            //Act
            var result = ErrorHelper.GetIdNotFoundError(id).First();

            //Assert
            result.Message.Should().Be("'Id' does not exist.");
            result.Extensions.ErrorCode.Should().Be(ErrorCode.ID_NOT_FOUND);
            result.Extensions.PropertyName.Should().Be("Id");
            result.Extensions.AttemptedValue.Should().Be(id.ToString());
            result.Extensions.Severity.Should().Be("Error");
        }
    }
}
