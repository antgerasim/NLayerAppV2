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

using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.DTO;
using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.ProductAgg;
using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Adapter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.MainBoundedContext.Tests.Adapters
{

   [TestClass()]
   public class ProductAdapterTests
   {

      [TestMethod()]
      public void ProductToProductDtoAdapter()
      {
         //Arrange
         var product = new Software("the title", "The description", "AB001");
         product.ChangeUnitPrice(10);
         product.IncrementStock(10);
         product.GenerateNewIdentity();

         //Act
         var adapter = TypeAdapterFactory.CreateAdapter();
         var productDto = adapter.Adapt<Product, ProductDto>(product);

         //Assert
         Assert.AreEqual(product.Id, productDto.Id);
         Assert.AreEqual(product.Title, productDto.Title);
         Assert.AreEqual(product.Description, productDto.Description);
         Assert.AreEqual(product.AmountInStock, productDto.AmountInStock);
         Assert.AreEqual(product.UnitPrice, productDto.UnitPrice);
      }

      [TestMethod()]
      public void EnumerableProductToListProductDtoAdapter()
      {
         //Arrange
         var software = new Software("the title", "The description", "AB001");
         software.ChangeUnitPrice(10);
         software.IncrementStock(10);
         software.GenerateNewIdentity();

         var products = new List<Software>()
         {
            software
         };

         //Act
         var adapter = TypeAdapterFactory.CreateAdapter();
         var productsDto = adapter.Adapt<IEnumerable<Product>, List<ProductDto>>(products);

         //Assert
         Assert.AreEqual(products[0].Id, productsDto[0].Id);
         Assert.AreEqual(products[0].Title, productsDto[0].Title);
         Assert.AreEqual(products[0].Description, productsDto[0].Description);
         Assert.AreEqual(products[0].AmountInStock, productsDto[0].AmountInStock);
         Assert.AreEqual(products[0].UnitPrice, productsDto[0].UnitPrice);
      }

      [TestMethod()]
      public void SoftwareToSoftwareDtoAdapter()
      {
         //Arrange
         var software = new Software("the title", "The description", "AB001");
         software.ChangeUnitPrice(10);
         software.IncrementStock(10);
         software.GenerateNewIdentity();
         //Act
         var adapter = TypeAdapterFactory.CreateAdapter();
         var softwareDto = adapter.Adapt<Software, SoftwareDto>(software);

         //Assert
         Assert.AreEqual(software.Id, softwareDto.Id);
         Assert.AreEqual(software.Title, softwareDto.Title);
         Assert.AreEqual(software.Description, softwareDto.Description);
         Assert.AreEqual(software.AmountInStock, softwareDto.AmountInStock);
         Assert.AreEqual(software.UnitPrice, softwareDto.UnitPrice);
         Assert.AreEqual(software.LicenseCode, softwareDto.LicenseCode);
      }

      [TestMethod()]
      public void EnumerableSoftwareToListSoftwareDtoAdapter()
      {
         //Arrange
         var software = new Software("the title", "The description", "AB001");

         software.ChangeUnitPrice(10);
         software.IncrementStock(10);
         software.GenerateNewIdentity();

         var softwares = new List<Software>()
         {
            software
         };

         //Act
         var adapter = TypeAdapterFactory.CreateAdapter();
         var softwaresDto = adapter.Adapt<IEnumerable<Software>, List<SoftwareDto>>(softwares);

         //Assert
         Assert.AreEqual(softwares[0].Id, softwaresDto[0].Id);
         Assert.AreEqual(softwares[0].Title, softwaresDto[0].Title);
         Assert.AreEqual(softwares[0].Description, softwaresDto[0].Description);
         Assert.AreEqual(softwares[0].AmountInStock, softwaresDto[0].AmountInStock);
         Assert.AreEqual(softwares[0].UnitPrice, softwaresDto[0].UnitPrice);
         Assert.AreEqual(softwares[0].LicenseCode, softwaresDto[0].LicenseCode);
      }

      [TestMethod()]
      public void BookToBookDtoAdapter()
      {
         //Arrange
         var book = new Book("the title", "The description", "Krasis Press", "ABD12");

         book.ChangeUnitPrice(10);
         book.IncrementStock(10);

         book.GenerateNewIdentity();

         //Act
         var adapter = TypeAdapterFactory.CreateAdapter();
         var bookDto = adapter.Adapt<Book, BookDto>(book);

         //Assert
         Assert.AreEqual(book.Id, bookDto.Id);
         Assert.AreEqual(book.Title, bookDto.Title);
         Assert.AreEqual(book.Description, bookDto.Description);
         Assert.AreEqual(book.AmountInStock, bookDto.AmountInStock);
         Assert.AreEqual(book.UnitPrice, bookDto.UnitPrice);
         Assert.AreEqual(book.Isbn, bookDto.Isbn);
         Assert.AreEqual(book.Publisher, bookDto.Publisher);
      }

      [TestMethod()]
      public void EnumerableBookToListBookDtoAdapter()
      {
         //Arrange
         var book = new Book("the title", "The description", "Krasis Press", "ABD12");

         book.ChangeUnitPrice(10);
         book.IncrementStock(10);
         book.GenerateNewIdentity();

         var books = new List<Book>()
         {
            book
         };

         //Act
         var adapter = TypeAdapterFactory.CreateAdapter();
         var booksDto = adapter.Adapt<IEnumerable<Book>, List<BookDto>>(books);

         //Assert
         Assert.AreEqual(books[0].Id, booksDto[0].Id);
         Assert.AreEqual(books[0].Title, booksDto[0].Title);
         Assert.AreEqual(books[0].Description, booksDto[0].Description);
         Assert.AreEqual(books[0].AmountInStock, booksDto[0].AmountInStock);
         Assert.AreEqual(books[0].UnitPrice, booksDto[0].UnitPrice);
         Assert.AreEqual(books[0].Isbn, booksDto[0].Isbn);
         Assert.AreEqual(books[0].Publisher, booksDto[0].Publisher);
      }

   }

}