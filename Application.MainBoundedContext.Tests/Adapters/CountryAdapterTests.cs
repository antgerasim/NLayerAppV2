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
    using Microsoft.Samples.NLayerApp.Domain.Seedwork;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CountryAgg;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Adapter;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.DTO;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.NetFramework.Adapter;
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule;
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule;

    [TestClass()]
    public class CountryAdapterTests
    {
        [TestMethod]
        public void CountryToCountryDTOAdapter()
        {
            //Arrange
            var country = new Country("Spain", "es-ES");
            country.GenerateNewIdentity();

            //Act
            ITypeAdapter adapter = TypeAdapterFactory.CreateAdapter();
            var dto = adapter.Adapt<Country, CountryDTO>(country);

            //Assert
            Assert.AreEqual(country.Id, dto.Id);
            Assert.AreEqual(country.CountryName, dto.CountryName);
            Assert.AreEqual(country.CountryISOCode, dto.CountryISOCode);
        }
        [TestMethod]
        public void CountryEnumerableToCountryDTOList()
        {
            //Arrange

            var country = new Country("Spain", "es-ES");
            country.GenerateNewIdentity();

            IEnumerable<Country> countries = new List<Country>() { country };

            //Act
            ITypeAdapter adapter = TypeAdapterFactory.CreateAdapter();
            var dtos = adapter.Adapt<IEnumerable<Country>, List<CountryDTO>>(countries);

            //Assert
            Assert.IsNotNull(dtos);
            Assert.IsTrue(dtos.Any());
            Assert.IsTrue(dtos.Count == 1);

            var dto = dtos[0];

            Assert.AreEqual(country.Id, dto.Id);
            Assert.AreEqual(country.CountryName, dto.CountryName);
            Assert.AreEqual(country.CountryISOCode, dto.CountryISOCode);
        }
    }
}
