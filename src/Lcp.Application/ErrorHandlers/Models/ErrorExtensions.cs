namespace Lcp.Application.ErrorHandlers.Models
{
    /// <summary>
    ///     Represents the error
    ///     extensions.
    /// </summary>
    public class ErrorExtensions
    {
        /// <summary>
        ///     Gets or sets the error code.
        /// </summary>
        public string? ErrorCode { get; set; } = string.Empty;
        /// <summary>
        ///     Gets or sets the property name.
        /// </summary>
        public string? PropertyName { get; set; } = string.Empty;
        /// <summary>
        ///     Gets or sets the attempted value.
        /// </summary>
        public string? AttemptedValue { get; set; } = string.Empty;
        /// <summary>
        ///     Gets or sets the severity.
        /// </summary>
        public string? Severity { get; set; } = string.Empty;
    }
}
