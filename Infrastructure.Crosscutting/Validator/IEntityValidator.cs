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
			

namespace Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Validator
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The entity validator base contract
    /// </summary>
    public interface IEntityValidator
    {
        /// <summary>
        /// Perform validation and return if the entity state is valid
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to validate</typeparam>
        /// <param name="item">The instance to validate</param>
        /// <returns>True if entity state is valid</returns>
        bool IsValid<TEntity>(TEntity item)
            where TEntity : class;

        /// <summary>
        /// Return the collection of errors if entity state is not valid
        /// </summary>
        /// <typeparam name="TEntity">The type of entity</typeparam>
        /// <param name="item">The instance with validation errors</param>
        /// <returns>A collection of validation errors</returns>
        IEnumerable<String> GetInvalidMessages<TEntity>(TEntity item)
            where TEntity : class;
    }
}
