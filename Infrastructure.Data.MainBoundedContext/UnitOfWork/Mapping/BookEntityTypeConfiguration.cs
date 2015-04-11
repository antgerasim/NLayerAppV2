using System.Data.Entity.ModelConfiguration;

using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.ProductAgg;

namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork.Mapping
{

   /// <summary>
   ///    Book entity type configuration
   /// </summary>
   internal class BookEntityTypeConfiguration : EntityTypeConfiguration<Book>
   {

      public BookEntityTypeConfiguration()
      {
         this.Property(b => b.Isbn).IsRequired();
         this.Property(b => b.Publisher).IsRequired();

         this.ToTable("Books");
      }

   }

}