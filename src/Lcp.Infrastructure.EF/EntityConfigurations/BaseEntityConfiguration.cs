using Lcp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lcp.Infrastructure.EF.EntityConfigurations
{
    /// <summary>
    ///     Provides configuration for the
    ///     <see cref="BaseEntity"/> class.
    /// </summary>
    public class BaseEntityConfiguration<T> where T : BaseEntity
    {
        /// <summary>
        ///     Configures the entity type for
        ///     the <see cref="BaseEntity"/> class.
        ///     <param name="builder">The entity type builder.</param>
        /// </summary>
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                   .HasDefaultValueSql("NEWID()")
                   .ValueGeneratedOnAdd();

            builder.Property(a => a.CreatedDate);

            builder.Property(a => a.CreatedBy)
                   .IsRequired()
                   .HasColumnType("nvarchar(320)");

            builder.Property(a => a.ModifiedDate)
                   .IsConcurrencyToken();

            builder.Property(a => a.ModifiedBy)
                   .IsRequired()
                   .HasColumnType("nvarchar(320)");

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
