//==================================================================================
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

using System;

using Microsoft.Samples.NLayerApp.Domain.Seedwork;

namespace Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.BankingModule.Aggregates.BankAccountAgg
{

   /// <summary>
   ///    The bank transferLog representation
   /// </summary>
   public class BankAccountActivity : Entity
   {
      #region Properties
      /// <summary>
      ///    Get or set the bank account identifier
      /// </summary>
      public Guid BankAccountId { get; set; }

      /// <summary>
      ///    The bank transferLog date
      /// </summary>
      public DateTime Date { get; set; }

      /// <summary>
      ///    The bank transferLog amount
      /// </summary>
      public decimal Amount { get; set; }

      /// <summary>
      ///    Get or set the activity description
      /// </summary>
      public string ActivityDescription { get; set; }
      #endregion
   }

}