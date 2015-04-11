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
using System.Linq.Expressions;

namespace Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification
{

   /// <summary>
   ///    A Logic OR Specification
   /// </summary>
   /// <typeparam name="T">Type of entity that check this specification</typeparam>
   public sealed class OrSpecification<T> : CompositeSpecification<T> where T : class
   {
      #region Public Constructor
      /// <summary>
      ///    Default constructor for AndSpecification
      /// </summary>
      /// <param name="leftSide">Left side specification</param>
      /// <param name="rightSide">Right side specification</param>
      public OrSpecification(ISpecification<T> leftSide, ISpecification<T> rightSide)
      {
         if (leftSide == (ISpecification<T>) null) { throw new ArgumentNullException("leftSide"); }

         if (rightSide == (ISpecification<T>) null) { throw new ArgumentNullException("rightSide"); }

         this._leftSideSpecification = leftSide;
         this._rightSideSpecification = rightSide;
      }
      #endregion

      #region Members
      private ISpecification<T> _rightSideSpecification = null;
      private ISpecification<T> _leftSideSpecification = null;
      #endregion

      #region Composite Specification overrides
      /// <summary>
      ///    Left side specification
      /// </summary>
      public override ISpecification<T> LeftSideSpecification
      {
         get
         {
            return _leftSideSpecification;
         }
      }

      /// <summary>
      ///    Righ side specification
      /// </summary>
      public override ISpecification<T> RightSideSpecification
      {
         get
         {
            return _rightSideSpecification;
         }
      }

      /// <summary>
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification.ISpecification{T}" />
      /// </summary>
      /// <returns>
      ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification.ISpecification{T}" />
      /// </returns>
      public override Expression<Func<T, bool>> SatisfiedBy()
      {
         var left = _leftSideSpecification.SatisfiedBy();
         var right = _rightSideSpecification.SatisfiedBy();

         return (left.Or(right));

      }
      #endregion
   }

}