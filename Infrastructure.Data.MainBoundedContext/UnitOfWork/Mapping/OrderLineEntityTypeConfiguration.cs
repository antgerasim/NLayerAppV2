using System.Data.Entity.ModelConfiguration;

using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.OrderAgg;

namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork.Mapping
{

   /// <summary>
   ///    The orderline entity type configuration
   /// </summary>
   internal class OrderLineEntityTypeConfiguration : EntityTypeConfiguration<OrderLine>
   {

      public OrderLineEntityTypeConfiguration()
      {
         this.HasKey(ol => ol.Id);

         this.Property(ol => ol.UnitPrice).HasPrecision(10, 2);

         this.Property(ol => ol.Discount).HasPrecision(10, 2);

         this.Ignore(ol => ol.TotalLine);

         //Associations
         this.HasRequired(p => p.Product).WithMany().HasForeignKey(ol => ol.ProductId);
      }

   }

}