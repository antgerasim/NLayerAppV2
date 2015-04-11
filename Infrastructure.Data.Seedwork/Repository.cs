//===================================================================================
// Microsoft Developer & Platform Evangelism
//=================================================================================== 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// Copyright (c) Microsoft Corporation.  All Rights Reserved.
// This code is released under the terms of the MS-LPL license, 
// http://microsoftnlayerapp.codeplex.com/license
//===================================================================================

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.Samples.NLayerApp.Domain.Seedwork;
using Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification;
using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Logging;
using Microsoft.Samples.NLayerApp.Infrastructure.Data.Seedwork.Resources;

namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.Seedwork
{

   /// <summary>
   ///    Repository base class
   /// </summary>
   /// <typeparam name="TEntity">The type of underlying entity in this repository</typeparam>
   public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
   {
      #region Members
      private IQueryableUnitOfWork _unitOfWork;
      #endregion

      #region Constructor
      /// <summary>
      ///    Create a new instance of repository
      /// </summary>
      /// <param name="unitOfWork">Associated Unit Of Work</param>
      public Repository(IQueryableUnitOfWork unitOfWork)
      {
         if (unitOfWork == (IUnitOfWork) null) { throw new ArgumentNullException("unitOfWork"); }

         _unitOfWork = unitOfWork;
      }
      #endregion

      #region IDisposable Members
      /// <summary>
      ///    <see cref="M:System.IDisposable.Dispose" />
      /// </summary>
      public void Dispose()
      {
         if (_unitOfWork != null) { _unitOfWork.Dispose(); }
      }
      #endregion

      #region Private Methods
      private IDbSet<TEntity> GetSet()
      {
         return _unitOfWork.CreateSet<TEntity>();
      }
      #endregion

      #region IRepository Members
      /// <summary>
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </summary>
      public IUnitOfWork UnitOfWork
      {
         get
         {
            return _unitOfWork;
         }
      }

      /// <summary>
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </summary>
      /// <param name="item">
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </param>
      public virtual void Add(TEntity item)
      {

         if (item != (TEntity) null) {
            GetSet().Add(item); // add new item in this set
         }
         else
         {
            LoggerFactory.CreateLog().LogInfo(Messages.info_CannotAddNullEntity, typeof (TEntity).ToString());
         }

      }

      /// <summary>
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </summary>
      /// <param name="item">
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </param>
      public virtual void Remove(TEntity item)
      {
         if (item != (TEntity) null)
         {
            //attach item if not exist
            _unitOfWork.Attach(item);

            //set as "removed"
            GetSet().Remove(item);
         }
         else
         {
            LoggerFactory.CreateLog().LogInfo(Messages.info_CannotRemoveNullEntity, typeof (TEntity).ToString());
         }
      }

      /// <summary>
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </summary>
      /// <param name="item">
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </param>
      public virtual void TrackItem(TEntity item)
      {
         if (item != (TEntity) null) {
            _unitOfWork.Attach<TEntity>(item);
         }
         else
         {
            LoggerFactory.CreateLog().LogInfo(Messages.info_CannotRemoveNullEntity, typeof (TEntity).ToString());
         }
      }

      /// <summary>
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </summary>
      /// <param name="item">
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </param>
      public virtual void Modify(TEntity item)
      {
         if (item != (TEntity) null) {
            _unitOfWork.SetModified(item);
         }
         else
         {
            LoggerFactory.CreateLog().LogInfo(Messages.info_CannotRemoveNullEntity, typeof (TEntity).ToString());
         }
      }

      /// <summary>
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </summary>
      /// <param name="id">
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </param>
      /// <returns>
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </returns>
      public virtual TEntity Get(Guid id)
      {
         if (id != Guid.Empty) {
            return GetSet().Find(id);
         }
         else
         {
            return null;
         }
      }

      /// <summary>
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </summary>
      /// <returns>
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </returns>
      public virtual IEnumerable<TEntity> GetAll()
      {
         return GetSet();
      }

      /// <summary>
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </summary>
      /// <param name="specification">
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </param>
      /// <returns>
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </returns>
      public virtual IEnumerable<TEntity> AllMatching(ISpecification<TEntity> specification)
      {
         return GetSet().Where(specification.SatisfiedBy());
      }

      /// <summary>
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </summary>
      /// <typeparam name="S">
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </typeparam>
      /// <param name="pageIndex">
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </param>
      /// <param name="pageCount">
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </param>
      /// <param name="orderByExpression">
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </param>
      /// <param name="ascending">
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </param>
      /// <returns>
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </returns>
      public virtual IEnumerable<TEntity> GetPaged<TKProperty>(
         int pageIndex,
         int pageCount,
         Expression<Func<TEntity, TKProperty>> orderByExpression,
         bool ascending)
      {
         var set = GetSet();

         if (ascending) {
            return set.OrderBy(orderByExpression).Skip(pageCount * pageIndex).Take(pageCount);
         }
         else
         {
            return set.OrderByDescending(orderByExpression).Skip(pageCount * pageIndex).Take(pageCount);
         }
      }

      /// <summary>
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </summary>
      /// <param name="filter">
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </param>
      /// <returns>
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </returns>
      public virtual IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter)
      {
         return GetSet().Where(filter);
      }

      /// <summary>
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </summary>
      /// <param name="persisted">
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </param>
      /// <param name="current">
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{TValueObject}" />
      /// </param>
      public virtual void Merge(TEntity persisted, TEntity current)
      {
         _unitOfWork.ApplyCurrentValues(persisted, current);
      }
      #endregion
   }

}