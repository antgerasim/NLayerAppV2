

namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork.Mapping
{

    using System.Data.Entity.ModelConfiguration;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;

    /// <summary>
    /// The picture entity type configuration
    /// </summary>
    class PictureEntityConfiguration
        :EntityTypeConfiguration<Picture>
    {
        /// <summary>
        /// Create a new instance of picture entity type configuration
        /// </summary>
        public PictureEntityConfiguration()
        {
            this.HasKey(p => p.Id);

            this.Property(p => p.RawPhoto)
                .IsOptional();

            this.ToTable("Customers");
        }
    }
}
