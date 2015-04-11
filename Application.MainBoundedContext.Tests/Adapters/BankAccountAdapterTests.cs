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

using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.DTO;
using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.BankingModule.Aggregates.BankAccountAgg;
using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CountryAgg;
using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;
using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Adapter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.MainBoundedContext.Tests.Adapters
{

   [TestClass()]
   public class BankAccountAdapterTests
   {

      [TestMethod()]
      public void AdaptBankActivityToBankActivityDto()
      {
         //Arrange
         var activity = new BankAccountActivity();

         activity.GenerateNewIdentity();
         activity.Date = DateTime.Now;
         activity.Amount = 1000;
         activity.ActivityDescription = "transfer...";

         //Act
         var adapter = TypeAdapterFactory.CreateAdapter();
         var activityDto = adapter.Adapt<BankAccountActivity, BankActivityDto>(activity);

         //Assert
         Assert.AreEqual(activity.Date, activityDto.Date);
         Assert.AreEqual(activity.Amount, activityDto.Amount);
         Assert.AreEqual(activity.ActivityDescription, activityDto.ActivityDescription);
      }

      [TestMethod()]
      public void AdaptEnumerableBankActivityToListBankActivityDto()
      {
         //Arrange
         var activity = new BankAccountActivity();

         activity.GenerateNewIdentity();
         activity.Date = DateTime.Now;
         activity.Amount = 1000;
         activity.ActivityDescription = "transfer...";

         IEnumerable<BankAccountActivity> activities = new List<BankAccountActivity>()
         {
            activity
         };

         //Act
         var adapter = TypeAdapterFactory.CreateAdapter();
         var activitiesDto = adapter.Adapt<IEnumerable<BankAccountActivity>, List<BankActivityDto>>(activities);

         //Assert
         Assert.IsNotNull(activitiesDto);
         Assert.IsTrue(activitiesDto.Count() == 1);

         Assert.AreEqual(activity.Date, activitiesDto[0].Date);
         Assert.AreEqual(activity.Amount, activitiesDto[0].Amount);
         Assert.AreEqual(activity.ActivityDescription, activitiesDto[0].ActivityDescription);
      }

      [TestMethod()]
      public void AdaptBankAccountToBankAccountDto()
      {
         //Arrange
         var country = new Country("Spain", "es-ES");
         country.GenerateNewIdentity();

         var customer = CustomerFactory.CreateCustomer(
            "jhon",
            "el rojo",
            "+3441",
            "company",
            country,
            new Address("", "", "", ""));
         customer.GenerateNewIdentity();

         var account = new BankAccount();
         account.GenerateNewIdentity();
         account.BankAccountNumber = new BankAccountNumber("4444", "5555", "3333333333", "02");
         account.SetCustomerOwnerOfThisBankAccount(customer);
         account.DepositMoney(1000, "reason");
         account.Lock();

         //Act
         var adapter = TypeAdapterFactory.CreateAdapter();
         var bankAccountDto = adapter.Adapt<BankAccount, BankAccountDto>(account);

         //Assert
         Assert.AreEqual(account.Id, bankAccountDto.Id);
         Assert.AreEqual(account.Iban, bankAccountDto.BankAccountNumber);
         Assert.AreEqual(account.Balance, bankAccountDto.Balance);
         Assert.AreEqual(account.Customer.FirstName, bankAccountDto.CustomerFirstName);
         Assert.AreEqual(account.Customer.LastName, bankAccountDto.CustomerLastName);
         Assert.AreEqual(account.Locked, bankAccountDto.Locked);
      }

      [TestMethod()]
      public void AdaptEnumerableBankAccountToListBankAccountListDto()
      {
         //Arrange

         var country = new Country("spain", "es-ES");
         country.GenerateNewIdentity();

         var customer = CustomerFactory.CreateCustomer(
            "jhon",
            "el rojo",
            "+341232",
            "company",
            country,
            new Address("", "", "", ""));
         customer.GenerateNewIdentity();

         var account = new BankAccount();
         account.GenerateNewIdentity();
         account.BankAccountNumber = new BankAccountNumber("4444", "5555", "3333333333", "02");
         account.SetCustomerOwnerOfThisBankAccount(customer);
         account.DepositMoney(1000, "reason");
         var accounts = new List<BankAccount>()
         {
            account
         };

         //Act
         var adapter = TypeAdapterFactory.CreateAdapter();
         var bankAccountsDto = adapter.Adapt<IEnumerable<BankAccount>, List<BankAccountDto>>(accounts);

         //Assert
         Assert.IsNotNull(bankAccountsDto);
         Assert.IsTrue(bankAccountsDto.Count == 1);

         Assert.AreEqual(account.Id, bankAccountsDto[0].Id);
         Assert.AreEqual(account.Iban, bankAccountsDto[0].BankAccountNumber);
         Assert.AreEqual(account.Balance, bankAccountsDto[0].Balance);
         Assert.AreEqual(account.Customer.FirstName, bankAccountsDto[0].CustomerFirstName);
         Assert.AreEqual(account.Customer.LastName, bankAccountsDto[0].CustomerLastName);
      }

   }

}