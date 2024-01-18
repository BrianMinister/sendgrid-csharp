using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Lcp.Domain.Entities;
using Lcp.Infrastructure.EF.Contexts;
using System;
using System.Linq;

namespace Lcp.Application.Schema.Queries
{
    /// <summary>
    ///     Provides the queries for the 
    ///     samples.
    /// </summary>
    [ExtendObjectType(Name = "TemplateQuery")]
    public class SampleQuery
    {
        /// <summary>
        ///      Gets a collection of samples.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <returns>The collection of samples.</returns>
        [UseDbContext(typeof(TemplateContext))]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Sample> GetSamples(
            [ScopedService] TemplateContext dbContext) => dbContext.Samples;

        /// <summary>
        ///     Gets a sample for
        ///     the given id.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="id">The sample id.</param>
        /// <returns>The sample.</returns>
        [UseDbContext(typeof(TemplateContext))]
        [UseFirstOrDefault]
        [UseProjection]
        public IQueryable<Sample> GetSampleById(
            [ScopedService] TemplateContext dbContext,
            Guid id
        ) => dbContext.Samples.Where(x => x.Id == id);

        /// <summary>
        ///     Gets a collection of samples given
        ///     a collection of ids.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="ids">The collection of sample ids.</param>
        /// <returns>The collection of samples.</returns>
        [UseDbContext(typeof(TemplateContext))]
        [UseProjection]
        public IQueryable<Sample> GetSamplesByIds(
            [ScopedService] TemplateContext dbContext,
            Guid[] ids
        ) => dbContext.Samples.Where(x => ids.Contains(x.Id));
    }
}
