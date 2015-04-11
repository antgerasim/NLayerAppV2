using System.Data.Entity.ModelConfiguration;

using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;

namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork.Mapping
{

   /// <summary>
   ///    The picture entity type configuration
   /// </summary>
   internal class PictureEntityConfiguration : EntityTypeConfiguration<Picture>
   {

      /// <summary>
      ///    Create a new instance of picture entity type configuration
      /// </summary>
      public PictureEntityConfiguration()
      {
         this.HasKey(p => p.Id);

         this.Property(p => p.RawPhoto).IsOptional();

         this.ToTable("Customers");
      }

   }

}