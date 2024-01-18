namespace Lcp.Api.Models
{
    /// <summary>
    ///     Represents the Fortify options
    ///     for authentication.
    /// </summary>
    public class FortifyOptions
    {
        /// <summary>
        ///     Gets or sets the authority.
        /// </summary>        
        public string Authority { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the API secret.
        /// </summary>        
        public string ApiSecret { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the API scope.
        /// </summary>
        public string ApiScope { get; set; } = string.Empty;
    }
}
