

namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.ERPModule.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Data.Entity;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.OrderAgg;
    using Microsoft.Samples.NLayerApp.Infrastructure.Data.Seedwork;
    using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork;
    using Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification;

    /// <summary>
    /// The order repository implementation
    /// </summary>
    public class OrderRepository
        :Repository<Order>,IOrderRepository
    {
 
        #region Constructor

        /// <summary>
        /// Create a new instance of this repository
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public OrderRepository(MainBCUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion

        #region Overrides

        public override IEnumerable<Order> GetPaged<KProperty>(int pageIndex, int pageCount, Expression<Func<Order, KProperty>> orderByExpression, bool ascending)
        {
            var currentUnitOfWork = this.UnitOfWork as MainBCUnitOfWork;

            var set = currentUnitOfWork.CreateSet<Order>();

            if (ascending)
            {
                return set.Include(o=>o.OrderLines)
                          .OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount)
                          .AsEnumerable();
            }
            else
            {
                return set.Include(o => o.OrderLines)
                          .OrderByDescending(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount)
                          .AsEnumerable();
            }
        }

        public override IEnumerable<Order> AllMatching(ISpecification<Order> specification)
        {
            var currentUnitOfWork = this.UnitOfWork as MainBCUnitOfWork;

            var set = currentUnitOfWork.CreateSet<Order>();

            return set.Include(o => o.OrderLines)
                      .Where(specification.SatisfiedBy())
                      .AsEnumerable();
        }
        #endregion
    }
}
