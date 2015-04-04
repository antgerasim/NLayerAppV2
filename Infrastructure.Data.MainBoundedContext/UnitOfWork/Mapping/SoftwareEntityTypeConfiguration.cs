
namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.ProductAgg;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// Software entity type configuration
    /// </summary>
    class SoftwareEntityTypeConfiguration
        :EntityTypeConfiguration<Software>
    {
        /// <summary>
        /// Create a new instance of software entity type configuration
        /// </summary>
        public SoftwareEntityTypeConfiguration()
        {
            this.Property(s => s.LicenseCode)
                .IsRequired();

            this.ToTable("Softwares");
        }
    }
}
