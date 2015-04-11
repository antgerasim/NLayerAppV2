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
using System.Linq;
using System.Linq.Expressions;

namespace Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification
{

   /// <summary>
   ///    NotEspecification convert a original
   ///    specification with NOT logic operator
   /// </summary>
   /// <typeparam name="TEntity">Type of element for this specificaiton</typeparam>
   public sealed class NotSpecification<TEntity> : Specification<TEntity> where TEntity : class
   {
      #region Members
      private Expression<Func<TEntity, bool>> _originalCriteria;
      #endregion

      #region Override Specification methods
      /// <summary>
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification.ISpecification{TEntity}" />
      /// </summary>
      /// <returns>
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification.ISpecification{TEntity}" />
      /// </returns>
      public override Expression<Func<TEntity, bool>> SatisfiedBy()
      {

         return Expression.Lambda<Func<TEntity, bool>>(
            Expression.Not(_originalCriteria.Body),
            _originalCriteria.Parameters.Single());
      }
      #endregion

      #region Constructor
      /// <summary>
      ///    Constructor for NotSpecificaiton
      /// </summary>
      /// <param name="originalSpecification">Original specification</param>
      public NotSpecification(ISpecification<TEntity> originalSpecification)
      {

         if (originalSpecification == (ISpecification<TEntity>) null) {
            throw new ArgumentNullException("originalSpecification");
         }

         _originalCriteria = originalSpecification.SatisfiedBy();
      }

      /// <summary>
      ///    Constructor for NotSpecification
      /// </summary>
      /// <param name="originalSpecification">Original specificaiton</param>
      public NotSpecification(Expression<Func<TEntity, bool>> originalSpecification)
      {
         if (originalSpecification == (Expression<Func<TEntity, bool>>) null) {
            throw new ArgumentNullException("originalSpecification");
         }

         _originalCriteria = originalSpecification;
      }
      #endregion
   }

}