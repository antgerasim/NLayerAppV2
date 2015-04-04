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


namespace Microsoft.Samples.NLayerApp.DistributedServices.MainBoundedContext
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.DTO;
    using Microsoft.Samples.NLayerApp.DistributedServices.Seedwork.ErrorHandlers;
    
    /// <summary>
    /// WCF SERVICE FACADE for Banking Module
    /// </summary>
    [ServiceContract]
    public interface IBankingModuleService : IDisposable
    {
        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        BankAccountDTO AddNewBankAccount(BankAccountDTO bankAccount);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        bool LockBankAccount(Guid bankAccountId);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        List<BankAccountDTO> FindBankAccounts();

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        List<BankActivityDTO> FindBankAccountActivities(Guid bankAccountId);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        void PerformTransfer(BankAccountDTO from, BankAccountDTO to, decimal amount);
    }
}
