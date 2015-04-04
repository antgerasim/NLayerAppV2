
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

    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;
    using Microsoft.Samples.NLayerApp.Domain.Seedwork;

    using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.ERPModule.Repositories;
    using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CountryAgg;

    [TestClass()]
    public class CustomerRepositoryTests
    {
        [TestMethod()]
        public void CustomerRepositoryGetMethodReturnCustomerWithPicture()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var customerRepository = new CustomerRepository(unitOfWork);

            var customerId = new Guid("43A38AC8-EAA9-4DF0-981F-2685882C7C45");

            
            //Act
            var customer = customerRepository.Get(customerId);

            //Assert
            Assert.IsNotNull(customer);
            Assert.IsNotNull(customer.Picture);
            Assert.IsTrue(customer.Id == customerId);
        }

        [TestMethod()]
        public void CustomerRepositoryGetMethodReturnNullWhenIdIsEmpty()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var customerRepository = new CustomerRepository(unitOfWork);

            //Act
            var customer = customerRepository.Get(Guid.Empty);

            //Assert
            Assert.IsNull(customer);
        }

        [TestMethod()]
        public void CustomerRepositoryGetEnalbedReturnOnlyEnabledCustomers()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var customerRepository = new CustomerRepository(unitOfWork);

            
            //Act
            var result = customerRepository.GetEnabled(0, 10);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
            Assert.IsTrue(result.All(c => c.IsEnabled));
        }

        [TestMethod()]
        public void CustomerRepositoryAddNewItemSaveItem()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var customerRepository = new CustomerRepository(unitOfWork);

            var country = new Country("spain", "es-ES");
            country.ChangeCurrentIdentity(new Guid("32BB805F-40A4-4C37-AA96-B7945C8C385C"));

            var customer = CustomerFactory.CreateCustomer("Felix", "Trend","+3434","company", country, new Address("city", "zipCode", "addressLine1", "addressLine2"));
            customer.SetTheCountryReference(country.Id);


            //Act
            customerRepository.Add(customer);
            unitOfWork.Commit();
        }

        [TestMethod()]
        public void CustomerRepositoryGetAllReturnMaterializedAllItems()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var customerRepository = new CustomerRepository(unitOfWork);

            //Act
            var allItems = customerRepository.GetAll();

            //Assert
            Assert.IsNotNull(allItems);
            Assert.IsTrue(allItems.Any());
        }

        [TestMethod()]
        public void CustomerRepositoryAllMatchingMethodReturnEntitiesWithSatisfiedCriteria()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var customerRepository = new CustomerRepository(unitOfWork);
            
            var spec = CustomerSpecifications.EnabledCustomers();

            //Act
            var result = customerRepository.AllMatching(spec);

            //Assert
            Assert.IsNotNull(result.All(c => c.IsEnabled));

        }

        [TestMethod()]
        public void CustomerRepositoryFilterMethodReturnEntitisWithSatisfiedFilter()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var customerRepository = new CustomerRepository(unitOfWork);

            //Act
            var result = customerRepository.GetFiltered(c => c.CreditLimit > 0);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.All(c => c.CreditLimit>0));
        }

        [TestMethod()]
        public void CustomerRepositoryPagedMethodReturnEntitiesInPageFashion()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var customerRepository = new CustomerRepository(unitOfWork);

            //Act
            var pageI = customerRepository.GetPaged(0, 1, b => b.Id, false);
            var pageII = customerRepository.GetPaged(1, 1, b => b.Id, false);

            //Assert
            Assert.IsNotNull(pageI);
            Assert.IsTrue(pageI.Count() == 1);

            Assert.IsNotNull(pageII);
            Assert.IsTrue(pageII.Count() == 1);

            Assert.IsFalse(pageI.Intersect(pageII).Any());
        }
        [TestMethod()]
        public void CustomerRepositoryRemoveItemDeleteIt()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var customerRepository = new CustomerRepository(unitOfWork);

            var country = new Country("Spain","es-ES");
            country.ChangeCurrentIdentity(new Guid("32BB805F-40A4-4C37-AA96-B7945C8C385C"));

            

            var address =  new Address("city", "zipCode", "addressline1", "addressline2");

            var customer = CustomerFactory.CreateCustomer("Frank", "Frank","+3444","company", country,address);
            customer.SetTheCountryReference(country.Id);
            
            customerRepository.Add(customer);
            unitOfWork.Commit();

            //Act
            customerRepository.Remove(customer);
            unitOfWork.Commit();

            var result = customerRepository.Get(customer.Id);

            //Assert
            Assert.IsNull(result);
        }
    }
}
