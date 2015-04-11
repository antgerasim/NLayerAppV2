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

using AutoMapper;

using Infrastructure.Crosscutting.Tests.Classes;

using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.NetFramework.Adapter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Infrastructure.Crosscutting.Tests
{

   [TestClass()]
   public class AutomapperTypeAdapterTests
   {

      [TestMethod()]
      public void AutoMapperTypeAdapterAdaptEntity()
      {
         //Arrange
         Mapper.Initialize(cfg => cfg.AddProfile(new TypeAdapterProfile()));
         var typeAdapter = new AutomapperTypeAdapter();

         var customer = new Customer()
         {
            Id = 1,
            FirstName = "Jhon",
            LastName = "Bep"
         };

         //act

         var dto = typeAdapter.Adapt<Customer, CustomerDto>(customer);

         //Assert
         Assert.IsNotNull(dto);
         Assert.AreEqual(dto.CustomerId, customer.Id);
         Assert.AreEqual(dto.FullName, string.Format("{0},{1}", customer.LastName, customer.FirstName));
      }

      [TestMethod()]
      public void AutoMapperTypeAdapterAdaptEntityEnumerable()
      {
         //Arrange
         Mapper.Initialize(cfg => cfg.AddProfile(new TypeAdapterProfile()));
         var typeAdapter = new AutomapperTypeAdapter();

         var customer = new Customer()
         {
            Id = 1,
            FirstName = "Jhon",
            LastName = "Bep"
         };

         //act

         var dto = typeAdapter.Adapt<IEnumerable<Customer>, List<CustomerDto>>(
            new Customer[]
            {
               customer
            });

         //Assert
         Assert.IsNotNull(dto);
         Assert.IsTrue(dto.Count == 1);
         Assert.AreEqual(dto[0].CustomerId, customer.Id);
         Assert.AreEqual(dto[0].FullName, string.Format("{0},{1}", customer.LastName, customer.FirstName));
      }

   }

}