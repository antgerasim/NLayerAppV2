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

namespace Microsoft.Samples.NLayerApp.Domain.Seedwork
{

   /// <summary>
   ///    Base class for entities
   /// </summary>
   public abstract class Entity
   {
      #region Properties
      /// <summary>
      ///    Get the persisten object identifier
      /// </summary>
      public virtual Guid Id
      {
         get
         {
            return _id;
         }
         protected set
         {
            _id = value;
         }
      }
      #endregion

      #region Members
      private int? _requestedHashCode;
      private Guid _id;
      #endregion

      #region Public Methods
      /// <summary>
      ///    Check if this entity is transient, ie, without identity at this moment
      /// </summary>
      /// <returns>True if entity is transient, else false</returns>
      public bool IsTransient()
      {
         return this.Id == Guid.Empty;
      }

      /// <summary>
      ///    Generate identity for this entity
      /// </summary>
      public void GenerateNewIdentity()
      {
         if (IsTransient()) { this.Id = IdentityGenerator.NewSequentialGuid(); }
      }

      /// <summary>
      ///    Change current identity for a new non transient identity
      /// </summary>
      /// <param name="identity">the new identity</param>
      public void ChangeCurrentIdentity(Guid identity)
      {
         if (identity != Guid.Empty) { this.Id = identity; }
      }
      #endregion

      #region Overrides Methods
      /// <summary>
      ///    <see cref="M:System.Object.Equals" />
      /// </summary>
      /// <param name="obj">
      ///    <see cref="M:System.Object.Equals" />
      /// </param>
      /// <returns>
      ///    <see cref="M:System.Object.Equals" />
      /// </returns>
      public override bool Equals(object obj)
      {
         if (obj == null || !(obj is Entity)) { return false; }

         if (ReferenceEquals(this, obj)) { return true; }

         var item = (Entity) obj;

         if (item.IsTransient() || this.IsTransient()) { return false; }
         else
         { return item.Id == this.Id; }
      }

      /// <summary>
      ///    <see cref="M:System.Object.GetHashCode" />
      /// </summary>
      /// <returns>
      ///    <see cref="M:System.Object.GetHashCode" />
      /// </returns>
      public override int GetHashCode()
      {
         if (!IsTransient())
         {
            if (!_requestedHashCode.HasValue)
            {
               _requestedHashCode = this.Id.GetHashCode() ^ 31;
               // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)
            }

            return _requestedHashCode.Value;
         }
         else
         { return base.GetHashCode(); }

      }

      public static bool operator ==(Entity left, Entity right)
      {
         if (Equals(left, null)) { return (Equals(right, null)) ? true : false; }
         else
         { return left.Equals(right); }
      }

      public static bool operator !=(Entity left, Entity right)
      {
         return !(left == right);
      }
      #endregion
   }

}