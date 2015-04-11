using System;

using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.OrderAgg;
using Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.MainBoundedContext.Tests
{

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
         Assert.IsInstanceOfType(spec, typeof (TrueSpecification<Order>));
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
         Assert.IsInstanceOfType(spec, typeof (AndSpecification<Order>));
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
         Assert.IsInstanceOfType(spec, typeof (AndSpecification<Order>));
      }

      [TestMethod()]
      [ExpectedException(typeof (InvalidOperationException))]
      public void OrderByNumberThrowInvalidOperationExceptionWhenOrderNumberPatternIsIncorrect()
      {
         //Arrange

         var orderNumber = "222"; //THIS IS AN INVALID ORDER NUMBER

         //Act
         var spec = OrdersSpecifications.OrdersByNumber(orderNumber);
      }

      [TestMethod()]
      public void OrderByNumberReturnDirectSpecificationWhenPatternIsOk()
      {
         //Arrange

         var orderNumber = "2011/12-1212"; //THIS IS AN INVALID ORDER NUMBER

         //Act
         var spec = OrdersSpecifications.OrdersByNumber(orderNumber);

         //Assert
         Assert.IsInstanceOfType(spec, typeof (DirectSpecification<Order>));
      }

   }

}