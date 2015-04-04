

namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.ProductAgg;

    /// <summary>
    /// Book entity type configuration
    /// </summary>
    class BookEntityTypeConfiguration
        :EntityTypeConfiguration<Book>
    {
        public BookEntityTypeConfiguration()
        {
            this.Property(b => b.ISBN)
                .IsRequired();
            this.Property(b => b.Publisher)
                .IsRequired();

            this.ToTable("Books");
        }
    }
}
