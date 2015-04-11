using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.ProductAgg;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.Seedwork;

namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.ERPModule.Repositories
{

   /// <summary>
   ///    Product repository implementation
   /// </summary>
   public class ProductRepository : Repository<Product>,
      IProductRepository
   {
      #region Constructor
      /// <summary>
      ///    Create a new instance
      /// </summary>
      /// <param name="unitOfWork">Associated unit of work</param>
      public ProductRepository(MainBcUnitOfWork unitOfWork)
         : base(unitOfWork)
      {
      }
      #endregion
   }

}