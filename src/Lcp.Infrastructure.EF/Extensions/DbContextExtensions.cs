using Lcp.Domain.Entities;
using Lcp.Microsvs.Events;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lcp.Infrastructure.EF.Extensions
{
    /// <summary>
    ///     Provides extension methods for 
    ///     performing database context 
    ///     operations.
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        ///     Sets the audit fields on the
        ///     given change tracker entries.
        /// </summary>
        /// <param name="objects">The collection of changed objects.</param>
        /// <param name="httpContext">The http context.</param>
        public static void SetAuditFields(this List<EntityEntry> objects, HttpContext? httpContext) =>
            objects.ForEach(
                e =>
                {
                    var date = DateTime.UtcNow;
                    var username = httpContext?.User
                                              .FindFirst("user_name")
                                              ?
                                              .Value
                                   ?? "system@lcptracker.com";

                    ((BaseEntity)e.Entity).ModifiedBy = username;
                    ((BaseEntity)e.Entity).ModifiedDate = date;

                    if (e.State == EntityState.Added)
                    {
                        ((BaseEntity)e.Entity).CreatedBy = username;
                        ((BaseEntity)e.Entity).CreatedDate = date;
                    }
                }
            );

        /// <summary>
        ///     Send a message to the named category 
        ///     for the given change tracker entities
        /// </summary>
        /// <param name="objects">The collection of changed objects.</param>
        /// <param name="sender">The event sender.</param>
        /// <param name="category">The event category.</param>
        /// <param name="eventName">The event name.</param>
        /// <param name="preSaveStatus">The entity state of the objects before SaveChanges was called.</param>
        public static void SendMessages(this List<EntityEntry> objects, IEventSender? sender, string category, string eventName, Dictionary<EntityEntry, EntityState> preSaveStatus)
        {
            if (sender == null)
                return;
            foreach (var entity in objects)
            {
                var entityType = entity.Entity.GetType();
                if (entityType.FullName.StartsWith("Castle.Proxies"))
                    entityType = entityType.BaseType;
                sender.SendMessage(
                    eventName,
                    new EventMessageBase()
                    {
                        Type = entityType?.Name ?? category,
                        Data = entity.Entity,
                        State = preSaveStatus[entity].ToObjectState()
                    }
                );
            }
        }

        /// <summary>
        ///     Returns the collection of objects 
        ///     changed by data operations.
        /// </summary>
        /// <param name="changeTracker">The change tracker.</param>
        /// <returns>The collection of changed objects.</returns>
        public static List<EntityEntry> GetChangeTrackerEntries(this ChangeTracker changeTracker) =>
            changeTracker.Entries()
                         .Where(
                             e => e.State == EntityState.Added
                                  || e.State == EntityState.Modified
                         )
                         .ToList();
    }
}
