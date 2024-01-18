using HotChocolate;
using System;

namespace Lcp.Domain.Entities
{
    /// <summary>
    ///     Represents an entity.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        ///     Gets or sets the entity
        ///     unique identifier.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        ///     Gets or sets the created date.
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        ///     Gets or sets the created
        ///     by user name.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;
        /// <summary>
        ///     Gets or sets the modified date.
        /// </summary>
        public DateTime ModifiedDate { get; set; }
        /// <summary>
        ///     Gets or sets the modified
        ///     by user name.
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;
        /// <summary>
        ///     Gets or sets a value indicating
        ///     whether the entity has been
        ///     deleted.
        /// </summary>
        [GraphQLIgnore]
        public bool IsDeleted { get; set; }
    }
}
