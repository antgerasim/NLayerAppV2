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


namespace Application.MainBoundedContext.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services;
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.DTO;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.BankingModule.Aggregates.BankAccountAgg;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.BankingModule.Aggregates.BankAccountAgg.Moles;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.BankingModule.Services;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg.Moles;
    using Microsoft.Samples.NLayerApp.Domain.Seedwork.Moles;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Adapter;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Adapter.Moles;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Logging;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.NetFramework.Logging;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.NetFramework.Validator;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Validator;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.NetFramework.Adapter;
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule;
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule;

    [TestClass()]
    public class BankAppServiceTests
    {
        [TestMethod()]
        public void LockBankAccountReturnFalseIfIdentifierIsEmpty()
        {
            //Arrange
            SIBankAccountRepository bankAccountRepository = new SIBankAccountRepository();
            bankAccountRepository.GetGuid = guid =>
            {
                if (guid == Guid.Empty)
                    return null;
                else
                    return new BankAccount { };
            };
            SICustomerRepository customerRepository = new SICustomerRepository();
            IBankTransferService transferService = new BankTransferService();

            IBankAppService bankingService = new BankAppService(bankAccountRepository, customerRepository, transferService);

            //Act
            var result = bankingService.LockBankAccount(Guid.Empty);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod()]
        public void LockBankAccountReturnFalseIfBankAccountNotExist()
        {
            //Arrange
            SICustomerRepository customerRepository = new SICustomerRepository();
            IBankTransferService transferService = new BankTransferService();
            SIBankAccountRepository bankAccountRepository = new SIBankAccountRepository();
            bankAccountRepository.UnitOfWorkGet = () =>
            {
                var uow = new SIUnitOfWork();
                uow.Commit = () => { };

                return uow;
            };
            bankAccountRepository.GetGuid = (guid) =>
            {
                return null;
            };

            IBankAppService bankingService = new BankAppService(bankAccountRepository, customerRepository, transferService);

            //Act
            var result = bankingService.LockBankAccount(Guid.NewGuid());

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod()]
        public void LockBankAccountReturnTrueIfBankAccountIsLocked()
        {
            //Arrange
            SICustomerRepository customerRepository = new SICustomerRepository();
            IBankTransferService transferService = new BankTransferService();
            
            SIBankAccountRepository bankAccountRepository = new SIBankAccountRepository();
            bankAccountRepository.UnitOfWorkGet = () =>
            {
                var uow = new SIUnitOfWork();
                uow.Commit = () => { };

                return uow;
            };

            bankAccountRepository.GetGuid = (guid) =>
            {
                var customer = new Customer();
                customer.GenerateNewIdentity();

                var bankAccount = new BankAccount();
                bankAccount.GenerateNewIdentity();

                bankAccount.SetCustomerOwnerOfThisBankAccount(customer);

                return bankAccount;
            };

            IBankAppService bankingService = new BankAppService(bankAccountRepository, customerRepository, transferService);

            //Act
            var result = bankingService.LockBankAccount(Guid.NewGuid());

            //Assert
            Assert.IsTrue(result);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AddBankAccountThrowArgumentNullExceptionWhenBankAccountDTOIsNull()
        {
            //Arrange
            SIBankAccountRepository bankAccountRepository = new SIBankAccountRepository();
            SICustomerRepository customerRepository = new SICustomerRepository();
            IBankTransferService transferService = new BankTransferService();
            
            IBankAppService bankingService = new BankAppService(bankAccountRepository, customerRepository, transferService);

            //Act
            var result = bankingService.AddBankAccount(null);

            //Assert

            Assert.IsNull(result);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AddBankAccountReturnNullWhenCustomerIdIsEmpty()
        {
            //Arrange
            SIBankAccountRepository bankAccountRepository = new SIBankAccountRepository();
            SICustomerRepository customerRepository = new SICustomerRepository();
            IBankTransferService transferService = new BankTransferService();
          
            var dto = new BankAccountDTO()
            {
                CustomerId = Guid.Empty
            };

            IBankAppService bankingService = new BankAppService(bankAccountRepository, customerRepository, transferService);

            //Act
            var result = bankingService.AddBankAccount(dto);
        }
        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddBankAccountThrowInvalidOperationExceptionWhenCustomerNotExist()
        {
            //Arrange
            SIBankAccountRepository bankAccountRepository = new SIBankAccountRepository();
            SICustomerRepository customerRepository = new SICustomerRepository();
            customerRepository.GetGuid = (guid) => { return null; };

            IBankTransferService transferService = new BankTransferService();

            var dto = new BankAccountDTO()
            {
                CustomerId = Guid.NewGuid()
            };

            IBankAppService bankingService = new BankAppService(bankAccountRepository, customerRepository, transferService);

            //Act
            bankingService.AddBankAccount(dto);
        }

        [TestMethod()]
        public void AddBankAccountReturnDTOWhenSaveSucceed()
        {
            //Arrange
            IBankTransferService transferService = new BankTransferService();

            SICustomerRepository customerRepository = new SICustomerRepository();
            customerRepository.GetGuid = (guid) =>
            {
                var customer = new Customer()
                {
                    FirstName = "Jhon",
                    LastName = "El rojo"
                };

                customer.ChangeCurrentIdentity(guid);

                return customer;
            };

            SIBankAccountRepository bankAccountRepository = new SIBankAccountRepository();
            bankAccountRepository.AddBankAccount = (ba) => { };
            bankAccountRepository.UnitOfWorkGet = () =>
            {
                var uow = new SIUnitOfWork();
                uow.Commit = () => { };

                return uow;
            };


            var dto = new BankAccountDTO()
            {
                CustomerId = Guid.NewGuid(),
                BankAccountNumber = "BA"
            };

            IBankAppService bankingService = new BankAppService(bankAccountRepository, customerRepository, transferService);

            //Act
            var result = bankingService.AddBankAccount(dto);

            //Assert
            Assert.IsNotNull(result);

        }

        [TestMethod()]
        public void FindBankAccountsReturnAllItems()
        {
            SIBankAccountRepository bankAccountRepository = new SIBankAccountRepository();
            bankAccountRepository.GetAll = () =>
            {
                var customer = new Customer();
                customer.GenerateNewIdentity();

                var bankAccount = new BankAccount()
                {
                    BankAccountNumber = new BankAccountNumber("4444", "5555", "3333333333", "02"),
                };
                bankAccount.SetCustomerOwnerOfThisBankAccount(customer);
                bankAccount.GenerateNewIdentity();

                var accounts = new List<BankAccount>(){ bankAccount };

                return accounts;

            };

            SICustomerRepository customerRepository = new SICustomerRepository();
            IBankTransferService transferService = new BankTransferService();

            IBankAppService bankingService = new BankAppService(bankAccountRepository, customerRepository, transferService);

            //Act
            var result = bankingService.FindBankAccounts();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 1);

        }

        [TestMethod()]
        public void FindBankAccountActivitiesReturnNullWhenBankAccountIdIsEmpty()
        {
            //Arrange

            SIBankAccountRepository bankAccountRepository = new SIBankAccountRepository();
            bankAccountRepository.GetGuid = guid =>
            {
                if (guid == Guid.Empty)
                    return null;
                else
                    return new BankAccount { };
            };
            SICustomerRepository customerRepository = new SICustomerRepository();
            IBankTransferService transferService = new BankTransferService();

            IBankAppService bankingService = new BankAppService(bankAccountRepository, customerRepository, transferService);

            //Act
            var result = bankingService.FindBankAccountActivities(Guid.Empty);


            //Assert
            Assert.IsNull(result);
        }
        [TestMethod()]
        public void FindBankAccountActivitiesReturnNullWhenBankAccountNotExists()
        {
            //Arrange

            SIBankAccountRepository bankAccountRepository = new SIBankAccountRepository();
            bankAccountRepository.GetGuid = (guid) =>
            {
                return null;
            };
            SICustomerRepository customerRepository = new SICustomerRepository();
            IBankTransferService transferService = new BankTransferService();

            IBankAppService bankingService = new BankAppService(bankAccountRepository, customerRepository, transferService);

            //Act
            var result = bankingService.FindBankAccountActivities(Guid.NewGuid());


            //Assert
            Assert.IsNull(result);
        }
        [TestMethod()]
        public void FindBankAccountActivitiesReturnAllItems()
        {
            //Arrange
            SIBankAccountRepository bankAccountRepository = new SIBankAccountRepository();
            bankAccountRepository.GetGuid = (guid) =>
            {
                var bActivity1 = new BankAccountActivity() { Date = DateTime.Now, Amount = 1000 };
                bActivity1.GenerateNewIdentity();

                var bActivity2 = new BankAccountActivity() { Date = DateTime.Now, Amount = 1000 };
                bActivity2.GenerateNewIdentity();

                var bankAccount = new BankAccount()
                {
                    BankAccountActivity = new HashSet<BankAccountActivity>(){ bActivity1,bActivity2}
                };
                bankAccount.GenerateNewIdentity();

                return bankAccount;
            };

            SICustomerRepository customerRepository = new SICustomerRepository();
            IBankTransferService transferService = new BankTransferService();

            IBankAppService bankingService = new BankAppService(bankAccountRepository, customerRepository, transferService);

            //Act
            var result = bankingService.FindBankAccountActivities(Guid.NewGuid());


            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 2);
        }

        [TestMethod()]
        public void PerformBankTransfer()
        {
            //Arrange

            //--> source bank account data

            var sourceId = new Guid("3481009C-A037-49DB-AE05-44FF6DB67DEC");
            var bankAccountNumberSource = new BankAccountNumber("4444", "5555", "3333333333", "02");
            var sourceCustomer = new Customer();
            sourceCustomer.GenerateNewIdentity();

            var source = BankAccountFactory.CreateBankAccount(sourceCustomer, bankAccountNumberSource);
            source.ChangeCurrentIdentity(sourceId);
            source.DepositMoney(1000, "initial");

            var sourceBankAccountDTO = new BankAccountDTO()
            {
                Id = sourceId,
                BankAccountNumber = source.Iban
            };

            //--> target bank account data
            var targetCustomer = new Customer();
            targetCustomer.GenerateNewIdentity();
            var targetId = new Guid("8A091975-F783-4730-9E03-831E9A9435C1");
            var bankAccountNumberTarget = new BankAccountNumber("1111", "2222", "3333333333", "01");
            var target = BankAccountFactory.CreateBankAccount(targetCustomer, bankAccountNumberTarget);
            target.ChangeCurrentIdentity(targetId);


            var targetBankAccountDTO = new BankAccountDTO()
            {
                Id = targetId,
                BankAccountNumber = target.Iban
            };

            var accounts = new List<BankAccount>() { source, target };


            SIBankAccountRepository bankAccountRepository = new SIBankAccountRepository();
            bankAccountRepository.GetGuid = (guid) =>
            {
                return accounts.Where(ba => ba.Id == guid).SingleOrDefault();
            };
            bankAccountRepository.UnitOfWorkGet = () =>
            {
                var unitOfWork = new SIUnitOfWork();
                unitOfWork.Commit = () => { };

                return unitOfWork;
            };

            SICustomerRepository customerRepository = new SICustomerRepository();
            IBankTransferService transferService = new BankTransferService();

            IBankAppService bankingService = new BankAppService(bankAccountRepository, customerRepository, transferService);

            //Act
            bankingService.PerformBankTransfer(sourceBankAccountDTO, targetBankAccountDTO, 100M);


            //Assert
            Assert.AreEqual(source.Balance, 900);
            Assert.AreEqual(target.Balance, 100);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorThrowExceptionIfBankTransferServiceDependencyIsNull()
        {
            //Arrange
            SICustomerRepository customerRepository = new SICustomerRepository();
            SIBankAccountRepository bankAcccountRepository = new SIBankAccountRepository();
            IBankTransferService transferService = null;
            

            //Act
            IBankAppService bankingService = new BankAppService(bankAcccountRepository, customerRepository, transferService);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorThrowExceptionIfCustomerRepositoryDependencyIsNull()
        {
            //Arrange
            SICustomerRepository customerRepository = null;
            SIBankAccountRepository bankAcccountRepository = new SIBankAccountRepository();
            IBankTransferService transferService = new BankTransferService();
            

            //Act
            IBankAppService bankingService = new BankAppService(bankAcccountRepository, customerRepository, transferService);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorThrowExceptionIfBankAccountRepositoryDependencyIsNull()
        {
            //Arrange
            SICustomerRepository customerRepository = new SICustomerRepository();
            SIBankAccountRepository bankAcccountRepository = null;
            IBankTransferService transferService = new BankTransferService();

            //Act
            IBankAppService bankingService = new BankAppService(bankAcccountRepository, customerRepository, transferService);

        }

        
    }
}
