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

    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CountryAgg;
    using Microsoft.Samples.NLayerApp.Domain.Seedwork;
    using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.ERPModule.Repositories;
    using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass()]
    public class CountryRepositoryTests
    {
        [TestMethod()]
        public void CountryRepositoryGetMethodReturnMaterializedEntityById()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var countryRepository = new CountryRepository(unitOfWork);
            var countryId = new Guid("32BB805F-40A4-4C37-AA96-B7945C8C385C");

            //Act
            var country = countryRepository.Get(countryId);
            
            //Assert
            Assert.IsNotNull(country);
            Assert.IsTrue(country.Id == countryId);
        }

        [TestMethod()]
        public void CountryRepositoryGetMethodReturnNullWhenIdIsEmpty()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var countryRepository = new CountryRepository(unitOfWork);

            //Act
            var country = countryRepository.Get(Guid.Empty);

            //Assert
            Assert.IsNull(country);
        }

        [TestMethod()]
        public void CountryRepositoryAddNewItemSaveItem()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var countryRepository = new CountryRepository(unitOfWork);

            var country = new Country("France", "fr-FR");
            country.GenerateNewIdentity();

            //Act
            countryRepository.Add(country);
            unitOfWork.Commit();
        }

        [TestMethod()]
        public void CountryRepositoryGetAllReturnMaterializedAllItems()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var countryRepository = new CountryRepository(unitOfWork);

            //Act
            var allItems = countryRepository.GetAll();

            //Assert
            Assert.IsNotNull(allItems);
            Assert.IsTrue(allItems.Any());
        }

        [TestMethod()]
        public void CountryRepositoryAllMatchingMethodReturnEntitiesWithSatisfiedCriteria()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var countryRepository = new CountryRepository(unitOfWork);

            string textToFind = "ain";
            var spec = CountrySpecifications.CountryFullText(textToFind);

            //Act
            var result = countryRepository.AllMatching(spec);

            //Assert
            Assert.IsNotNull(result.All(c=>c.CountryISOCode.Contains(textToFind) || c.CountryName.Contains(textToFind)));

        }

        [TestMethod()]
        public void CountryRepositoryFilterMethodReturnEntitisWithSatisfiedFilter()
        {
             //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var countryRepository = new CountryRepository(unitOfWork);

            //Act
            var result =countryRepository.GetFiltered(c=>c.CountryName.Contains("EU"));

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.All(c=>c.CountryName.Contains("EU")));
        }

        [TestMethod()]
        public void CountryRepositoryPagedMethodReturnEntitiesInPageFashion()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var countryRepository = new CountryRepository(unitOfWork);

            //Act
            var pageI = countryRepository.GetPaged(0, 1, b => b.Id, false);
            var pageII = countryRepository.GetPaged(1, 1, b => b.Id, false);

            //Assert
            Assert.IsNotNull(pageI);
            Assert.IsTrue(pageI.Count() == 1);

            Assert.IsNotNull(pageII);
            Assert.IsTrue(pageII.Count() == 1);

            Assert.IsFalse(pageI.Intersect(pageII).Any());
        }
        [TestMethod()]
        public void CountryRepositoryRemoveItemDeleteIt()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var countryRepository = new CountryRepository(unitOfWork);

            var country = new Country("England", "en-EN");
            country.GenerateNewIdentity();
            
            countryRepository.Add(country);
            countryRepository.UnitOfWork.Commit();

            //Act
            countryRepository.Remove(country);
            unitOfWork.Commit();
        }
    }
}
