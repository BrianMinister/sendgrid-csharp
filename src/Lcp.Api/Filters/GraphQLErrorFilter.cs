using HotChocolate;
using Lcp.Application.ErrorHandlers.Models;

namespace Lcp.Api.ErrorHandlers.Filters
{
    /// <summary>
    ///     Provides a filter for handling exceptions.
    /// </summary>
    public class GraphQLErrorFilter : IErrorFilter
    {
        /// <summary>
        ///     Formats the error object of
        ///     unexpected exceptions.
        /// </summary>
        /// <param name="error">The error object.</param>
        /// <returns>The formatted error object.</returns>
        public IError OnError(IError error)
        {
            if (error.Exception != null)
                return error
                    .WithMessage(error.Exception.Message)
                    .WithCode(ErrorCode.UNEXPECTED_ERROR);

            return error;
        }
    }
}
