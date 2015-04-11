using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CountryAgg;
using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.MainBoundedContext.Tests
{

   [TestClass()]
   public class CustomerAggTests
   {

      [TestMethod()]
      [ExpectedException(typeof (ArgumentException))]
      public void CustomerCannotAssociateTransientCountry()
      {
         //Arrange
         var country = new Country("Spain", "es-ES");

         //Act
         var customer = new Customer();
         customer.SetTheCountryForThisCustomer(country);
      }

      [TestMethod()]
      [ExpectedException(typeof (ArgumentException))]
      public void CustomerCannotAssociateNullCountry()
      {
         //Arrange
         var country = new Country("Spain", "es-ES");

         //Act
         var customer = new Customer();
         customer.SetTheCountryForThisCustomer(null);
      }

      [TestMethod()]
      public void CustomerSetCountryFixCountryId()
      {
         //Arrange
         var country = new Country("Spain", "es-ES");
         country.GenerateNewIdentity();

         //Act
         var customer = new Customer();
         customer.SetTheCountryForThisCustomer(country);

         //Assert
         Assert.AreEqual(country.Id, customer.CountryId);
      }

      [TestMethod()]
      public void CustomerDisableSetIsEnabledToFalse()
      {
         //Arrange 
         var customer = new Customer();

         //Act
         customer.Disable();

         //assert
         Assert.IsFalse(customer.IsEnabled);
      }

      [TestMethod()]
      public void CustomerEnableSetIsEnabledToTrue()
      {
         //Arrange 
         var customer = new Customer();

         //Act
         customer.Enable();

         //assert
         Assert.IsTrue(customer.IsEnabled);
      }

      [TestMethod()]
      public void CustomerFactoryWithCountryEntityCreateValidCustomer()
      {
         //Arrange
         var lastName = "El rojo";
         var firstName = "Jhon";
         var telephone = "+34111111";
         var company = "company name";

         var country = new Country("Spain", "es-ES");
         country.GenerateNewIdentity();

         //Act
         var customer = CustomerFactory.CreateCustomer(
            firstName,
            lastName,
            telephone,
            company,
            country,
            new Address("city", "zipcode", "AddressLine1", "AddressLine2"));
         var validationContext = new ValidationContext(customer, null, null);
         var validationResults = customer.Validate(validationContext);

         //Assert
         Assert.AreEqual(customer.LastName, lastName);
         Assert.AreEqual(customer.FirstName, firstName);
         Assert.AreEqual(customer.Country, country);
         Assert.AreEqual(customer.CountryId, country.Id);
         Assert.AreEqual(customer.IsEnabled, true);
         Assert.AreEqual(customer.Company, company);
         Assert.AreEqual(customer.Telephone, telephone);
         Assert.AreEqual(customer.CreditLimit, 1000M);

         Assert.IsFalse(validationResults.Any());
      }

      [TestMethod()]
      public void CustomerFactoryWithCountryIdEntityCreateValidCustomer()
      {
         //Arrange
         var lastName = "El rojo";
         var firstName = "Jhon";
         var telephone = "+34111111";
         var company = "company name";

         var country = new Country("Spain", "es-ES");
         country.GenerateNewIdentity();

         //Act
         var customer = CustomerFactory.CreateCustomer(
            firstName,
            lastName,
            telephone,
            company,
            country,
            new Address("city", "zipcode", "AddressLine1", "AddressLine2"));
         var validationContext = new ValidationContext(customer, null, null);
         var validationResults = customer.Validate(validationContext);

         //Assert
         Assert.AreEqual(customer.LastName, lastName);
         Assert.AreEqual(customer.FirstName, firstName);
         Assert.AreEqual(customer.CountryId, country.Id);
         Assert.AreEqual(customer.IsEnabled, true);
         Assert.AreEqual(customer.Company, company);
         Assert.AreEqual(customer.Telephone, telephone);
         Assert.AreEqual(customer.CreditLimit, 1000M);

         Assert.IsFalse(validationResults.Any());
      }

   }

}