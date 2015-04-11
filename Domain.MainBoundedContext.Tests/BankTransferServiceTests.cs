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

using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.BankingModule.Aggregates.BankAccountAgg;
using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.BankingModule.Services;
using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CountryAgg;
using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.MainBoundedContext.Tests
{

   [TestClass()]
   public class BankTransferServiceTests
   {

      [TestMethod()]
      [ExpectedException(typeof (InvalidOperationException))]
      public void PerformTransferThrowExceptionIfSourceCantWithdrawedWithLockedAccount()
      {
         //Arrange

         var customer = GetCustomer();

         var source = BankAccountFactory.CreateBankAccount(
            customer,
            new BankAccountNumber("1111", "2222", "3333333333", "01"));
         source.DepositMoney(1000, "initial load");
         source.Lock();

         var target = BankAccountFactory.CreateBankAccount(
            customer,
            new BankAccountNumber("1111", "2222", "12312321322", "01"));

         //Act
         var bankTransferService = new BankTransferService();
         bankTransferService.PerformTransfer(10, source, target);
      }

      [TestMethod()]
      [ExpectedException(typeof (InvalidOperationException))]
      public void PerformTransferThrowExceptionIfTargetBankAccountNumberIsEqualToSourceBankAccountNumber()
      {
         //Arrange
         var customer = GetCustomer();

         var source = BankAccountFactory.CreateBankAccount(
            customer,
            new BankAccountNumber("1111", "2222", "3333333333", "01"));
         source.DepositMoney(1000, "initial load");
         source.Lock();

         var target = BankAccountFactory.CreateBankAccount(
            customer,
            new BankAccountNumber("1111", "2222", "3333333333", "01"));

         //Act
         var bankTransferService = new BankTransferService();

         bankTransferService.PerformTransfer(10, source, target);
      }

      [TestMethod()]
      [ExpectedException(typeof (InvalidOperationException))]
      public void PerformTransferThrowExceptionIfSourceCantWithdrawedWithExceedAmoung()
      {
         //Arrange
         var customer = GetCustomer();

         var source = BankAccountFactory.CreateBankAccount(
            customer,
            new BankAccountNumber("1111", "2222", "3333333333", "01"));
         source.DepositMoney(1000, "initial load");

         var target = BankAccountFactory.CreateBankAccount(
            customer,
            new BankAccountNumber("1111", "2222", "12312321322", "01"));

         //Act
         var bankTransferService = new BankTransferService();
         bankTransferService.PerformTransfer(2000, source, target);
      }

      [TestMethod()]
      [ExpectedException(typeof (InvalidOperationException))]
      public void PerformTransferThrowExceptionIfTargetIsLockedAccount()
      {
         //Arrange
         var customer = GetCustomer();

         var source = BankAccountFactory.CreateBankAccount(
            customer,
            new BankAccountNumber("1111", "2222", "3333333333", "01"));
         source.DepositMoney(1000, "initial load");

         var target = BankAccountFactory.CreateBankAccount(
            customer,
            new BankAccountNumber("1111", "2222", "12312321322", "01"));
         target.Lock();

         //Act
         var bankTransferService = new BankTransferService();
         bankTransferService.PerformTransfer(10, source, target);
      }

      [TestMethod()]
      public void PerformTransferCreateActivities()
      {
         //Arrange
         var customer = GetCustomer();

         var source = BankAccountFactory.CreateBankAccount(
            customer,
            new BankAccountNumber("1111", "2222", "3333333333", "01"));
         source.DepositMoney(1000, "initial load");

         var target = BankAccountFactory.CreateBankAccount(
            customer,
            new BankAccountNumber("1111", "2222", "12312321322", "01"));

         //Act

         var activities = source.BankAccountActivity.Count;

         var bankTransferService = new BankTransferService();
         bankTransferService.PerformTransfer(50, source, target);

         //Assert
         Assert.IsNotNull(source.BankAccountActivity);
         Assert.AreEqual(++activities, source.BankAccountActivity.Count);

      }

      private Customer GetCustomer()
      {
         var firstName = "firstName";
         var lastName = "lastName";
         var telephone = string.Empty;
         var company = string.Empty;
         var city = "city";
         var zipCode = "zipCode";
         var addressline1 = "addressline1";
         var addressline2 = "addressline2";

         var country = new Country("spain", "es-ES");
         country.GenerateNewIdentity();

         var address = new Address(city, zipCode, addressline1, addressline2);

         var customer = CustomerFactory.CreateCustomer(firstName, lastName, telephone, company, country, address);

         return customer;
      }

   }

}