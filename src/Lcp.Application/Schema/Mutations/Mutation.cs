using HotChocolate.Types;

namespace Lcp.Application.Schema.Mutations
{
    /// <summary>
    ///     Represents the root mutation object that can
    ///     be extended by all other mutation objects.
    /// </summary>
    public class Mutation : ObjectType
    {
        /// <summary>
        ///     Configures the object type name and
        ///     description for the mutation object.
        /// </summary>
        /// <param name="descriptor">The object type descriptor.</param>
        protected override void Configure(IObjectTypeDescriptor descriptor) =>
            descriptor.Name("TemplateMutation")
                      .Description("Provides the mutations for the template services.");
    }
}
