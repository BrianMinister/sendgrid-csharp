#nullable disable
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Lcp.Domain.Entities;
using Lcp.Infrastructure.EF.Extensions;
using Lcp.Microsvs.Events;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Lcp.Infrastructure.EF.Contexts
{
    /// <inheritdoc />
    public class TemplateContext : DbContext
    {
        /// <summary>
        ///     Gets or sets the DbSet for Samples.
        /// </summary>
        public DbSet<Sample> Samples { get; set; }

        /// <summary>
        ///     Initializes a new instance of the 
        ///     <see cref="TemplateContext"/> class.
        /// </summary>
        /// <param name="options">Database context options.</param>
        public TemplateContext(DbContextOptions<TemplateContext> options)
            : base(options)
        {
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        /// <summary>
        ///    Saves all changes made to this context to
        ///    the database. The audit fields are set for
        ///    the user derived from the http context. 
        /// </summary>
        /// <param name="context">The http context.</param>
        /// <param name="eventSender">The event sender.</param>
        /// <param name="caller">The caller of this method - filled by the runtime.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The number of state entries written to the database.</returns>
        public async Task<int> SaveChangesAsync<T>(HttpContext context,
                                                   IEventSender eventSender,
                                                   [CallerMemberName] string caller = "",
                                                   CancellationToken cancellationToken = default)
            where T : BaseEntity
        {
            var entries = ChangeTracker.GetChangeTrackerEntries();
            entries.SetAuditFields(context);
            var preSaveStatus = entries.ToDictionary(key => key, value => value.State);
            var result = await base.SaveChangesAsync(true, cancellationToken);
            entries.SendMessages(eventSender, typeof(T).Name, caller, preSaveStatus);
            return result;
        }
    }
}
