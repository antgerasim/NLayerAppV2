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
    /// This is the data transfer object
    /// for customer entity in a list. The name
    /// of properties for this type
    /// is based on conventions of many mappers
    /// to simplificate the mapping process
    /// </summary>
    public class CustomerListDTO
    {
        /// <summary>
        /// The customer identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The customer first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The customer lastName
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Get or set the telephone 
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// Get or set the company name
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// The asociated credit limit
        /// </summary>
        public decimal CreditLimit { get; set; }

        /// <summary>
        /// The address city
        /// </summary>
        public string AddressCity { get; set; }

        /// <summary>
        /// The address zip code
        /// </summary>
        public string AddressZipCode { get; set; }

        /// <summary>
        /// The address line 1
        /// </summary>
        public string AddressAddressLine1 { get; set; }

        /// <summary>
        /// The address line 2
        /// </summary>
        public string AddressAddressLine2 { get; set; }

    }
}
