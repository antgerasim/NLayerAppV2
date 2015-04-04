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
    using Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;
    using System.Text.RegularExpressions;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.Resources;

    /// <summary>
    /// The Orders specifications
    /// </summary>
    public static class OrdersSpecifications
    {
        /// <summary>
        /// Orders by Customer specification
        /// </summary>
        /// <param name="customer">The customer</param>
        /// <returns>Related specification for this criterion</returns>
        public static ISpecification<Order> OrdersByCustomer(Customer customer)
        {
            if (customer == null
                ||
                customer.IsTransient())
            {
                throw new ArgumentNullException("customer");
            }

            return new DirectSpecification<Order>(o => o.CustomerId == customer.Id);
        }
        /// <summary>
        /// Order by order number
        /// </summary>
        /// <param name="orderNumber">Find orders using the order number</param>
        /// <returns>Related specification for this criterion</returns>
        public static ISpecification<Order> OrdersByNumber(string orderNumber)
        {
            string orderNumberPattern = @"\d{4}/\d{1,2}-\d+";

            if (Regex.IsMatch(orderNumber, orderNumberPattern))
            {
                int sequenceNumber = Int32.Parse(Regex.Split(orderNumber, "-")[1]);
                return new DirectSpecification<Order>(o => o.SequenceNumberOrder == sequenceNumber);
            }
            else
                throw new InvalidOperationException(Messages.exception_OrderNumberSpecificationInvalidOrderNumberPattern);
            
        }

        /// <summary>
        /// The orders in a date range
        /// </summary>
        /// <param name="startDate">The start date </param>
        /// <param name="endDate">The end date</param>
        /// <returns>Related specification for this criteria</returns>
        public static ISpecification<Order> OrderFromDateRange(DateTime? startDate, DateTime? endDate)
        {
            Specification<Order> spec = new TrueSpecification<Order>();

            if (startDate.HasValue)
                spec &= new DirectSpecification<Order>(o => o.OrderDate > (startDate ?? DateTime.MinValue));

            if (endDate.HasValue)
                spec &= new DirectSpecification<Order>(o => o.OrderDate < (endDate ?? DateTime.MaxValue));

            return spec;
        }

    }
}
