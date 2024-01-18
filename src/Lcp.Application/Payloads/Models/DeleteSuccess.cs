namespace Lcp.Application.Payloads.Models
{
    /// <summary>
    ///      Represents the success indicator
    ///      of the delete operation.
    /// </summary>
    public record DeleteSuccess(bool Deleted) 
    {
        /// <summary>
        ///     Gets or sets a value indicating 
        ///     whether the delete operation is
        ///     a success.
        /// </summary>
        public bool Deleted { get; } = Deleted;
    }
}
