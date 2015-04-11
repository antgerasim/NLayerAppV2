using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.OrderAgg;

namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork.Mapping
{

   /// <summary>
   ///    The order entity configuration
   /// </summary>
   internal class OrderEntityConfiguration : EntityTypeConfiguration<Order>
   {

      public OrderEntityConfiguration()
      {

         this.HasKey(o => o.Id);

         this.Property(o => o.SequenceNumberOrder).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

         this.Ignore(o => o.OrderNumber);

         //order->orderline navigation
         this.HasMany(o => o.OrderLines).WithRequired().HasForeignKey(ol => ol.OrderId).WillCascadeOnDelete(true);
      }

   }

}