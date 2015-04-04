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
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule;
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.DTO;
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.OrderAgg;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.ProductAgg;
    using Microsoft.Samples.NLayerApp.Domain.Seedwork;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Adapter;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.NetFramework.Adapter;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass()]
    public class OrderAdapterTests
    {
        [TestMethod()]
        public void OrderToOrderDTOAdapter()
        {
            //Arrange

            Customer customer = new Customer();
            customer.GenerateNewIdentity();
            customer.FirstName = "Unai";
            customer.LastName = "Zorrilla";

            Product product = new Software("the product title", "the product description","license code");
            product.GenerateNewIdentity();
            

            Order order = new Order();
            order.GenerateNewIdentity();
            order.OrderDate = DateTime.Now;
            order.ShippingInformation = new ShippingInfo("shippingName", "shippingAddress", "shippingCity", "shippingZipCode");
            order.SetTheCustomerForThisOrder(customer);

            var orderLine = order.AddNewOrderLine(product.Id, 10, 10, 0.5M);
            orderLine.SetProduct(product);

            //Act
            ITypeAdapter adapter = TypeAdapterFactory.CreateAdapter();
            var orderDTO = adapter.Adapt<Order, OrderDTO>(order);

            //Assert
            Assert.AreEqual(orderDTO.Id, order.Id);
            Assert.AreEqual(orderDTO.OrderDate, order.OrderDate);
            Assert.AreEqual(orderDTO.DeliveryDate, order.DeliveryDate);

            Assert.AreEqual(orderDTO.ShippingAddress, order.ShippingInformation.ShippingAddress);
            Assert.AreEqual(orderDTO.ShippingCity, order.ShippingInformation.ShippingCity);
            Assert.AreEqual(orderDTO.ShippingName, order.ShippingInformation.ShippingName);
            Assert.AreEqual(orderDTO.ShippingZipCode, order.ShippingInformation.ShippingZipCode);

            Assert.AreEqual(orderDTO.CustomerFullName, order.Customer.FullName);
            Assert.AreEqual(orderDTO.CustomerId, order.Customer.Id);

            Assert.AreEqual(orderDTO.OrderNumber, string.Format("{0}/{1}-{2}",order.OrderDate.Year,order.OrderDate.Month,order.SequenceNumberOrder));


            Assert.IsNotNull(orderDTO.OrderLines);
            Assert.IsTrue(orderDTO.OrderLines.Any());

            Assert.AreEqual(orderDTO.OrderLines[0].Id, orderLine.Id);
            Assert.AreEqual(orderDTO.OrderLines[0].Amount, orderLine.Amount);
            Assert.AreEqual(orderDTO.OrderLines[0].Discount, orderLine.Discount * 100);
            Assert.AreEqual(orderDTO.OrderLines[0].UnitPrice, orderLine.UnitPrice);
            Assert.AreEqual(orderDTO.OrderLines[0].TotalLine, orderLine.TotalLine);
            Assert.AreEqual(orderDTO.OrderLines[0].ProductId, product.Id);
            Assert.AreEqual(orderDTO.OrderLines[0].ProductTitle, product.Title);

        }

        [TestMethod()]
        public void EnumerableOrderToOrderListDTOAdapter()
        {
            //Arrange

            Customer customer = new Customer();
            customer.GenerateNewIdentity();
            customer.FirstName = "Unai";
            customer.LastName = "Zorrilla";

            Product product = new Software("the product title", "the product description","license code");
            product.GenerateNewIdentity();


            Order order = new Order();
            order.GenerateNewIdentity();
            order.OrderDate = DateTime.Now;
            order.ShippingInformation = new ShippingInfo("shippingName", "shippingAddress", "shippingCity", "shippingZipCode");
            order.SetTheCustomerForThisOrder(customer);

            var line = order.AddNewOrderLine(product.Id, 1, 200, 0);
            

            var orders = new List<Order>() { order };

            //Act
            ITypeAdapter adapter = TypeAdapterFactory.CreateAdapter();
            var orderListDTO = adapter.Adapt<IEnumerable<Order>, List<OrderListDTO>>(orders);

            //Assert
            Assert.AreEqual(orderListDTO[0].Id, order.Id);
            Assert.AreEqual(orderListDTO[0].OrderDate, order.OrderDate);
            Assert.AreEqual(orderListDTO[0].DeliveryDate, order.DeliveryDate);
            Assert.AreEqual(orderListDTO[0].TotalOrder, order.GetOrderTotal());

            Assert.AreEqual(orderListDTO[0].ShippingAddress, order.ShippingInformation.ShippingAddress);
            Assert.AreEqual(orderListDTO[0].ShippingCity, order.ShippingInformation.ShippingCity);
            Assert.AreEqual(orderListDTO[0].ShippingName, order.ShippingInformation.ShippingName);
            Assert.AreEqual(orderListDTO[0].ShippingZipCode, order.ShippingInformation.ShippingZipCode);

            Assert.AreEqual(orderListDTO[0].CustomerFullName, order.Customer.FullName);
            Assert.AreEqual(orderListDTO[0].CustomerId, order.Customer.Id);
        }
    }
}
