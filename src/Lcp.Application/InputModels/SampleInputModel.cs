namespace Lcp.Application.InputModels
{
    /// <summary>
    ///     Represents the sample input model.
    ///     GraphQL does not accept the same
    ///     type for inputs and outputs.
    /// </summary>
    public record SampleInputModel : BaseInputModel
    {
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; init; } = string.Empty;
    }
}
