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

using Microsoft.Samples.NLayerApp.Domain.Seedwork;

namespace Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.BankingModule.Aggregates.BankAccountAgg
{

   /// <summary>
   ///    Base contract for bank account repository
   ///    <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.IRepository{BankAccount}" />
   /// </summary>
   public interface IBankAccountRepository : IRepository<BankAccount>
   {

   }

}