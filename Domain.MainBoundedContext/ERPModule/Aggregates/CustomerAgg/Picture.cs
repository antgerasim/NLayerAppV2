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


namespace Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg
{

    using System;
    using Microsoft.Samples.NLayerApp.Domain.Seedwork;

    /// <summary>
    /// Picture of customer
    /// </summary>
    public class Picture
        :Entity
    {
        #region Properties

        /// <summary>
        /// Get the raw of photo
        /// </summary>
        public byte[] RawPhoto{ get; set; }

        #endregion
    }
}
