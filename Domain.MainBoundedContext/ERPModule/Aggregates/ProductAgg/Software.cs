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


using System;
namespace Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.ProductAgg
{

    /// <summary>
    /// The software product
    /// </summary>
    public class Software
        :Product
    {
        #region Properties

        /// <summary>
        /// Get or set the license code
        /// </summary>
        public string LicenseCode { get; private set; }

        #endregion

        #region Constructor

        //required by ef
        private Software() { }

        public Software(string title, string description,string licenseCode)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException("title");

            if (String.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException("description");

            if (String.IsNullOrWhiteSpace(licenseCode))
                throw new ArgumentNullException("licenseCode");

            this.Title = title;
            this.Description = description;
            this.LicenseCode = licenseCode;
        }

        #endregion
    }
}
