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


namespace Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.ProductAgg
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Samples.NLayerApp.Domain.Seedwork;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.Resources;

    /// <summary>
    /// Product aggregate root-entity
    /// </summary>
    public abstract class Product
        :Entity,IValidatableObject
    {
        #region Properties

        /// <summary>
        /// Get or set the long description for this product
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// Get or set the product title
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// Get or set the unit price for this product
        /// </summary>
        public decimal UnitPrice { get; private set; }

        /// <summary>
        /// Get or set the stock items of this product
        /// </summary>
        public int AmountInStock { get; private set; }


        #endregion

        #region Public Methods

        /// <summary>
        /// Change the unit price
        /// </summary>
        /// <param name="unitPrice">The new unit price</param>
        public void ChangeUnitPrice(decimal unitPrice)
        {
            this.UnitPrice = unitPrice;
        }

        /// <summary>
        /// Increment the stock of this product
        /// </summary>
        /// <param name="units">The added items to stock</param>
        public void IncrementStock(int units = 0)
        {
            this.AmountInStock += units;
        }

        #endregion

        #region IValidatableObject Members

        /// <summary>
        /// <see cref="M:System.ComponentModel.DataAnnotations.IValidatableObject.Validate"/>
        /// </summary>
        /// <param name="validationContext"><see cref="M:System.ComponentModel.DataAnnotations.IValidatableObject.Validate"/></param>
        /// <returns><see cref="M:System.ComponentModel.DataAnnotations.IValidatableObject.Validate"/></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();
            
            if (AmountInStock < 0)
                validationResults.Add(new ValidationResult(Messages.validation_ProductAmountLessThanZero, new string[] { "AmountInStock" }));
           
            if (UnitPrice < 0)
                validationResults.Add(new ValidationResult(Messages.validation_ProductUnitPriceLessThanZero, new string[] { "UnitPrice" }));
            
            return validationResults;
        }

        #endregion  
    }
}
