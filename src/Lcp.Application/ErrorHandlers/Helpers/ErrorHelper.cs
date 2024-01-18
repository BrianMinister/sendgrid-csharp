using FluentValidation.Results;
using Lcp.Application.ErrorHandlers.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lcp.Application.ErrorHandlers.Helpers
{
    /// <summary>
    ///     Provides a helper class for
    ///     formatting validation errors.
    /// </summary>
    public static class ErrorHelper
    {
        /// <summary>
        ///     Formats validation errors returned
        ///     by the FluentValidation validator.
        /// </summary>
        /// <param name="result">The validation result.</param>
        /// <returns>The collection of errors.</returns>
        public static IEnumerable<Error> FormatErrors(this ValidationResult result) =>
            result.Errors.Select(e => new Error
            {
                Message = e.ErrorMessage,
                Extensions = new ErrorExtensions
                {
                    ErrorCode = e.ErrorCode,
                    PropertyName = e.PropertyName,
                    AttemptedValue = e.AttemptedValue.ToString(),
                    Severity = e.Severity.ToString()
                }
            });

        /// <summary>
        ///     Formats the validation error for 
        ///     the ID_NOT_FOUND error code.
        /// </summary>
        /// <param name="id">The input id.</param>
        /// <returns>The collection of errors.</returns>
        public static IEnumerable<Error> GetIdNotFoundError(Guid id) =>
            new Error[]
            {
                new Error
                {
                    Message = "'Id' does not exist.",
                    Extensions = new ErrorExtensions
                    {
                        ErrorCode = ErrorCode.ID_NOT_FOUND,
                        PropertyName = "Id",
                        AttemptedValue = id.ToString(),
                        Severity = "Error"
                    }
                }
            };
    }
}
