using HotChocolate.Types;

namespace Lcp.Application.Schema.Queries
{
    /// <summary>
    ///     Represents the root query object that can
    ///     be extended by all other query objects.
    /// </summary>
    public class Query : ObjectType
    {
        /// <summary>
        ///     Configures the object type name and
        ///     description for the query object.
        /// </summary>
        /// <param name="descriptor">The object type descriptor.</param>
        protected override void Configure(IObjectTypeDescriptor descriptor) =>
            descriptor.Name("TemplateQuery")
                      .Description("Provides the queries for the template services.");
    }
}
