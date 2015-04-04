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
			
namespace Infrastructure.Data.MainBoundedContext.Tests
{
    using System;
    using System.Linq;

    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.BankingModule.Aggregates.BankAccountAgg;
    using Microsoft.Samples.NLayerApp.Domain.Seedwork;

    using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.BankingModule.Repositories;
    using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;
    using System.Data.Entity.Validation;
    using System.Data.Entity.Infrastructure;
    using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.ERPModule.Repositories;

    /// <summary>
    /// Summary description for BankAccountRepositoryTests
    /// </summary>
    [TestClass]
    public class BankAccountRepositoryTests
    {
        [TestMethod]
        public void BankAccountRepositoryGetMethodReturnMaterializedEntityById()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var bankAccountRepository = new BankAccountRepository(unitOfWork);

            var selectedBankAccount = new Guid("0343C0B0-7C40-444A-B044-B463F36A1A1F");

            //Act
            var bankAccount = bankAccountRepository.Get(selectedBankAccount);

            //Assert
            Assert.IsNotNull(bankAccount);
            Assert.IsTrue(bankAccount.Id == selectedBankAccount);
        }

        [TestMethod]
        public void BankAccountRepositoryGetMethodReturnNullWhenIdIsEmpty()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var bankAccountRepository = new BankAccountRepository(unitOfWork);

            //Act
            var bankAccount = bankAccountRepository.Get(Guid.Empty);

            //Assert
            Assert.IsNull(bankAccount);
        }
        [TestMethod]
        public void BankAccountRepositoryAddNewItemSaveItem()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var customerRepository = new CustomerRepository(unitOfWork);
            var bankAccountRepository = new BankAccountRepository(unitOfWork);
           
            var customer = customerRepository.Get(new Guid("43A38AC8-EAA9-4DF0-981F-2685882C7C45"));
            
            var bankAccountNumber = new BankAccountNumber("1111", "2222", "3333333333", "01");

            var newBankAccount = BankAccountFactory.CreateBankAccount(customer,bankAccountNumber);
            

            //Act
            bankAccountRepository.Add(newBankAccount);

            try
            {
                unitOfWork.Commit();
            }
            catch (DbUpdateException ex)
            {
                var entry = ex.Entries.First();
            }
        }

        [TestMethod()]
        public void BankAccountRepositoryGetAllReturnMaterializedBankAccountsAndCustomers()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var bankAccountRepository = new BankAccountRepository(unitOfWork);

            //Act
            var allItems = bankAccountRepository.GetAll();

            //Assert
            Assert.IsNotNull(allItems);
            Assert.IsTrue(allItems.Any());
            Assert.IsTrue(allItems.All(ba => ba.Customer != null));
        }
        [TestMethod()]
        public void BankAccountRepositoryAllMatchingMethodReturnEntitiesWithSatisfiedCriteria()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var bankAccountRepository = new BankAccountRepository(unitOfWork);

            string iban = string.Format("ES{0} {1} {2} {0}{3}","02","4444","5555","3333333333");

            var spec =BankAccountSpecifications.BankAccountIbanNumber(iban);

            //Act
            var result = bankAccountRepository.AllMatching(spec);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.All(b => b.Iban == iban));
        }
        [TestMethod()]
        public void BankAccountRepositoryFilterMethodReturnEntitisWithSatisfiedFilter()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var bankAccountRepository = new BankAccountRepository(unitOfWork);

            string iban = string.Format("ES{0} {1} {2} {0}{3}", "02", "4444", "5555", "3333333333");
            
            
            //Act
            var allItems = bankAccountRepository.GetFiltered(ba => ba.Iban == iban);

            //Assert
            Assert.IsNotNull(allItems);
            Assert.IsTrue(allItems.All(b => b.Iban == iban));
        }
        [TestMethod()]
        public void BankAccountRepositoryPagedMethodReturnEntitiesInPageFashion()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var bankAccountRepository = new BankAccountRepository(unitOfWork);
           
            //Act
            var pageI = bankAccountRepository.GetPaged(0, 1, b => b.Id, false);
            var pageII = bankAccountRepository.GetPaged(1, 1, b => b.Id, false);

            //Assert
            Assert.IsNotNull(pageI);
            Assert.IsTrue(pageI.Count() == 1);

            Assert.IsNotNull(pageII);
            Assert.IsTrue(pageII.Count() == 1);

            Assert.IsFalse(pageI.Intersect(pageII).Any());
        }

        [TestMethod()]
        public void BankAccountRepositoryRemoveItemDeleteIt()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var customerRepository = new CustomerRepository(unitOfWork);
            var bankAccountRepository = new BankAccountRepository(unitOfWork);

            var customer = customerRepository.Get(new Guid("43A38AC8-EAA9-4DF0-981F-2685882C7C45"));
           
            var bankAccountNumber = new BankAccountNumber("4444", "5555", "3333333333", "02");

            var newBankAccount = BankAccountFactory.CreateBankAccount(customer, bankAccountNumber);
            

            bankAccountRepository.Add(newBankAccount);
            unitOfWork.Commit();

            //Act
            bankAccountRepository.Remove(newBankAccount);
            unitOfWork.Commit();

          
        }
    }
}
