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


namespace Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.BankingModule.Aggregates.BankAccountAgg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.Samples.NLayerApp.Domain.Seedwork;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.Resources;

    /// <summary>
    /// The bank transferLog representation
    /// </summary>
    public class BankAccountActivity
        :Entity
    {
        #region Properties

        /// <summary>
        /// Get or set the bank account identifier
        /// </summary>
        public Guid BankAccountId { get; set; }

        /// <summary>
        /// The bank transferLog date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The bank transferLog amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Get or set the activity description
        /// </summary>
        public string ActivityDescription { get; set; }

        #endregion
    }
}
