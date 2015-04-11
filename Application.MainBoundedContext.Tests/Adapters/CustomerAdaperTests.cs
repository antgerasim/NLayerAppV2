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

using System.Collections.Generic;
using System.Linq;

using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.DTO;
using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CountryAgg;
using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;
using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Adapter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.MainBoundedContext.Tests.Adapters
{

   [TestClass]
   public class CustomerAdaperTests
   {

      [TestMethod]
      public void CustomerToCustomerDtoAdapt()
      {
         //Arrange

         var country = new Country("Spain", "es-ES");
         country.GenerateNewIdentity();

         var address = new Address("Monforte", "27400", "AddressLine1", "AddressLine2");

         var customer = CustomerFactory.CreateCustomer("Jhon", "El rojo", "617404929", "Spirtis", country, address);
         var picture = new Picture
         {
            RawPhoto = new byte[0]
            {
            }
         };

         customer.ChangeTheCurrentCredit(1000M);
         customer.ChangePicture(picture);
         customer.SetTheCountryForThisCustomer(country);

         //Act

         var adapter = TypeAdapterFactory.CreateAdapter();
         var dto = adapter.Adapt<Customer, CustomerDto>(customer);

         //Assert

         Assert.AreEqual(customer.Id, dto.Id);
         Assert.AreEqual(customer.FirstName, dto.FirstName);
         Assert.AreEqual(customer.LastName, dto.LastName);
         Assert.AreEqual(customer.Company, dto.Company);
         Assert.AreEqual(customer.Telephone, dto.Telephone);
         Assert.AreEqual(customer.CreditLimit, dto.CreditLimit);

         Assert.AreEqual(customer.Country.CountryName, dto.CountryCountryName);
         Assert.AreEqual(country.Id, dto.CountryId);

         Assert.AreEqual(customer.Address.City, dto.AddressCity);
         Assert.AreEqual(customer.Address.ZipCode, dto.AddressZipCode);
         Assert.AreEqual(customer.Address.AddressLine1, dto.AddressAddressLine1);
         Assert.AreEqual(customer.Address.AddressLine2, dto.AddressAddressLine2);
      }

      [TestMethod]
      public void CustomerEnumerableToCustomerListDtoListAdapt()
      {
         //Arrange

         var country = new Country("Spain", "es-ES");
         country.GenerateNewIdentity();

         var address = new Address("Monforte", "27400", "AddressLine1", "AddressLine2");

         var customer = CustomerFactory.CreateCustomer("Jhon", "El rojo", "617404929", "Spirtis", country, address);
         var picture = new Picture
         {
            RawPhoto = new byte[0]
            {
            }
         };

         customer.ChangeTheCurrentCredit(1000M);
         customer.ChangePicture(picture);
         customer.SetTheCountryForThisCustomer(country);

         IEnumerable<Customer> customers = new List<Customer>()
         {
            customer
         };

         //Act
         var adapter = TypeAdapterFactory.CreateAdapter();

         var dtos = adapter.Adapt<IEnumerable<Customer>, List<CustomerListDto>>(customers);

         //Assert

         Assert.IsNotNull(dtos);
         Assert.IsTrue(dtos.Any());
         Assert.IsTrue(dtos.Count == 1);

         var dto = dtos[0];

         Assert.AreEqual(customer.Id, dto.Id);
         Assert.AreEqual(customer.FirstName, dto.FirstName);
         Assert.AreEqual(customer.LastName, dto.LastName);
         Assert.AreEqual(customer.Company, dto.Company);
         Assert.AreEqual(customer.Telephone, dto.Telephone);
         Assert.AreEqual(customer.CreditLimit, dto.CreditLimit);
         Assert.AreEqual(customer.Address.City, dto.AddressCity);
         Assert.AreEqual(customer.Address.ZipCode, dto.AddressZipCode);
         Assert.AreEqual(customer.Address.AddressLine1, dto.AddressAddressLine1);
         Assert.AreEqual(customer.Address.AddressLine2, dto.AddressAddressLine2);

      }

   }

}