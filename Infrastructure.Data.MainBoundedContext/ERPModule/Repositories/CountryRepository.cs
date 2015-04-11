using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CountryAgg;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.Seedwork;

namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.ERPModule.Repositories
{

   /// <summary>
   ///    The country repository implementation
   /// </summary>
   public class CountryRepository : Repository<Country>,
      ICountryRepository
   {
      #region Constructor
      /// <summary>
      ///    Create a new instance
      /// </summary>
      /// <param name="unitOfWork">Associated unit of work</param>
      public CountryRepository(MainBcUnitOfWork unitOfWork)
         : base(unitOfWork)
      {
      }
      #endregion
   }

}