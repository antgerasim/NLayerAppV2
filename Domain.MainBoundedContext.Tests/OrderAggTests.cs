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
			

namespace Domain.MainBoundedContext.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.OrderAgg;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;
    using Microsoft.Samples.NLayerApp.Domain.Seedwork;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CountryAgg;

    [TestClass()]
    public class OrderAggTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void OrderCannotSetTransientCustomer()
        {
            //Arrange 
            Customer customer = new Customer();

            Order order = new Order();

            //Act
            order.SetTheCustomerForThisOrder(customer);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void OrderCannotSetNullCustomer()
        {
            //Arrange 
            Customer customer = new Customer();

            Order order = new Order();

            //Act
            order.SetTheCustomerForThisOrder(customer);
        }

        [TestMethod()]
        public void OrderSetDeliveredSetDateAndState()
        {
            //Arrange 
            Order order = new Order();

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
            string shippingName= "shippingName";
            string shippingCity = "shippingCity";
            string shippingZipCode = "shippingZipCode";
            string shippingAddress = "shippingAddress";

            Customer customer = new Customer();
            customer.GenerateNewIdentity();

            Order order = OrderFactory.CreateOrder(customer, shippingName, shippingCity, shippingAddress, shippingZipCode);
            order.GenerateNewIdentity();

            var line = order.AddNewOrderLine(Guid.NewGuid(), 1, 1, 0);

            //Assert
            Assert.AreEqual(order.Id, line.OrderId);
        }

        [TestMethod()]
        public void OrderGetTotalOrderSumLines()
        {
            //Arrange
            string shippingName= "shippingName";
            string shippingCity = "shippingCity";
            string shippingZipCode = "shippingZipCode";
            string shippingAddress = "shippingAddress";

            Customer customer = new Customer();
            customer.GenerateNewIdentity();

            Order order = OrderFactory.CreateOrder(customer, shippingName, shippingCity, shippingAddress, shippingZipCode);

            order.AddNewOrderLine(Guid.NewGuid(),1,500, 10);
            order.AddNewOrderLine(Guid.NewGuid(),2,300, 10);

            decimal expected = ((1 * 500) * (1 - (10M / 100M))) + ((2 * 300) * (1 - (10M / 100M)));

            //Act
            
            decimal actual = order.GetOrderTotal();

            //Assert
            Assert.AreEqual(expected,actual);
        }
        [TestMethod()]
        public void OrderDiscountInOrderLineCanBeZero()
        {
            //Arrange
            string shippingName = "shippingName";
            string shippingCity = "shippingCity";
            string shippingZipCode = "shippingZipCode";
            string shippingAddress = "shippingAddress";

            Customer customer = new Customer();
            customer.GenerateNewIdentity();

            Order order = OrderFactory.CreateOrder(customer, shippingName, shippingCity, shippingAddress, shippingZipCode);

            order.AddNewOrderLine(Guid.NewGuid(), 1, 500, 0);
            order.AddNewOrderLine(Guid.NewGuid(), 2, 300, 0);

            decimal expected = ((1 * 500) * (1 - (0M / 100M))) + ((2 * 300) * (1 - (0M / 100M)));

            //Act
            decimal actual = order.GetOrderTotal();

            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void OrderDiscountLessThanZeroIsEqualToZeroDiscount()
        {
            //Arrange
            string shippingName = "shippingName";
            string shippingCity = "shippingCity";
            string shippingZipCode = "shippingZipCode";
            string shippingAddress = "shippingAddress";

            Customer customer = new Customer();
            customer.GenerateNewIdentity();

            Order order = OrderFactory.CreateOrder(customer, shippingName, shippingCity, shippingAddress, shippingZipCode);

            order.AddNewOrderLine(Guid.NewGuid(), 1, 500, -10);
            order.AddNewOrderLine(Guid.NewGuid(), 2, 300, -10);

            decimal expected = ((1 * 500) * (1 - (0M / 100M))) + ((2 * 300) * (1 - (0M / 100M)));

            //Act
            decimal actual = order.GetOrderTotal();

            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void OrderDiscountGreatherThan100IsEqualTo100Discount()
        {
            //Arrange
            string shippingName = "shippingName";
            string shippingCity = "shippingCity";
            string shippingZipCode = "shippingZipCode";
            string shippingAddress = "shippingAddress";

            Customer customer = new Customer();
            customer.GenerateNewIdentity();

            Order order = OrderFactory.CreateOrder(customer, shippingName, shippingCity, shippingAddress, shippingZipCode);

            order.AddNewOrderLine(Guid.NewGuid(), 1, 500, 101);
            order.AddNewOrderLine(Guid.NewGuid(), 2, 300, 101);

            decimal expected = ((1 * 500) * (1 - (100M / 100M))) + ((2 * 300) * (1 - (100M / 100M)));

            //Act
            decimal actual = order.GetOrderTotal();

            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void OrderFactoryCreateValidOrder()
        {
            //Arrange
            
            string shippingName = "shippingName";
            string shippingCity = "shippingCity";
            string shippingZipCode = "shippingZipCode";
            string shippingAddress = "shippingAddress";

            Customer customer = new Customer();
            customer.GenerateNewIdentity();

            //Act
            Order order = OrderFactory.CreateOrder(customer, shippingName, shippingCity, shippingAddress, shippingZipCode);
            var validationContext = new ValidationContext(order, null, null);
            var validationResult = order.Validate(validationContext);

            //Assert
            ShippingInfo shippingInfo = new ShippingInfo(shippingName, shippingAddress, shippingCity, shippingZipCode);

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
            string shippingName = "shippingName";
            string shippingCity = "shippingCity";
            string shippingZipCode = "shippingZipCode";
            string shippingAddress = "shippingAddress";

            Country country = new Country("Spain", "es-ES");
            country.GenerateNewIdentity();

            var customer = CustomerFactory.CreateCustomer("jhon", "el rojo", "+3422", "company", country, new Address("city", "zipCode", "address line1", "addres line2"));
            

            //Act
            Order order = OrderFactory.CreateOrder(customer, shippingName, shippingCity, shippingAddress, shippingZipCode);
            order.AddNewOrderLine(Guid.NewGuid(), 1, 240, 0); // this is less that 1000 ( default customer credit )
            
            //assert
            var result = order.IsCreditValidForOrder();

            //Assert
            Assert.IsTrue(result);


        }
        [TestMethod()]
        public void IsCreditValidForOrderReturnFalseIfTotalOrderIsGreaterThanCustomerCredit()
        {
            //Arrange
            string shippingName = "shippingName";
            string shippingCity = "shippingCity";
            string shippingZipCode = "shippingZipCode";
            string shippingAddress = "shippingAddress";

            var country = new Country("spain", "es-ES");
            country.GenerateNewIdentity();

            var customer = CustomerFactory.CreateCustomer("jhon", "el rojo", "+3422", "company", country, new Address("city", "zipCode", "address line1", "addres line2"));
            
            //Act
            Order order = OrderFactory.CreateOrder(customer, shippingName, shippingCity, shippingAddress, shippingZipCode);
            order.AddNewOrderLine(Guid.NewGuid(), 100, 240, 0); // this is greater that 1000 ( default customer credit )
           
            //assert
            var result = order.IsCreditValidForOrder();

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void OrderNumberIsComposedWithOrderDateAndSequenceOrderNumber()
        {
            //Arrange

            string shippingName = "shippingName";
            string shippingCity = "shippingCity";
            string shippingZipCode = "shippingZipCode";
            string shippingAddress = "shippingAddress";

            Customer customer = new Customer();
            customer.GenerateNewIdentity();

            Order order = OrderFactory.CreateOrder(customer, shippingName, shippingCity, shippingAddress, shippingZipCode);

            //Act
            string expected = string.Format("{0}/{1}-{2}", order.OrderDate.Year, order.OrderDate.Month, order.SequenceNumberOrder);
            string result = order.OrderNumber;

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
