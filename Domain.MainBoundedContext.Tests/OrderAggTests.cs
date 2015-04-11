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
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CountryAgg;
using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;
using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.OrderAgg;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.MainBoundedContext.Tests
{

   [TestClass()]
   public class OrderAggTests
   {

      [TestMethod()]
      [ExpectedException(typeof (ArgumentException))]
      public void OrderCannotSetTransientCustomer()
      {
         //Arrange 
         var customer = new Customer();

         var order = new Order();

         //Act
         order.SetTheCustomerForThisOrder(customer);
      }

      [TestMethod()]
      [ExpectedException(typeof (ArgumentException))]
      public void OrderCannotSetNullCustomer()
      {
         //Arrange 
         var customer = new Customer();

         var order = new Order();

         //Act
         order.SetTheCustomerForThisOrder(customer);
      }

      [TestMethod()]
      public void OrderSetDeliveredSetDateAndState()
      {
         //Arrange 
         var order = new Order();

         //Act
         order.SetOrderAsDelivered();

         //Assert
         Assert.IsTrue(order.IsDelivered);
         Assert.IsNotNull(order.DeliveryDate);
         Assert.IsTrue(order.DeliveryDate != default(DateTime));
      }

      [TestMethod()]
      public void OrderAddNewOrderLineFixOrderId()
      {
         //Arrange
         var shippingName = "shippingName";
         var shippingCity = "shippingCity";
         var shippingZipCode = "shippingZipCode";
         var shippingAddress = "shippingAddress";

         var customer = new Customer();
         customer.GenerateNewIdentity();

         var order = OrderFactory.CreateOrder(customer, shippingName, shippingCity, shippingAddress, shippingZipCode);
         order.GenerateNewIdentity();

         var line = order.AddNewOrderLine(Guid.NewGuid(), 1, 1, 0);

         //Assert
         Assert.AreEqual(order.Id, line.OrderId);
      }

      [TestMethod()]
      public void OrderGetTotalOrderSumLines()
      {
         //Arrange
         var shippingName = "shippingName";
         var shippingCity = "shippingCity";
         var shippingZipCode = "shippingZipCode";
         var shippingAddress = "shippingAddress";

         var customer = new Customer();
         customer.GenerateNewIdentity();

         var order = OrderFactory.CreateOrder(customer, shippingName, shippingCity, shippingAddress, shippingZipCode);

         order.AddNewOrderLine(Guid.NewGuid(), 1, 500, 10);
         order.AddNewOrderLine(Guid.NewGuid(), 2, 300, 10);

         var expected = ((1 * 500) * (1 - (10M / 100M))) + ((2 * 300) * (1 - (10M / 100M)));

         //Act

         var actual = order.GetOrderTotal();

         //Assert
         Assert.AreEqual(expected, actual);
      }

      [TestMethod()]
      public void OrderDiscountInOrderLineCanBeZero()
      {
         //Arrange
         var shippingName = "shippingName";
         var shippingCity = "shippingCity";
         var shippingZipCode = "shippingZipCode";
         var shippingAddress = "shippingAddress";

         var customer = new Customer();
         customer.GenerateNewIdentity();

         var order = OrderFactory.CreateOrder(customer, shippingName, shippingCity, shippingAddress, shippingZipCode);

         order.AddNewOrderLine(Guid.NewGuid(), 1, 500, 0);
         order.AddNewOrderLine(Guid.NewGuid(), 2, 300, 0);

         var expected = ((1 * 500) * (1 - (0M / 100M))) + ((2 * 300) * (1 - (0M / 100M)));

         //Act
         var actual = order.GetOrderTotal();

         //Assert
         Assert.AreEqual(expected, actual);
      }

      [TestMethod()]
      public void OrderDiscountLessThanZeroIsEqualToZeroDiscount()
      {
         //Arrange
         var shippingName = "shippingName";
         var shippingCity = "shippingCity";
         var shippingZipCode = "shippingZipCode";
         var shippingAddress = "shippingAddress";

         var customer = new Customer();
         customer.GenerateNewIdentity();

         var order = OrderFactory.CreateOrder(customer, shippingName, shippingCity, shippingAddress, shippingZipCode);

         order.AddNewOrderLine(Guid.NewGuid(), 1, 500, -10);
         order.AddNewOrderLine(Guid.NewGuid(), 2, 300, -10);

         var expected = ((1 * 500) * (1 - (0M / 100M))) + ((2 * 300) * (1 - (0M / 100M)));

         //Act
         var actual = order.GetOrderTotal();

         //Assert
         Assert.AreEqual(expected, actual);
      }

      [TestMethod()]
      public void OrderDiscountGreatherThan100IsEqualTo100Discount()
      {
         //Arrange
         var shippingName = "shippingName";
         var shippingCity = "shippingCity";
         var shippingZipCode = "shippingZipCode";
         var shippingAddress = "shippingAddress";

         var customer = new Customer();
         customer.GenerateNewIdentity();

         var order = OrderFactory.CreateOrder(customer, shippingName, shippingCity, shippingAddress, shippingZipCode);

         order.AddNewOrderLine(Guid.NewGuid(), 1, 500, 101);
         order.AddNewOrderLine(Guid.NewGuid(), 2, 300, 101);

         var expected = ((1 * 500) * (1 - (100M / 100M))) + ((2 * 300) * (1 - (100M / 100M)));

         //Act
         var actual = order.GetOrderTotal();

         //Assert
         Assert.AreEqual(expected, actual);
      }

      [TestMethod()]
      public void OrderFactoryCreateValidOrder()
      {
         //Arrange

         var shippingName = "shippingName";
         var shippingCity = "shippingCity";
         var shippingZipCode = "shippingZipCode";
         var shippingAddress = "shippingAddress";

         var customer = new Customer();
         customer.GenerateNewIdentity();

         //Act
         var order = OrderFactory.CreateOrder(customer, shippingName, shippingCity, shippingAddress, shippingZipCode);
         var validationContext = new ValidationContext(order, null, null);
         var validationResult = order.Validate(validationContext);

         //Assert
         var shippingInfo = new ShippingInfo(shippingName, shippingAddress, shippingCity, shippingZipCode);

         Assert.AreEqual(shippingInfo, order.ShippingInformation);
         Assert.AreEqual(order.Customer, customer);
         Assert.AreEqual(order.CustomerId, customer.Id);
         Assert.IsFalse(order.IsDelivered);
         Assert.IsNull(order.DeliveryDate);
         Assert.IsTrue(order.OrderDate != default(DateTime));
         Assert.IsFalse(validationResult.Any());
      }

      [TestMethod()]
      public void IsCreditValidForOrderReturnTrueIfTotalOrderIsLessThanCustomerCredit()
      {
         //Arrange
         var shippingName = "shippingName";
         var shippingCity = "shippingCity";
         var shippingZipCode = "shippingZipCode";
         var shippingAddress = "shippingAddress";

         var country = new Country("Spain", "es-ES");
         country.GenerateNewIdentity();

         var customer = CustomerFactory.CreateCustomer(
            "jhon",
            "el rojo",
            "+3422",
            "company",
            country,
            new Address("city", "zipCode", "address line1", "addres line2"));

         //Act
         var order = OrderFactory.CreateOrder(customer, shippingName, shippingCity, shippingAddress, shippingZipCode);
         order.AddNewOrderLine(Guid.NewGuid(), 1, 240, 0);
         // this is less that 1000 ( default customer credit )

         //assert
         var result = order.IsCreditValidForOrder();

         //Assert
         Assert.IsTrue(result);

      }

      [TestMethod()]
      public void IsCreditValidForOrderReturnFalseIfTotalOrderIsGreaterThanCustomerCredit()
      {
         //Arrange
         var shippingName = "shippingName";
         var shippingCity = "shippingCity";
         var shippingZipCode = "shippingZipCode";
         var shippingAddress = "shippingAddress";

         var country = new Country("spain", "es-ES");
         country.GenerateNewIdentity();

         var customer = CustomerFactory.CreateCustomer(
            "jhon",
            "el rojo",
            "+3422",
            "company",
            country,
            new Address("city", "zipCode", "address line1", "addres line2"));

         //Act
         var order = OrderFactory.CreateOrder(customer, shippingName, shippingCity, shippingAddress, shippingZipCode);
         order.AddNewOrderLine(Guid.NewGuid(), 100, 240, 0);
         // this is greater that 1000 ( default customer credit )

         //assert
         var result = order.IsCreditValidForOrder();

         //Assert
         Assert.IsFalse(result);
      }

      [TestMethod()]
      public void OrderNumberIsComposedWithOrderDateAndSequenceOrderNumber()
      {
         //Arrange

         var shippingName = "shippingName";
         var shippingCity = "shippingCity";
         var shippingZipCode = "shippingZipCode";
         var shippingAddress = "shippingAddress";

         var customer = new Customer();
         customer.GenerateNewIdentity();

         var order = OrderFactory.CreateOrder(customer, shippingName, shippingCity, shippingAddress, shippingZipCode);

         //Act
         var expected = string.Format(
            "{0}/{1}-{2}",
            order.OrderDate.Year,
            order.OrderDate.Month,
            order.SequenceNumberOrder);
         var result = order.OrderNumber;

         //Assert
         Assert.AreEqual(expected, result);
      }

   }

}