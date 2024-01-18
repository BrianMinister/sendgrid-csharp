using Lcp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lcp.Infrastructure.EF.EntityConfigurations
{
    /// <summary>
    ///     Provides configuration for the
    ///     <see cref="Sample"/> entity.
    /// </summary>
    public class SampleConfiguration : BaseEntityConfiguration<Sample>,
                                        IEntityTypeConfiguration<Sample>
    {
        /// <summary>
        ///     Configures the entity type for
        ///     the <see cref="Sample"/> entity.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
        public override void Configure(EntityTypeBuilder<Sample> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasColumnType("nvarchar(50)");
        }
    }
}
