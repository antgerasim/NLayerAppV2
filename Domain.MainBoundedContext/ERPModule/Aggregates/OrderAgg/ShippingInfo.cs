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

namespace Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.OrderAgg
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Samples.NLayerApp.Domain.Seedwork;

    /// <summary>
    /// Shipping information 
    /// In this specifc Domain-Model, the ShippingInfo is a Value-Object.
    /// </summary>
    public class ShippingInfo
        :ValueObject<ShippingInfo>
    {
        #region Constructor

        /// <summary>
        /// Create a new instance of shipping info, providing its values. It will be immutable.
        /// </summary>
        /// <param name="shippingName">The shipping name</param>
        /// <param name="shippingAddress">The shipping address</param>
        /// <param name="shippingCity">The shipping city</param>
        /// <param name="shippingZipCode">The shipping zip code</param>
        public ShippingInfo(string shippingName, string shippingAddress, string shippingCity, string shippingZipCode)
        {
            this.ShippingName = shippingName;
            this.ShippingAddress = shippingAddress;
            this.ShippingCity = shippingCity;
            this.ShippingZipCode = shippingZipCode;

        }

        private ShippingInfo()  //required for EF
        {
        }

        #endregion

        #region Properties

        //Sets are 'private' because this class is a Value-Object
        //Therefore, it must be immutable from its creation.

        /// <summary>
        /// Get or set the shipping name
        /// </summary>
        public string ShippingName { get; private set; }

        /// <summary>
        /// Get or set the shipping address
        /// </summary>
        public string ShippingAddress { get; private set; }

        /// <summary>
        /// Get or set the shipping city
        /// </summary>
        public string ShippingCity { get; private set; }

        /// <summary>
        /// Get or set the shipping zip code
        /// </summary>
        public string ShippingZipCode { get; private set; }

        #endregion
    }
}
