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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.DTO;
using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.Resources;
using Microsoft.Samples.NLayerApp.Application.Seedwork;
using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.BankingModule.Aggregates.BankAccountAgg;
using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.BankingModule.Services;
using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;
using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Logging;
using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Validator;

namespace Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services
{

   /// <summary>
   ///    The bank management service implementation
   /// </summary>
   public class BankAppService : IBankAppService
   {
      #region Constructor
      /// <summary>
      ///    Create a new instance
      /// </summary>
      public BankAppService(
         IBankAccountRepository bankAccountRepository,
         // the bank account repository dependency
         ICustomerRepository customerRepository,
         // the customer repository dependency
         IBankTransferService transferService)
      {
         //check preconditions
         if (bankAccountRepository == null) { throw new ArgumentNullException("bankAccountRepository"); }

         if (customerRepository == null) { throw new ArgumentNullException("customerRepository"); }

         if (transferService == null) { throw new ArgumentNullException("trasferService"); }

         _bankAccountRepository = bankAccountRepository;
         _customerRepository = customerRepository;
         _transferService = transferService;
      }
      #endregion

      #region IDisposable Members
      /// <summary>
      ///    <see cref="M:System.IDisposable.Dispose" />
      /// </summary>
      public void Dispose()
      {
         //dispose all resources
         _bankAccountRepository.Dispose();
         _customerRepository.Dispose();
      }
      #endregion

      #region Members
      private readonly IBankAccountRepository _bankAccountRepository;
      private readonly ICustomerRepository _customerRepository;
      private readonly IBankTransferService _transferService;
      #endregion

      #region IBankAppService Members
      /// <summary>
      ///    <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IBankAppService" />
      /// </summary>
      /// <param name="bankAccountDto">
      ///    <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IBankAppService" />
      /// </param>
      /// <returns>
      ///    <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IBankAppService" />
      /// </returns>
      public BankAccountDto AddBankAccount(BankAccountDto bankAccountDto)
      {
         if (bankAccountDto == null || bankAccountDto.CustomerId == Guid.Empty) {
            throw new ArgumentException(Messages.warning_CannotAddNullBankAccountOrInvalidCustomer);
         }

         //check if exists the customer for this bank account
         var associatedCustomer = _customerRepository.Get(bankAccountDto.CustomerId);

         if (associatedCustomer != null) // if the customer exist
         {
            //Create a new bank account  number
            var accountNumber = CalculateNewBankAccountNumber();

            //Create account from factory 
            var account = BankAccountFactory.CreateBankAccount(associatedCustomer, accountNumber);

            //save bank account
            SaveBankAccount(account);

            return account.ProjectedAs<BankAccountDto>();
         }
         else //the customer for this bank account not exist, cannot create a new bank account
         {
            throw new InvalidOperationException(Messages.warning_CannotCreateBankAccountForNonExistingCustomer);
         }

      }

      /// <summary>
      ///    <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IBankAppService" />
      /// </summary>
      /// <param name="bankAccountId">
      ///    <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IBankAppService" />
      /// </param>
      /// <returns>
      ///    <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IBankAppService" />
      /// </returns>
      public bool LockBankAccount(Guid bankAccountId)
      {
         //recover bank account, lock and commit changes
         var bankAccount = _bankAccountRepository.Get(bankAccountId);

         if (bankAccount != null)
         {
            bankAccount.Lock();

            _bankAccountRepository.UnitOfWork.Commit();

            return true;
         }
         else // if not exist the bank account return false
         {
            LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotLockNonExistingBankAccount, bankAccountId);

            return false;
         }
      }

      /// <summary>
      ///    <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IBankAppService" />
      /// </summary>
      /// <returns>
      ///    <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IBankAppService" />
      /// </returns>
      public List<BankAccountDto> FindBankAccounts()
      {
         var bankAccounts = _bankAccountRepository.GetAll();

         if (bankAccounts != null && bankAccounts.Any()) {
            return bankAccounts.ProjectedAsCollection<BankAccountDto>();
         }
         else // no results
         {
            return null;
         }
      }

      /// <summary>
      ///    <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IBankAppService" />
      /// </summary>
      /// <param name="fromAccount">
      ///    <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IBankAppService" />
      /// </param>
      /// <param name="toAccount">
      ///    <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IBankAppService" />
      /// </param>
      /// <param name="amount">
      ///    <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IBankAppService" />
      /// </param>
      public void PerformBankTransfer(BankAccountDto fromAccount, BankAccountDto toAccount, decimal amount)
      {
         //Application-Logic Process: 
         // 1º Get Accounts objects from Repositories
         // 2º Start Transaction
         // 3º Call PerformTransfer method in Domain Service
         // 4º If no exceptions, commit the unit of work and complete transaction

         if (BankAccountHasIdentity(fromAccount) && BankAccountHasIdentity(toAccount))
         {
            var source = _bankAccountRepository.Get(fromAccount.Id);
            var target = _bankAccountRepository.Get(toAccount.Id);

            if (source != null & target != null) // if all accounts exist
            {
               using (var scope = new TransactionScope())
               {
                  //perform transfer
                  _transferService.PerformTransfer(amount, source, target);

                  //comit unit of work
                  _bankAccountRepository.UnitOfWork.Commit();

                  //complete transaction
                  scope.Complete();
               }
            }
            else
            {
               LoggerFactory.CreateLog().LogError(Messages.error_CannotPerformTransferInvalidAccounts);
            }
         }
         else
         {
            LoggerFactory.CreateLog().LogError(Messages.error_CannotPerformTransferInvalidAccounts);
         }

      }

      /// <summary>
      ///    <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IBankAppService" />
      /// </summary>
      /// <param name="bankAccountId">
      ///    <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.IBankManagementService" />
      /// </param>
      /// <returns>
      ///    <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.IBankManagementService" />
      /// </returns>
      public List<BankActivityDto> FindBankAccountActivities(Guid bankAccountId)
      {
         var account = _bankAccountRepository.Get(bankAccountId);

         if (account != null) {
            return account.BankAccountActivity.ProjectedAsCollection<BankActivityDto>();
         }
         else // the bank account not exist
         {
            LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotGetActivitiesForInvalidOrNotExistingBankAccount);
            return null;
         }
      }
      #endregion

      #region Private Methods
      private void SaveBankAccount(BankAccount bankAccount)
      {
         //validate bank account
         var validator = EntityValidatorFactory.CreateValidator();

         if (validator.IsValid<BankAccount>(bankAccount)) // save entity
         {
            _bankAccountRepository.Add(bankAccount);
            _bankAccountRepository.UnitOfWork.Commit();
         }
         else //throw validation errors
         {
            throw new ApplicationValidationErrorsException(validator.GetInvalidMessages(bankAccount));
         }
      }

      private BankAccountNumber CalculateNewBankAccountNumber()
      {
         var bankAccountNumber = new BankAccountNumber();

         //simulate bank account number creation....

         bankAccountNumber.OfficeNumber = "2354";
         bankAccountNumber.NationalBankCode = "2134";
         bankAccountNumber.CheckDigits = "02";
         bankAccountNumber.AccountNumber = new Random().Next(1, Int32.MaxValue).ToString();

         return bankAccountNumber;

      }

      private bool BankAccountHasIdentity(BankAccountDto bankAccountDto)
      {
         //return true is bank account dto has identity
         return (bankAccountDto != null && bankAccountDto.Id != Guid.Empty);
      }
      #endregion
   }

}