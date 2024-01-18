namespace Lcp.Application.ErrorHandlers.Models
{
    /// <summary>
    ///     Provides the error codes for
    ///     input and server errors.
    /// </summary>
    public static class ErrorCode
    {
        /// <summary>
        ///     Error code associated with
        ///     objects that do not exist for
        ///     the given id.
        /// </summary>
        public static readonly string ID_NOT_FOUND = "ID_NOT_FOUND";

        /// <summary>
        ///     Error code associated with
        ///     duplicate values.
        /// </summary>
        public static readonly string DUPLICATE_ERROR = "DUPLICATE_ERROR";

        /// <summary>
        ///     Error code associated with
        ///     unexpected errors.
        /// </summary>
        public static readonly string UNEXPECTED_ERROR = "UNEXPECTED_ERROR";
    }
}
