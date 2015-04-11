using System.Data.Entity.ModelConfiguration;

using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CountryAgg;

namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork.Mapping
{

   /// <summary>
   ///    The country entity type configuration
   /// </summary>
   internal class CountryEntityConfiguration : EntityTypeConfiguration<Country>
   {

      /// <summary>
      ///    Create a new instance of Country entity type configuration
      /// </summary>
      public CountryEntityConfiguration()
      {
         //configure key and properties
         this.HasKey(c => c.Id);

         this.Property(c => c.CountryName).HasMaxLength(50).IsRequired();

         this.Property(c => c.CountryIsoCode).HasMaxLength(10).IsRequired();
      }

   }

}