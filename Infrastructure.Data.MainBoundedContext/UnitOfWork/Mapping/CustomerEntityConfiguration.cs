
namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// The customer entity type configuration
    /// </summary>
    class CustomerEntityConfiguration
        :EntityTypeConfiguration<Customer>
    {
        /// <summary>
        /// Create a new instance of customer entity configuration
        /// </summary>
        public CustomerEntityConfiguration()
        {
            //configure keys and properties
            this.HasKey(c => c.Id);

            this.Property(c => c.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            this.Property(c => c.LastName)
                .HasMaxLength(100)
                .IsRequired();

            this.Property(c => c.Company)
                .HasMaxLength(200);

            this.Property(c => c.Telephone)
                .HasMaxLength(25);

            this.Property(c => c.IsEnabled)
                .IsRequired();

            this.Property(c => c.CreditLimit)
                .IsRequired();

            
            //configure associations
            this.HasRequired(c => c.Picture) // this is a table-splitting
                .WithRequiredPrincipal();

            this.HasRequired(c => c.Country) // 1..*
                .WithMany()
                .HasForeignKey(c=>c.CountryId)
                .WillCascadeOnDelete(false);

           
            //configure table map
            this.ToTable("Customers");
        }
    }
}
