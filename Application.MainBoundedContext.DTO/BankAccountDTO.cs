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

namespace Microsoft.Samples.NLayerApp.Application.MainBoundedContext.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// This is the data transfer object for
    /// Bank Account entitiy.The name
    /// of properties for this type
    /// is based on conventions of many mappers
    /// to simplificate the mapping process.
    /// </summary>
    public class BankAccountDTO
    {
        /// <summary>
        /// The bank account identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Bank account number
        /// </summary>
        public string BankAccountNumber { get; set; }

        /// <summary>
        /// The bank balance
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// The locked state of this bank account
        /// </summary>
        public bool Locked { get; set; }

        /// <summary>
        /// Get or set the customer id
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// The owner first name
        /// </summary>
        public string CustomerFirstName { get; set; }

        /// <summary>
        /// the owner last name
        /// </summary>
        public string CustomerLastName { get; set; }

      
    }
}
