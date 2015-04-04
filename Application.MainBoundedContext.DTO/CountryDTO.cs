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
    /// for country entity. The name
    /// of properties for this type
    /// is based on conventions of many mappers
    /// to simplificate the mapping process
    /// </summary>
    public class CountryDTO
    {
        /// <summary>
        /// The country identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The country name
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        /// The country ISO Code
        /// </summary>
        public string CountryISOCode { get; set; }
    }
}
