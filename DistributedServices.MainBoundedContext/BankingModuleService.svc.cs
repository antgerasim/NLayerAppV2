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

    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services;
    using Microsoft.Samples.NLayerApp.DistributedServices.MainBoundedContext.InstanceProviders;
    using Microsoft.Samples.NLayerApp.DistributedServices.Seedwork.ErrorHandlers;
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.DTO;
    

    [ApplicationErrorHandlerAttribute()] // manage all unhandled exceptions
    [UnityInstanceProviderServiceBehavior()] //create instance and inject dependencies using unity container
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class BankingModuleService : IBankingModuleService
    {
        #region Members

        readonly IBankAppService _bankAppService;

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of banking module service
        /// </summary>
        /// <param name="bankAppService">The bank application service dependency</param>
        public BankingModuleService(IBankAppService bankAppService)
        {
            if (bankAppService == null)
                throw new ArgumentNullException("bankAppService");

            _bankAppService = bankAppService;
        }
        #endregion

        #region IBaningModuleService Members

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService"/>
        /// </summary>
        /// <param name="newBankAccount"><see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService"/></returns>
        public BankAccountDTO AddNewBankAccount(BankAccountDTO newBankAccount)
        {
            return _bankAppService.AddBankAccount(newBankAccount);
        }

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService"/>
        /// </summary>
        /// <param name="bankAccountId"><see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService"/></returns>
        public bool LockBankAccount(Guid bankAccountId)
        {
            return _bankAppService.LockBankAccount(bankAccountId);
        }

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService"/>
        /// </summary>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService"/></returns>
        public List<BankAccountDTO> FindBankAccounts()
        {
            return _bankAppService.FindBankAccounts();
        }
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService"/>
        /// </summary>
        /// <param name="bankAccountId"><see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService"/></returns>
        public List<BankActivityDTO> FindBankAccountActivities(Guid bankAccountId)
        {
            return _bankAppService.FindBankAccountActivities(bankAccountId);
        }
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService"/>
        /// </summary>
        /// <param name="from"><see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService"/></param>
        /// <param name="to"><see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService"/></param>
        /// <param name="amount"><see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService"/></param>
        public void PerformTransfer(BankAccountDTO from, BankAccountDTO to, decimal amount)
        {
            _bankAppService.PerformBankTransfer(from, to, amount);
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// <see cref="M:System.IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            //dispose all resources
            _bankAppService.Dispose();
        }
        #endregion
    }
}
