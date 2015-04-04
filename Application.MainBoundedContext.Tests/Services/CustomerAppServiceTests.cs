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
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule;
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.DTO;
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule;
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services;
    using Microsoft.Samples.NLayerApp.Application.Seedwork;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CountryAgg;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CountryAgg.Moles;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg.Moles;
    using Microsoft.Samples.NLayerApp.Domain.Seedwork.Moles;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Adapter;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Logging;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.NetFramework.Adapter;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.NetFramework.Logging;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.NetFramework.Validator;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Validator;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass()]
    public class CustomerAppServiceTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AddNewCustomerThrowExceptionIfCustomerDtoIsNull()
        {
            //Arrange
            var countryRepository = new SICountryRepository();
            var customerRepository = new SICustomerRepository();

            var customerManagementService = new CustomerAppService(countryRepository, customerRepository);


            //act
            var result = customerManagementService.AddNewCustomer(null);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AddNewCustomerThrowArgumentExceptionIfCustomerCountryInformationIsEmpty()
        {
            //Arrange
            var countryRepository = new SICountryRepository();
            var customerRepository = new SICustomerRepository();

            var customerManagementService = new CustomerAppService(countryRepository, customerRepository);

            var customerDTO = new CustomerDTO()
            {
                CountryId = Guid.Empty
            };

            //act
            var result = customerManagementService.AddNewCustomer(customerDTO);
        }

        [TestMethod()]
        public void AddNewCustomerReturnAdaptedDTO()
        {
            //Arrange
             
            var countryRepository = new SICountryRepository();
            countryRepository.GetGuid = (guid) =>
            {
                var country = new Country("Spain", "es-ES"); ;
                country.ChangeCurrentIdentity(guid);

                return country;
            };
            var customerRepository = new SICustomerRepository();
            customerRepository.AddCustomer = (customer) => { };
            customerRepository.UnitOfWorkGet = () =>
            {
                var uow = new SIUnitOfWork();
                uow.Commit = () => { };

                return uow;
            };

            var customerManagementService = new CustomerAppService(countryRepository, customerRepository);

            var customerDTO = new CustomerDTO()
            {
                CountryId = Guid.NewGuid(),
                FirstName = "Jhon",
                LastName = "El rojo"
            };

            //act
            var result = customerManagementService.AddNewCustomer(customerDTO);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id != Guid.Empty);
            Assert.AreEqual(result.FirstName, customerDTO.FirstName);
            Assert.AreEqual(result.LastName, customerDTO.LastName);
        }


        [TestMethod()]
        [ExpectedException(typeof(ApplicationValidationErrorsException))]
        public void AddNewCustomerThrowApplicationErrorsWhenEntityIsNotValid()
        {
            //Arrange
            var countryId = Guid.NewGuid();

            var countryRepository = new SICountryRepository();
            countryRepository.GetGuid = (guid)=>
            {
                var country = new Country("spain", "es-ES");
                country.GenerateNewIdentity();

                return country;
            };
            var customerRepository = new SICustomerRepository();
            customerRepository.AddCustomer = (customer) => { };
            customerRepository.UnitOfWorkGet = () =>
            {
                var uow = new SIUnitOfWork();
                uow.Commit = () => { };

                return uow;
            };

            var customerManagementService = new CustomerAppService(countryRepository, customerRepository);

            var customerDTO = new CustomerDTO() //missing lastname
            {
                CountryId = Guid.NewGuid(),
                FirstName = "Jhon"
            };

            //act
            var result = customerManagementService.AddNewCustomer(customerDTO);
        }
        [TestMethod()]
        public void RemoveCustomerSetCustomerAsDisabled()
        {
            //Arrange
            var country = new Country("spain", "es-ES");
            country.GenerateNewIdentity();

            Guid customerId = Guid.NewGuid();
            
            var countryRepository = new SICountryRepository();
            var customerRepository = new SICustomerRepository();

            customerRepository.UnitOfWorkGet = () =>
            {
                var uow = new SIUnitOfWork();
                uow.Commit = () => { };

                return uow;
            };

            var customer = CustomerFactory.CreateCustomer("Jhon", "El rojo","+3434","company",country, new Address("city", "zipCode", "address line", "address line"));
            customer.ChangeCurrentIdentity(customerId);



            customerRepository.GetGuid = (guid) =>
            {
                return customer;
            };

            //Act
            var customerManagementService = new CustomerAppService(countryRepository, customerRepository);
            customerManagementService.RemoveCustomer(customerId);

            //Assert
            Assert.IsFalse(customer.IsEnabled);
        }

        [TestMethod()]
        public void UpdateCustomerMergePersistentAndCurrent()
        {
            //Arrange
            var country = new Country("spain", "es-ES");
            country.GenerateNewIdentity();

            Guid customerId = Guid.NewGuid();
            
            var countryRepository = new SICountryRepository();
            var customerRepository = new SICustomerRepository();

            customerRepository.UnitOfWorkGet = () =>
            {
                var uow = new SIUnitOfWork();
                uow.Commit = () => { };

                return uow;
            };

            customerRepository.GetGuid = (guid) =>
            {
                var customer = CustomerFactory.CreateCustomer("Jhon",
                                                               "El rojo",
                                                               "+3434",
                                                               "company",
                                                               country,
                                                               new Address("city", "zipCode", "address line", "address line"));
                customer.ChangeCurrentIdentity(customerId);

                return customer;
            };

            customerRepository.MergeCustomerCustomer = (persistent, current) =>
            {
                Assert.AreEqual(persistent, current);
                Assert.IsTrue(persistent != null);
                Assert.IsTrue(current != null);
            };

            var customerManagementService = new CustomerAppService(countryRepository, customerRepository);

            var customerDTO = new CustomerDTO() //missing lastname
            {
                Id = customerId,
                CountryId = country.Id,
                FirstName = "Jhon",
                LastName = "El rojo",
            };

            //act
            customerManagementService.UpdateCustomer(customerDTO);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void FindCustomersWithInvalidPageArgumentsThrowArgumentException()
        {
            //Arrange
            var countryRepository = new SICountryRepository();
            var customerRepository = new SICustomerRepository();

            var customerManagementService = new CustomerAppService(countryRepository, customerRepository);

            //Act
            customerManagementService.FindCustomers(-1, 0);
            
        }
        [TestMethod()]
        public void FindCustomersInPageReturnNullIfNotData()
        {
            //Arrange
            var countryRepository = new SICountryRepository();
            var customerRepository = new SICustomerRepository();
            customerRepository.GetEnabledInt32Int32 = (index, count) =>
            {
                return new List<Customer>();
            };

            var customerManagementService = new CustomerAppService(countryRepository, customerRepository);

            //Act
            var result = customerManagementService.FindCustomers(0, 1);

            //Assert
            Assert.IsNull(result);

        }
        [TestMethod()]
        public void FindCustomersByFilterReturnNullIfNotData()
        {
            //Arrange
            var countryRepository = new SICountryRepository();
            var customerRepository = new SICustomerRepository();
            customerRepository.AllMatchingISpecificationOfCustomer = (spec) =>
            {
                return new List<Customer>();
            };

            var customerManagementService = new CustomerAppService(countryRepository, customerRepository);

            //Act
            var result = customerManagementService.FindCustomers("text");

            //Assert
            Assert.IsNull(result);

        }
        [TestMethod()]
        public void FindCustomersInPageMaterializeResults()
        {
            //Arrange
            var countryRepository = new SICountryRepository();
            var customerRepository = new SICustomerRepository();
            var country = new Country("spain", "es-ES");
            country.GenerateNewIdentity();

            customerRepository.GetEnabledInt32Int32 = (index, count) =>
            {
                var customers = new List<Customer>();
                customers.Add(CustomerFactory.CreateCustomer("Jhon",
                                                            "El rojo",
                                                            "+343",
                                                            "company",
                                                             country,
                                                             new Address("city", "zipCode", "address line", "address line2")));
                return customers;
            };


            var customerManagementService = new CustomerAppService(countryRepository, customerRepository);

            //Act
            var result = customerManagementService.FindCustomers(0, 1);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 1);
        }

        [TestMethod()]
        public void FindCustomersByFilterMaterializeResults()
        {
            //Arrange
            var countryRepository = new SICountryRepository();
            var customerRepository = new SICustomerRepository();
            var country = new Country("Spain", "es-ES");
            country.GenerateNewIdentity();

            customerRepository.AllMatchingISpecificationOfCustomer = (spec) =>
            {
                var customers = new List<Customer>();
                customers.Add(CustomerFactory.CreateCustomer("Jhon",
                                                            "El rojo",
                                                            "+34343",
                                                            "company",
                                                             country,
                                                             new Address("city", "zipCode", "address line", "address line2")));
                return customers;
            };


            var customerManagementService = new CustomerAppService(countryRepository, customerRepository);

            //Act
            var result = customerManagementService.FindCustomers("Jhon");

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 1);
        }
        [TestMethod()]
        public void FindCustomerReturnNullIfCustomerIdIsEmpty()
        {
            //Arrange
            var countryRepository = new SICountryRepository();
            var customerRepository = new SICustomerRepository();
            customerRepository.GetGuid = (guid) =>
            {
                if (guid == Guid.Empty)
                    return null;
                else
                    return new Customer();
            };
            var customerManagementService = new CustomerAppService(countryRepository, customerRepository);

            //Act
            var result = customerManagementService.FindCustomer(Guid.Empty);

            //Assert
            Assert.IsNull(result);

        }
        [TestMethod()]
        public void FindCustomerMaterializaResultIfExist()
        {
            //Arrange
            var countryRepository = new SICountryRepository();
            var customerRepository = new SICustomerRepository();
            var country = new Country("spain", "es-ES");
            country.GenerateNewIdentity();

            customerRepository.GetGuid = (guid) =>
            {
                return CustomerFactory.CreateCustomer("Jhon",
                                                      "El rojo",
                                                      "+3434344",
                                                      "company",
                                                      country,
                                                      new Address("city", "zipCode", "address line1", "address line2"));

            };

            var customerManagementService = new CustomerAppService(countryRepository, customerRepository);

            //Act
            var result = customerManagementService.FindCustomer(Guid.NewGuid());

            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void FindCountriesWithInvalidPageArgumentsReturnNull()
        {
            //Arrange
            var countryRepository = new SICountryRepository();
            var customerRepository = new SICustomerRepository();

            var customerManagementService = new CustomerAppService(countryRepository, customerRepository);

            //Act
            customerManagementService.FindCountries(-1, 0);
        }
        [TestMethod()]
        public void FindCountriesInPageReturnNullIfNotData()
        {
            //Arrange
            var customerRepository = new SICustomerRepository();
            var countryRepository = new SICountryRepository();
            countryRepository.GetPagedInt32Int32ExpressionOfFuncOfCountryKPropertyBoolean<string>((index, count, order, ascending) =>
            {
                return new List<Country>();
            });

            var customerManagementService = new CustomerAppService(countryRepository, customerRepository);

            //Act
            var result = customerManagementService.FindCountries(0, 1);

            //Assert
            Assert.IsNull(result);
        }
        [TestMethod()]
        public void FindCountriesInPageMaterializeResults()
        {
            //Arrange
            var customerRepository = new SICustomerRepository();
            var countryRepository = new SICountryRepository();
            countryRepository.GetPagedInt32Int32ExpressionOfFuncOfCountryKPropertyBoolean<string>((index, count, order, ascending) =>
            {
                var country = new Country("country name", "country iso");
                country.GenerateNewIdentity();

                return new List<Country>()
                {
                    country
                };
            });

            var customerManagementService = new CustomerAppService(countryRepository, customerRepository);

            //Act
            var result = customerManagementService.FindCountries(0, 1);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 1);
        }
        [TestMethod()]
        public void FindCountriesByFilterMaterializeResults()
        {
            //Arrange
            var customerRepository = new SICustomerRepository();
            var countryRepository = new SICountryRepository();
            countryRepository.AllMatchingISpecificationOfCountry = (spec) =>
            {

                var country = new Country("country name", "country iso");
                country.GenerateNewIdentity();

                return new List<Country>()
                {
                   country
                };
            };

            var customerManagementService = new CustomerAppService(countryRepository, customerRepository);

            //Act
            var result = customerManagementService.FindCountries("filter");

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 1);
        }
        [TestMethod()]
        public void FindCountriesByFilterReturnNullIfNotData()
        {
            //Arrange
            var customerRepository = new SICustomerRepository();
            var countryRepository = new SICountryRepository();
            countryRepository.AllMatchingISpecificationOfCountry = (spec) =>
            {
                return new List<Country>();
            };

            var customerManagementService = new CustomerAppService(countryRepository, customerRepository);

            //Act
            var result = customerManagementService.FindCountries("filter");

            //Assert
            Assert.IsNull(result);
        }
        
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorThrowExceptionWhenCustomerRepositoryDependencyIsNull()
        {
            //Arrange
            SICustomerRepository customerRepository = null;
            var countryRepository = new SICountryRepository();

            //act
            var customerManagementService = new CustomerAppService(countryRepository, customerRepository);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorThrowExceptionWhenCountryRepositoryDependencyIsNull()
        {
            //Arrange
            var customerRepository = new SICustomerRepository();
            SICountryRepository countryRepository = null;

            //act
            var customerManagementService = new CustomerAppService(countryRepository, customerRepository);

        }
    }
}
