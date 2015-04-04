

namespace Domain.MainBoundedContext.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.OrderAgg;
    using Microsoft.Samples.NLayerApp.Domain.Seedwork;
    using Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification;

    [TestClass()]
    public class OrderSpecificationsTests
    {
        [TestMethod()]
        public void OrderFromDateRangeNullDatesReturnTrueSpecification()
        {
            //Arrange
            DateTime? start = null;
            DateTime? end = null;

            //Act
            var spec = OrdersSpecifications.OrderFromDateRange(start, end);

            //Assert
            Assert.IsNotNull(spec);
            Assert.IsInstanceOfType(spec, typeof(TrueSpecification<Order>));
        }
        [TestMethod()]
        public void OrderFromDateRangeNullStartDateReturnDirectSpecification()
        {
            //Arrange
            DateTime? start = null;
            DateTime? end = DateTime.Now;

            //Act
            var spec = OrdersSpecifications.OrderFromDateRange(start, end);

            //Assert
            Assert.IsNotNull(spec);
            Assert.IsInstanceOfType(spec, typeof(AndSpecification<Order>));
        }
        [TestMethod()]
        public void OrderFromDateRangeNullEndDateReturnDirectSpecification()
        {
            //Arrange
            DateTime? start = DateTime.Now;
            DateTime? end = null;

            //Act
            var spec = OrdersSpecifications.OrderFromDateRange(start, end);

            //Assert
            Assert.IsNotNull(spec);
            Assert.IsInstanceOfType(spec, typeof(AndSpecification<Order>));
        }
        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void OrderByNumberThrowInvalidOperationExceptionWhenOrderNumberPatternIsIncorrect()
        {
            //Arrange

            string orderNumber = "222"; //THIS IS AN INVALID ORDER NUMBER

            //Act
            var spec = OrdersSpecifications.OrdersByNumber(orderNumber);
        }
        [TestMethod()]
        public void OrderByNumberReturnDirectSpecificationWhenPatternIsOk()
        {
            //Arrange

            string orderNumber = "2011/12-1212"; //THIS IS AN INVALID ORDER NUMBER

            //Act
            var spec = OrdersSpecifications.OrdersByNumber(orderNumber);

            //Assert
            Assert.IsInstanceOfType(spec, typeof(DirectSpecification<Order>));
        }
    }
}
