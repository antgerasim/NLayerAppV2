using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;
using Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.MainBoundedContext.Tests
{

   [TestClass()]
   public class CustomerSpecificationsTests
   {

      [TestMethod()]
      public void CustomerFullTextEmptyTextReturnDirectSpecification()
      {
         //Arrange 
         ISpecification<Customer> spec = null;

         //Act
         spec = CustomerSpecifications.CustomerFullText(string.Empty);

         //Assert
         Assert.IsNotNull(spec);
         Assert.IsInstanceOfType(spec, typeof (DirectSpecification<Customer>));
      }

      [TestMethod()]
      public void CustomerFullTextNullTextReturnDirectSpecification()
      {
         //Arrange 
         ISpecification<Customer> spec = null;

         //Act
         spec = CustomerSpecifications.CustomerFullText(null);

         //Assert
         Assert.IsNotNull(spec);
         Assert.IsInstanceOfType(spec, typeof (DirectSpecification<Customer>));
      }

      [TestMethod()]
      public void CustomerFullTextNonEmptyTextReturnAndSpecification()
      {
         //Arrange 
         ISpecification<Customer> spec = null;

         //Act
         spec = CustomerSpecifications.CustomerFullText("Unai");

         //Assert
         Assert.IsNotNull(spec);
         Assert.IsInstanceOfType(spec, typeof (AndSpecification<Customer>));
      }

      [TestMethod()]
      public void CustomerEnabledCustomersSpecificationReturnDirectSpecification()
      {
         //Arrange 
         ISpecification<Customer> spec = null;

         //Act
         spec = CustomerSpecifications.EnabledCustomers();

         //Assert
         Assert.IsNotNull(spec);
         Assert.IsInstanceOfType(spec, typeof (DirectSpecification<Customer>));
      }

   }

}