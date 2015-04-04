//===================================================================================
// Microsoft Developer and Platform Evangelism
//=================================================================================== 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// Copyright (c) Microsoft Corporation.  All Rights Reserved.
// This code is released under the terms of the MS-LPL license, 
// http://microsoftnlayerapp.codeplex.com/license
//===================================================================================

namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork
{
    using System.Collections.Generic;
    using System.Linq;

    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;

    
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.BankingModule.Aggregates.BankAccountAgg;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CountryAgg;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.OrderAgg;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.ProductAgg;
    using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork.Mapping;
    using Microsoft.Samples.NLayerApp.Infrastructure.Data.Seedwork;

    public class MainBCUnitOfWork
        :DbContext,IQueryableUnitOfWork
    {
        #region IDbSet Members

        IDbSet<Customer> _customers;
        public IDbSet<Customer> Customers
        {
            get 
            {
                if (_customers == null)
                    _customers = base.Set<Customer>();

                return _customers;
            }
        }

        IDbSet<Product> _products;
        public IDbSet<Product> Products
        {
            get 
            {
                if (_products == null)
                    _products = base.Set<Product>();

                return _products;
            }
        }

        IDbSet<Order> _orders;
        public IDbSet<Order> Orders
        {
            get 
            {
                if (_orders == null)
                    _orders = base.Set<Order>();

                return _orders;
            }
        }

        IDbSet<Country> _countries;
        public IDbSet<Country> Countries
        {
            get 
            {
                if (_countries == null)
                    _countries = base.Set<Country>();

                return _countries;
            }
        }

        IDbSet<BankAccount> _bankAccounts;
        public IDbSet<BankAccount> BankAccounts
        {
            get 
            {
                if (_bankAccounts == null)
                    _bankAccounts = base.Set<BankAccount>();

                return _bankAccounts;
            }
        }

        #endregion

        #region IQueryableUnitOfWork Members

        public DbSet<TEntity> CreateSet<TEntity>()
            where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void Attach<TEntity>(TEntity item) 
            where TEntity : class
        {
            //attach and set as unchanged
            base.Entry<TEntity>(item).State = System.Data.EntityState.Unchanged;
        }

        public void SetModified<TEntity>(TEntity item) 
            where TEntity : class
        {
            //this operation also attach item in object state manager
            base.Entry<TEntity>(item).State = System.Data.EntityState.Modified;
        }
        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current)
            where TEntity : class
        {
            //if it is not attached, attach original and set current values
            base.Entry<TEntity>(original).CurrentValues.SetValues(current);
        }

        public void Commit()
        {
            base.SaveChanges();
        }

        public void CommitAndRefreshChanges()
        {
            bool saveFailed = false;

            do
            {
                try
                {
                    base.SaveChanges();

                    saveFailed = false;

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                              .ForEach(entry =>
                               {
                                   entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                               });

                }
            } while (saveFailed);

        }

        public void RollbackChanges()
        {
            // set all entities in change tracker 
            // as 'unchanged state'
            base.ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = System.Data.EntityState.Unchanged);
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return base.Database.SqlQuery<TEntity>(sqlQuery, parameters);
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return base.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        #endregion

        #region DbContext Overrides

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Remove unused conventions
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //Add entity configurations in a structured way using 'TypeConfiguration’ classes
            modelBuilder.Configurations.Add(new BankAccountEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ProductEntityConfiguration());
            modelBuilder.Configurations.Add(new SoftwareEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new BookEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new CountryEntityConfiguration());
            modelBuilder.Configurations.Add(new CustomerEntityConfiguration());
            modelBuilder.Configurations.Add(new OrderEntityConfiguration());
            modelBuilder.Configurations.Add(new OrderLineEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new PictureEntityConfiguration());
        }
        #endregion
    }
}
