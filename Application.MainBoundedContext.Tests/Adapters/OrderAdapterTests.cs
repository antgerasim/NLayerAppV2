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

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.DTO;
using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;
using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.OrderAgg;
using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.ProductAgg;
using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Adapter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.MainBoundedContext.Tests.Adapters
{

   [TestClass()]
   public class OrderAdapterTests
   {

      [TestMethod()]
      public void OrderToOrderDtoAdapter()
      {
         //Arrange

         var customer = new Customer();
         customer.GenerateNewIdentity();
         customer.FirstName = "Unai";
         customer.LastName = "Zorrilla";

         Product product = new Software("the product title", "the product description", "license code");
         product.GenerateNewIdentity();

         var order = new Order();
         order.GenerateNewIdentity();
         order.OrderDate = DateTime.Now;
         order.ShippingInformation = new ShippingInfo(
            "shippingName",
            "shippingAddress",
            "shippingCity",
            "shippingZipCode");
         order.SetTheCustomerForThisOrder(customer);

         var orderLine = order.AddNewOrderLine(product.Id, 10, 10, 0.5M);
         orderLine.SetProduct(product);

         //Act
         var adapter = TypeAdapterFactory.CreateAdapter();
         var orderDto = adapter.Adapt<Order, OrderDto>(order);

         //Assert
         Assert.AreEqual(orderDto.Id, order.Id);
         Assert.AreEqual(orderDto.OrderDate, order.OrderDate);
         Assert.AreEqual(orderDto.DeliveryDate, order.DeliveryDate);

         Assert.AreEqual(orderDto.ShippingAddress, order.ShippingInformation.ShippingAddress);
         Assert.AreEqual(orderDto.ShippingCity, order.ShippingInformation.ShippingCity);
         Assert.AreEqual(orderDto.ShippingName, order.ShippingInformation.ShippingName);
         Assert.AreEqual(orderDto.ShippingZipCode, order.ShippingInformation.ShippingZipCode);

         Assert.AreEqual(orderDto.CustomerFullName, order.Customer.FullName);
         Assert.AreEqual(orderDto.CustomerId, order.Customer.Id);

         Assert.AreEqual(
            orderDto.OrderNumber,
            string.Format("{0}/{1}-{2}", order.OrderDate.Year, order.OrderDate.Month, order.SequenceNumberOrder));

         Assert.IsNotNull(orderDto.OrderLines);
         Assert.IsTrue(orderDto.OrderLines.Any());

         Assert.AreEqual(orderDto.OrderLines[0].Id, orderLine.Id);
         Assert.AreEqual(orderDto.OrderLines[0].Amount, orderLine.Amount);
         Assert.AreEqual(orderDto.OrderLines[0].Discount, orderLine.Discount * 100);
         Assert.AreEqual(orderDto.OrderLines[0].UnitPrice, orderLine.UnitPrice);
         Assert.AreEqual(orderDto.OrderLines[0].TotalLine, orderLine.TotalLine);
         Assert.AreEqual(orderDto.OrderLines[0].ProductId, product.Id);
         Assert.AreEqual(orderDto.OrderLines[0].ProductTitle, product.Title);

      }

      [TestMethod()]
      public void EnumerableOrderToOrderListDtoAdapter()
      {
         //Arrange

         var customer = new Customer();
         customer.GenerateNewIdentity();
         customer.FirstName = "Unai";
         customer.LastName = "Zorrilla";

         Product product = new Software("the product title", "the product description", "license code");
         product.GenerateNewIdentity();

         var order = new Order();
         order.GenerateNewIdentity();
         order.OrderDate = DateTime.Now;
         order.ShippingInformation = new ShippingInfo(
            "shippingName",
            "shippingAddress",
            "shippingCity",
            "shippingZipCode");
         order.SetTheCustomerForThisOrder(customer);

         var line = order.AddNewOrderLine(product.Id, 1, 200, 0);

         var orders = new List<Order>()
         {
            order
         };

         //Act
         var adapter = TypeAdapterFactory.CreateAdapter();
         var orderListDto = adapter.Adapt<IEnumerable<Order>, List<OrderListDto>>(orders);

         //Assert
         Assert.AreEqual(orderListDto[0].Id, order.Id);
         Assert.AreEqual(orderListDto[0].OrderDate, order.OrderDate);
         Assert.AreEqual(orderListDto[0].DeliveryDate, order.DeliveryDate);
         Assert.AreEqual(orderListDto[0].TotalOrder, order.GetOrderTotal());

         Assert.AreEqual(orderListDto[0].ShippingAddress, order.ShippingInformation.ShippingAddress);
         Assert.AreEqual(orderListDto[0].ShippingCity, order.ShippingInformation.ShippingCity);
         Assert.AreEqual(orderListDto[0].ShippingName, order.ShippingInformation.ShippingName);
         Assert.AreEqual(orderListDto[0].ShippingZipCode, order.ShippingInformation.ShippingZipCode);

         Assert.AreEqual(orderListDto[0].CustomerFullName, order.Customer.FullName);
         Assert.AreEqual(orderListDto[0].CustomerId, order.Customer.Id);
      }

   }

}