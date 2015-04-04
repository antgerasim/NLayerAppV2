//===================================================================================
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


namespace Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CountryAgg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.Samples.NLayerApp.Domain.Seedwork;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.Resources;

    /// <summary>
    /// The country entity
    /// </summary>
    public class Country
        :Entity
    {
        #region Properties

        /// <summary>
        /// Get or set the Country Name
        /// </summary>
        public string CountryName { get; private set; }

        /// <summary>
        /// Get or set the Country ISO Code
        /// </summary>
        public string CountryISOCode { get; private set; }

        #endregion

        #region Constructor

        //required by EF
        private Country() { } 

        public Country(string countryName, string countryISOCode)
        {
            if (String.IsNullOrWhiteSpace(countryName))
                throw new ArgumentNullException("countryName");

            if (String.IsNullOrWhiteSpace(countryISOCode))
                throw new ArgumentNullException("countryISOCode");

            this.CountryName = countryName;
            this.CountryISOCode = countryISOCode;
        }

        #endregion
    }
}
