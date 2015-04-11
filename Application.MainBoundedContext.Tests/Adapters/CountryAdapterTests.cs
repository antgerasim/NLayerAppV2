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
using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Adapter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.MainBoundedContext.Tests.Adapters
{

   [TestClass()]
   public class CountryAdapterTests
   {

      [TestMethod]
      public void CountryToCountryDtoAdapter()
      {
         //Arrange
         var country = new Country("Spain", "es-ES");
         country.GenerateNewIdentity();

         //Act
         var adapter = TypeAdapterFactory.CreateAdapter();
         var dto = adapter.Adapt<Country, CountryDto>(country);

         //Assert
         Assert.AreEqual(country.Id, dto.Id);
         Assert.AreEqual(country.CountryName, dto.CountryName);
         Assert.AreEqual(country.CountryIsoCode, dto.CountryIsoCode);
      }

      [TestMethod]
      public void CountryEnumerableToCountryDtoList()
      {
         //Arrange

         var country = new Country("Spain", "es-ES");
         country.GenerateNewIdentity();

         IEnumerable<Country> countries = new List<Country>()
         {
            country
         };

         //Act
         var adapter = TypeAdapterFactory.CreateAdapter();
         var dtos = adapter.Adapt<IEnumerable<Country>, List<CountryDto>>(countries);

         //Assert
         Assert.IsNotNull(dtos);
         Assert.IsTrue(dtos.Any());
         Assert.IsTrue(dtos.Count == 1);

         var dto = dtos[0];

         Assert.AreEqual(country.Id, dto.Id);
         Assert.AreEqual(country.CountryName, dto.CountryName);
         Assert.AreEqual(country.CountryIsoCode, dto.CountryIsoCode);
      }

   }

}