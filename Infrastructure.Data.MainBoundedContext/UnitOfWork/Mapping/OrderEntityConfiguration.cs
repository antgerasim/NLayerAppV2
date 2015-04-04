
namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.OrderAgg;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The order entity configuration
    /// </summary>
    class OrderEntityConfiguration
        :EntityTypeConfiguration<Order>
    {
        public OrderEntityConfiguration()
        {

            this.HasKey(o => o.Id);

            this.Property(o => o.SequenceNumberOrder)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Ignore(o => o.OrderNumber);

            //order->orderline navigation
            this.HasMany(o => o.OrderLines)
                .WithRequired()
                .HasForeignKey(ol => ol.OrderId)
                .WillCascadeOnDelete(true);
        }
    }
}
