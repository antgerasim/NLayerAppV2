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

namespace Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.BankingModule.Services
{
    using System;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.BankingModule.Aggregates.BankAccountAgg;


    /// <summary>
    /// Bank transfer domain service base contract
    /// </summary>
    public interface IBankTransferService
    {
        /// <summary>
        /// Perform a new transferLog into two bank accounts
        /// </summary>
        /// <param name="amount">The bank transferLog amount</param>
        /// <param name="originAccount">The originAccount bank account</param>
        /// <param name="destinationAccount">The destinationAccount bank account</param>
        void PerformTransfer(decimal amount, BankAccount originAccount, BankAccount destinationAccount);
    }
}
