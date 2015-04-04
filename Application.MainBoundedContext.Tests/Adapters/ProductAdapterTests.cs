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
    using System.Collections.Generic;
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.DTO;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.ProductAgg;
    using Microsoft.Samples.NLayerApp.Domain.Seedwork;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Adapter;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule;
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.NetFramework.Adapter;

    [TestClass()]
    public class ProductAdapterTests
    {
        [TestMethod()]
        public void ProductToProductDTOAdapter()
        {
            //Arrange
            var product = new Software("the title", "The description","AB001");
            product.ChangeUnitPrice(10);
            product.IncrementStock(10);
            product.GenerateNewIdentity();

            //Act
            ITypeAdapter adapter = TypeAdapterFactory.CreateAdapter();
            var productDTO = adapter.Adapt<Product, ProductDTO>(product);

            //Assert
            Assert.AreEqual(product.Id, productDTO.Id);
            Assert.AreEqual(product.Title, productDTO.Title);
            Assert.AreEqual(product.Description, productDTO.Description);
            Assert.AreEqual(product.AmountInStock, productDTO.AmountInStock);
            Assert.AreEqual(product.UnitPrice, productDTO.UnitPrice);
        }

        [TestMethod()]
        public void EnumerableProductToListProductDTOAdapter()
        {
            //Arrange
            var software = new Software("the title", "The description","AB001");
            software.ChangeUnitPrice(10);
            software.IncrementStock(10);
            software.GenerateNewIdentity();

            var products = new List<Software>() { software };

            //Act
            ITypeAdapter adapter = TypeAdapterFactory.CreateAdapter();
            var productsDTO = adapter.Adapt<IEnumerable<Product>, List<ProductDTO>>(products);

            //Assert
            Assert.AreEqual(products[0].Id, productsDTO[0].Id);
            Assert.AreEqual(products[0].Title, productsDTO[0].Title);
            Assert.AreEqual(products[0].Description, productsDTO[0].Description);
            Assert.AreEqual(products[0].AmountInStock, productsDTO[0].AmountInStock);
            Assert.AreEqual(products[0].UnitPrice, productsDTO[0].UnitPrice);
        }

        [TestMethod()]
        public void SoftwareToSoftwareDTOAdapter()
        {
            //Arrange
            var software = new Software("the title", "The description","AB001");
            software.ChangeUnitPrice(10);
            software.IncrementStock(10);
            software.GenerateNewIdentity();
            //Act
            ITypeAdapter adapter = TypeAdapterFactory.CreateAdapter();
            var softwareDTO = adapter.Adapt<Software, SoftwareDTO>(software);

            //Assert
            Assert.AreEqual(software.Id, softwareDTO.Id);
            Assert.AreEqual(software.Title, softwareDTO.Title);
            Assert.AreEqual(software.Description, softwareDTO.Description);
            Assert.AreEqual(software.AmountInStock, softwareDTO.AmountInStock);
            Assert.AreEqual(software.UnitPrice, softwareDTO.UnitPrice);
            Assert.AreEqual(software.LicenseCode, softwareDTO.LicenseCode);
        }

        [TestMethod()]
        public void EnumerableSoftwareToListSoftwareDTOAdapter()
        {
            //Arrange
            var software = new Software("the title", "The description", "AB001");

            software.ChangeUnitPrice(10);
            software.IncrementStock(10);
            software.GenerateNewIdentity();

            var softwares = new List<Software>() { software };
            

            //Act
            ITypeAdapter adapter = TypeAdapterFactory.CreateAdapter();
            var softwaresDTO = adapter.Adapt<IEnumerable<Software>, List<SoftwareDTO>>(softwares);

            //Assert
            Assert.AreEqual(softwares[0].Id, softwaresDTO[0].Id);
            Assert.AreEqual(softwares[0].Title, softwaresDTO[0].Title);
            Assert.AreEqual(softwares[0].Description, softwaresDTO[0].Description);
            Assert.AreEqual(softwares[0].AmountInStock, softwaresDTO[0].AmountInStock);
            Assert.AreEqual(softwares[0].UnitPrice, softwaresDTO[0].UnitPrice);
            Assert.AreEqual(softwares[0].LicenseCode, softwaresDTO[0].LicenseCode);
        }

        [TestMethod()]
        public void BookToBookDTOAdapter()
        {
            //Arrange
            var book = new Book("the title", "The description", "Krasis Press", "ABD12");

            book.ChangeUnitPrice(10);
            book.IncrementStock(10);

            book.GenerateNewIdentity();

            //Act
            ITypeAdapter adapter = TypeAdapterFactory.CreateAdapter();
            var bookDTO = adapter.Adapt<Book, BookDTO>(book);

            //Assert
            Assert.AreEqual(book.Id, bookDTO.Id);
            Assert.AreEqual(book.Title, bookDTO.Title);
            Assert.AreEqual(book.Description, bookDTO.Description);
            Assert.AreEqual(book.AmountInStock, bookDTO.AmountInStock);
            Assert.AreEqual(book.UnitPrice, bookDTO.UnitPrice);
            Assert.AreEqual(book.ISBN, bookDTO.ISBN);
            Assert.AreEqual(book.Publisher, bookDTO.Publisher);
        }

        [TestMethod()]
        public void EnumerableBookToListBookDTOAdapter()
        {
            //Arrange
            var book = new Book("the title", "The description","Krasis Press","ABD12");

            book.ChangeUnitPrice(10);
            book.IncrementStock(10);
            book.GenerateNewIdentity();

            var books = new List<Book>() { book };

            //Act
            ITypeAdapter adapter = TypeAdapterFactory.CreateAdapter();
            var booksDTO = adapter.Adapt<IEnumerable<Book>, List<BookDTO>>(books);

            //Assert
            Assert.AreEqual(books[0].Id, booksDTO[0].Id);
            Assert.AreEqual(books[0].Title, booksDTO[0].Title);
            Assert.AreEqual(books[0].Description, booksDTO[0].Description);
            Assert.AreEqual(books[0].AmountInStock, booksDTO[0].AmountInStock);
            Assert.AreEqual(books[0].UnitPrice, booksDTO[0].UnitPrice);
            Assert.AreEqual(books[0].ISBN, booksDTO[0].ISBN);
            Assert.AreEqual(books[0].Publisher, booksDTO[0].Publisher);
        }
    }
}
