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

    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.ProductAgg;
    using Microsoft.Samples.NLayerApp.Domain.Seedwork;
    using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.ERPModule.Repositories;
    using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass()]
    public class ProductRepositoryTests
    {
        [TestMethod()]
        public void ProductRepositoryGetMethodReturnMaterializedEntityById()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            IProductRepository productRepository = new ProductRepository(unitOfWork);

            var productId = new Guid("44668EBF-7B54-4431-8D61-C1298DB50857");
            Product product = null;

            //Act
            product = productRepository.Get(productId);

            //Assert
            Assert.IsNotNull(product);
            Assert.IsTrue(product.Id == productId);
        }

        [TestMethod()]
        public void ProductRepositoryGetMethodReturnNullWhenIdIsEmpty()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var productRepository = new ProductRepository(unitOfWork);

            Product product = null;

            //Act
            product = productRepository.Get(Guid.Empty);

            //Assert
            Assert.IsNull(product);
        }

        [TestMethod()]
        public void ProductRepositoryAddNewItemSaveItem()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            IProductRepository productRepository = new ProductRepository(unitOfWork);

            var book = new Book("The book title", "Any book description", "Krasis Press", "ABC");

            book.ChangeUnitPrice(40);
            book.IncrementStock(1);
            book.GenerateNewIdentity();

            //Act
            productRepository.Add(book);
            unitOfWork.Commit();
          
        }

        [TestMethod()]
        public void ProductRepositoryGetAllReturnMaterializedAllItems()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            IProductRepository productRepository = new ProductRepository(unitOfWork);

            //Act
            var allItems = productRepository.GetAll();

            //Assert
            Assert.IsNotNull(allItems);
            Assert.IsTrue(allItems.Any());
        }

        [TestMethod()]
        public void ProductRepositoryAllMatchingMethodReturnEntitiesWithSatisfiedCriteria()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            IProductRepository productRepository = new ProductRepository(unitOfWork);

            var spec = ProductSpecifications.ProductFullText("book");

            //Act
            var result = productRepository.AllMatching(spec);

            //Assert
            Assert.IsNotNull(result.All(p => p.Title.Contains("book") || p.Description.Contains("book")));

        }

        [TestMethod()]
        public void ProductRepositoryFilterMethodReturnEntitisWithSatisfiedFilter()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            IProductRepository productRepository = new ProductRepository(unitOfWork);

            //Act
            var result = productRepository.GetFiltered(p => p.AmountInStock > 1);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.All(p=>p.AmountInStock > 1));
        }

        [TestMethod()]
        public void ProductRepositoryPagedMethodReturnEntitiesInPageFashion()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            IProductRepository productRepository = new ProductRepository(unitOfWork);

            //Act
            var pageI = productRepository.GetPaged(0, 1, b => b.Id, false);
            var pageII = productRepository.GetPaged(1, 1, b => b.Id, false);

            //Assert
            Assert.IsNotNull(pageI);
            Assert.IsTrue(pageI.Count() == 1);

            Assert.IsNotNull(pageII);
            Assert.IsTrue(pageII.Count() == 1);

            Assert.IsFalse(pageI.Intersect(pageII).Any());
        }
        [TestMethod()]
        public void OrderRepositoryRemoveItemDeleteIt()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            IProductRepository productRepository = new ProductRepository(unitOfWork);

            var book = new Book("The book title", "Any book description", "Krasis Press", "ABC");

            book.ChangeUnitPrice(40M);
            book.IncrementStock(1);
            book.GenerateNewIdentity();

            //Act
            productRepository.Add(book);
            unitOfWork.Commit();
            
        }
    }
}
