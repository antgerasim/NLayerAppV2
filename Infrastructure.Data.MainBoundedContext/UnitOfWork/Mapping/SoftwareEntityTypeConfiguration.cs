using System.Data.Entity.ModelConfiguration;

using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.ProductAgg;

namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork.Mapping
{

   /// <summary>
   ///    Software entity type configuration
   /// </summary>
   internal class SoftwareEntityTypeConfiguration : EntityTypeConfiguration<Software>
   {

      /// <summary>
      ///    Create a new instance of software entity type configuration
      /// </summary>
      public SoftwareEntityTypeConfiguration()
      {
         this.Property(s => s.LicenseCode).IsRequired();

         this.ToTable("Softwares");
      }

   }

}