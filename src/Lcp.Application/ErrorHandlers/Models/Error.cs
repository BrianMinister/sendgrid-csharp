namespace Lcp.Application.ErrorHandlers.Models
{
    /// <summary>
    ///     Represents the custom error for
    ///     passing errors back to clients 
    ///     in a GraphQL mutation.
    /// </summary>
    public class Error
    {
        /// <summary>
        ///      Gets or sets the error message.
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        ///     Gets or sets the error extensions.
        /// </summary>
        public ErrorExtensions? Extensions { get; set; }
    }
}
