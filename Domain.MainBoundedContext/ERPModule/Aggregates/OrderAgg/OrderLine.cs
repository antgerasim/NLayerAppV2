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


namespace Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.OrderAgg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.Samples.NLayerApp.Domain.Seedwork;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.Resources;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.ProductAgg;

    /// <summary>
    /// The order line representation
    /// </summary>
    public class OrderLine
        : Entity, IValidatableObject
    {

        #region Properties

        /// <summary>
        /// Get or set the current unit price of the product in this line
        /// <remarks>
        /// The unit price cannot be less than zero
        /// </remarks>
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Get or set the amount of units in this line
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Get or set the discount associated
        /// <remarks>
        /// Discount is a value between [0-1]
        /// </remarks>
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Get the total amount of money for this line
        /// </summary>
        public decimal TotalLine
        {
            get
            {
                return (UnitPrice * Amount) * (1 - (Discount/100M));
            }
        }

        /// <summary>
        /// Related aggregate identifier
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// Get or set the product identifier
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Get or set associated product 
        /// </summary>
        public Product Product { get; private set; }

        #endregion

        #region Public Methods
        
        /// <summary>
        /// Sets a product in this order line
        /// </summary>
        /// <param name="product">The related product for this order line</param>
        public void SetProduct(Product product)
        {
            if (product == null
                ||
                product.IsTransient())
            {
                throw new ArgumentNullException(Messages.exception_CannotAssociateTransientOrNullProduct);
            } 

            //fix identifiers
            this.ProductId = product.Id;
            this.Product = product;
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

            if (Discount < 0 || Discount > 1)
                validationResults.Add(new ValidationResult(Messages.validation_OrderLineDiscountCannotBeLessThanZeroOrGreaterThanOne,
                                                            new string[] { "Discount" }));
            if (OrderId == Guid.Empty)
                validationResults.Add(new ValidationResult(Messages.validation_OrderLineOrderIdIsEmpty,
                                                           new string[] { "OrderId" }));

            if (Amount <= 0)
                validationResults.Add(new ValidationResult(Messages.validation_OrderLineAmountLessThanOne,
                                                           new string[] { "Amount" }));

            if (UnitPrice < 0)
                validationResults.Add(new ValidationResult(Messages.validation_OrderLineUnitPriceLessThanZero,
                                                           new string[] { "UnitPrice" }));

            if ( ProductId == Guid.Empty)
                validationResults.Add(new ValidationResult(Messages.validation_OrderLineProductIdCannotBeNull,
                                                         new string[]{"ProductId"}));

            return validationResults;
        }

        #endregion
    }
}
