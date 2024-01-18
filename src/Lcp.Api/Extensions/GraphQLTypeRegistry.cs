using HotChocolate.Types;
using Lcp.Api.ErrorHandlers.Filters;
using Lcp.Api.Logging;
using Lcp.Application.Schema.DataLoaders;
using Lcp.Application.Schema.Mutations;
using Lcp.Application.Schema.Queries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Lcp.Api.Extensions
{
    /// <summary>
    ///     Provides the registry for the 
    ///     GraphQL types.
    /// </summary>
    public static class GraphQLTypeRegistry
    {
        /// <summary>
        ///     Adds the configuration and type registration
        ///     for HotChocolate GraphQL.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddHotChocolate(this IServiceCollection services)
        {
            services.AddGraphQLServer()
                    .ModifyRequestOptions(o => o.IncludeExceptionDetails = false)
                    .AddAuthorization()
                    .AddType(new UuidType('D'))
                    .AddQueryType<Query>()
                    .AddTypeExtension<SampleQuery>()
                    .AddMutationType<Mutation>()
                    .AddTypeExtension<SampleMutation>()
                    .AddDataLoader<SampleByIdDataLoader>()
                    .AddFiltering()
                    .AddSorting()
                    .AddProjections()
                    .AddDiagnosticEventListener(
                        sp => new ConsoleQueryLogger(sp.GetApplicationService<ILogger<ConsoleQueryLogger>>())
                    )
                    .AddErrorFilter<GraphQLErrorFilter>();

            return services;
        }
    }
}
