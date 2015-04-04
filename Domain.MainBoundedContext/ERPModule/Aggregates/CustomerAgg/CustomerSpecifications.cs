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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification;

    /// <summary>
    /// A list of customer specifications. You can learn
    /// about Specifications, Enhanced Query Objects or repository methods
    /// reading our Architecture guide and checking the DesignNotes.txt in Domain.Seedwork project
    /// </summary>
    public static class CustomerSpecifications
    {
        /// <summary>
        /// Enabled customers specification
        /// </summary>
        /// <returns>Asociated specification for this criteria</returns>
        public static Specification<Customer> EnabledCustomers()
        {
            return  new DirectSpecification<Customer>(c => c.IsEnabled);
        }

        /// <summary>
        /// Customer with firstName or LastName equal to <paramref name="text"/>
        /// </summary>
        /// <param name="text">The firstName or lastName to find</param>
        /// <returns>Associated specification for this creterion</returns>
        public static Specification<Customer> CustomerFullText(string text)
        {
            Specification<Customer> specification = new DirectSpecification<Customer>(c => c.IsEnabled);

            if (!String.IsNullOrWhiteSpace(text))
            {
                var firstNameSpec = new DirectSpecification<Customer>(c => c.FirstName.ToLower().Contains(text));
                var lastNameSpec = new DirectSpecification<Customer>(c => c.LastName.ToLower().Contains(text));

                specification &= (firstNameSpec || lastNameSpec);
            }

            return specification;
        }
    }
}
