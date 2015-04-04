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

    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.OrderAgg;
    using Microsoft.Samples.NLayerApp.Domain.Seedwork;
    using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.ERPModule.Repositories;
    using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;
    using System.Data.Entity.Validation;
    using System.Data;

    [TestClass()]
    public class OrderRepositoryTests
    {
        [TestMethod()]
        public void OrderRepositoryGetMethodReturnMaterializedEntityById()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var orderRepository = new OrderRepository(unitOfWork);

            var orderId = new Guid("3135513C-63FD-43E6-9697-6C6E5D8CE55B");

            //Act
            var order = orderRepository.Get(orderId);

            //Assert
            Assert.IsNotNull(order);
            Assert.IsTrue(order.Id == orderId);

        }

        [TestMethod()]
        public void OrderRepositoryGetMethodReturnNullWhenIdIsEmpty()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var orderRepository = new OrderRepository(unitOfWork);

            //Act
            var order = orderRepository.Get(Guid.Empty);

            //Assert
            Assert.IsNull(order);
        }

        [TestMethod()]
        public void OrderRepositoryAddNewItemSaveItem()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();

            var customerRepository = new CustomerRepository(unitOfWork);
            var orderRepository = new OrderRepository(unitOfWork);

            var customer = customerRepository.Get(new Guid("0CD6618A-9C8E-4D79-9C6B-4AA69CF18AE6"));

            var order = OrderFactory.CreateOrder(customer, "shipping name", "shipping city", "shipping address", "shipping zip code");
            order.GenerateNewIdentity();

            //Act
            orderRepository.Add(order);
            unitOfWork.Commit();
        }

        [TestMethod()]
        public void OrderRepositoryGetAllReturnMaterializedAllItems()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var orderRepository = new OrderRepository(unitOfWork);

            //Act
            var allItems = orderRepository.GetAll();

            //Assert

            Assert.IsNotNull(allItems);
            Assert.IsTrue(allItems.Any());

        }

        [TestMethod()]
        public void OrderRepositoryAllMatchingMethodReturnEntitiesWithSatisfiedCriteria()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var orderRepository = new OrderRepository(unitOfWork);

            var spec = OrdersSpecifications.OrderFromDateRange(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));

            //Act
            var result = orderRepository.AllMatching(spec);

            //Assert
            Assert.IsNotNull(result.All(o => o.OrderDate > DateTime.Now.AddDays(-2) && o.OrderDate < DateTime.Now.AddDays(-1)));

        }

        [TestMethod()]
        public void OrderRepositoryFilterMethodReturnEntitisWithSatisfiedFilter()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var orderRepository = new OrderRepository(unitOfWork);

            //Act
            var result = orderRepository.GetFiltered(o => o.IsDelivered == false);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.All(o => o.IsDelivered));
        }

        [TestMethod()]
        public void OrderRepositoryPagedMethodReturnEntitiesInPageFashion()
        {
            //Arrange
            var unitOfWork = new MainBCUnitOfWork();
            var orderRepository = new OrderRepository(unitOfWork);

            //Act
            var pageI = orderRepository.GetPaged(0, 1, b => b.Id, false);
            var pageII = orderRepository.GetPaged(1, 1, b => b.Id, false);

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

            var customerRepository = new CustomerRepository(unitOfWork);
            var orderRepository = new OrderRepository(unitOfWork);

            var customer = customerRepository.Get(new Guid("0CD6618A-9C8E-4D79-9C6B-4AA69CF18AE6"));

            var order = OrderFactory.CreateOrder(customer, "shipping name", "shipping city", "shipping address", "shipping zip code");
            order.GenerateNewIdentity();

            orderRepository.Add(order);
            orderRepository.UnitOfWork.Commit();

            //Act
            orderRepository.Remove(order);
            unitOfWork.Commit();
        }
    }
}
