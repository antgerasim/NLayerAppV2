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


namespace Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.BankingModule.Aggregates.BankAccountAgg
{
    using System;
    using Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification;

    /// <summary>
    /// A list of bank account specifications. You can learn
    /// about Specifications, Enhanced Query Objects or repository methods
    /// in DesignNotes.txt in Domain.Seedwork project
    /// </summary>
    public static class BankAccountSpecifications
    {
        /// <summary>
        /// Specification for bank accounts with number like to <paramref name="ibanNumber"/>
        /// </summary>
        /// <param name="ibanNumber">The bank account number</param>
        /// <returns>Associated specification</returns>
        public static ISpecification<BankAccount> BankAccountIbanNumber(string ibanNumber)
        {
            Specification<BankAccount> specification = new TrueSpecification<BankAccount>();

            if (!String.IsNullOrWhiteSpace(ibanNumber))
            {
                specification &= new DirectSpecification<BankAccount>((b) => b.Iban
                                                                              .ToLower()
                                                                              .Contains(ibanNumber.ToLower()));
            }

            return specification;
        }
    }
}
