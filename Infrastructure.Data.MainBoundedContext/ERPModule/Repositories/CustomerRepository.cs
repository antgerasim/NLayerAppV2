
namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.ERPModule.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity;

    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;
    using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork;
    using Microsoft.Samples.NLayerApp.Infrastructure.Data.Seedwork;


    /// <summary>
    /// The customer repository implementation
    /// </summary>
    public class CustomerRepository
        : Repository<Customer>, ICustomerRepository
    {

        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public CustomerRepository(MainBCUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion

        #region ICustomerRepository Members

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg.ICustomerRepository"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override Customer Get(Guid id)
        {
            if (id != Guid.Empty)
            {
                var currentUnitOfWork = this.UnitOfWork as MainBCUnitOfWork;

                var set = currentUnitOfWork.CreateSet<Customer>();

                return set.Include(c => c.Picture)
                          .Where(c => c.Id == id)
                          .SingleOrDefault();
            }
            else
                return null;
        }

        public override void Merge(Customer persisted, Customer current)
        {
            //merge customer and customer picture
            var currentUnitOfWork = this.UnitOfWork as MainBCUnitOfWork;

            currentUnitOfWork.ApplyCurrentValues(persisted, current);
            currentUnitOfWork.ApplyCurrentValues(persisted.Picture, current.Picture);
        }

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg.ICustomerRepository"/>
        /// </summary>
        /// <param name="pageIndex"><see cref="Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg.ICustomerRepository"/></param>
        /// <param name="pageCount"><see cref="Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg.ICustomerRepository"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg.ICustomerRepository"/></returns>
        public IEnumerable<Customer> GetEnabled(int pageIndex, int pageCount)
        {
            var currentUnitOfWork = this.UnitOfWork as MainBCUnitOfWork;

            return currentUnitOfWork.Customers
                                     .Where(c=>c.IsEnabled == true)
                                     .OrderBy(c => c.FullName)
                                     .Skip(pageIndex * pageCount)
                                     .Take(pageCount);
        }

        #endregion
    }
}
