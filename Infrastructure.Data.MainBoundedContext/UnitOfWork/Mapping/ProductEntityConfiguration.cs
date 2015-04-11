using System.Data.Entity.ModelConfiguration;

using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.ProductAgg;

namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork.Mapping
{

   /// <summary>
   ///    The product entity type configuration
   /// </summary>
   internal class ProductEntityConfiguration : EntityTypeConfiguration<Product>
   {

      /// <summary>
      ///    Create a new instance of product entity configuration
      /// </summary>
      public ProductEntityConfiguration()
      {
         //configure key and properties

         this.HasKey(p => p.Id);

         this.Property(p => p.Title).IsRequired();

         this.Property(p => p.UnitPrice).HasPrecision(10, 2);

         this.ToTable("Products");
      }

   }

}