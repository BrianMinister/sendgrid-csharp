using GreenDonut;
using Lcp.Domain.Entities;
using Lcp.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lcp.Application.Schema.DataLoaders
{
    /// <summary>
    ///     Provides cache for loading the 
    ///     sample data by id.
    /// </summary>
    public class SampleByIdDataLoader : BatchDataLoader<Guid, Sample?>
    {
        private readonly IDbContextFactory<TemplateContext> _dbContextFactory;

        /// <summary>
        ///     Initializes a new instance of the 
        ///     <see cref="SampleByIdDataLoader"/> 
        ///     class.
        /// </summary>
        /// <param name="batchScheduler">Not used, passed thru to base.</param>
        /// <param name="dbContextFactory">The database context factory.</param>
        public SampleByIdDataLoader(
            IBatchScheduler batchScheduler,
            IDbContextFactory<TemplateContext> dbContextFactory) : base(batchScheduler) =>
                _dbContextFactory = dbContextFactory;

        /// <summary>
        ///     Implementation override 
        ///     of LoadBatchAsync.
        /// </summary>
        /// <param name="keys">The collection of ids for the data load.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of sample key/value pairs.</returns>
        protected override async Task<IReadOnlyDictionary<Guid, Sample?>> LoadBatchAsync(
            IReadOnlyList<Guid> keys,
            CancellationToken cancellationToken)
        {
            await using var context = _dbContextFactory.CreateDbContext();

            return await context.Samples
                                .Where(x => keys.Contains(x.Id))
                                .ToDictionaryAsync(x => x.Id, cancellationToken);
        }
    }
}
