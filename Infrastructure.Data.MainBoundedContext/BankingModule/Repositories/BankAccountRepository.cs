using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.BankingModule.Aggregates.BankAccountAgg;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.Seedwork;

namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.BankingModule.Repositories
{

   /// <summary>
   ///    The bank account repository implementation
   /// </summary>
   public class BankAccountRepository : Repository<BankAccount>,
      IBankAccountRepository
   {
      #region Constructor
      /// <summary>
      ///    Create a new instance
      /// </summary>
      /// <param name="unitOfWork">Associated unit of work</param>
      public BankAccountRepository(MainBcUnitOfWork unitOfWork)
         : base(unitOfWork)
      {

      }
      #endregion

      #region Overrides
      /// <summary>
      ///    Get all bank accounts and the customer information
      /// </summary>
      /// <returns>Enumerable collection of bank accounts</returns>
      public override IEnumerable<BankAccount> GetAll()
      {
         var currentUnitOfWork = this.UnitOfWork as MainBcUnitOfWork;

         var set = currentUnitOfWork.CreateSet<BankAccount>();

         return set.Include(ba => ba.Customer).AsEnumerable();

      }
      #endregion
   }

}