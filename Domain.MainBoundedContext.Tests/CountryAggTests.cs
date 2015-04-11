using System;

using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CountryAgg;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.MainBoundedContext.Tests
{

   [TestClass()]
   public class CountryAggTests
   {

      [TestMethod()]
      [ExpectedException(typeof (ArgumentNullException))]
      public void CannotCreateCountryWithNullName()
      {
         //Arrange and Act
         var country = new Country(null, "es-ES");
      }

      [TestMethod()]
      [ExpectedException(typeof (ArgumentNullException))]
      public void CannotCreateCountryWithNullIsoCode()
      {
         //Arrange and Act
         var country = new Country("spain", null);
      }

      [TestMethod()]
      [ExpectedException(typeof (ArgumentNullException))]
      public void CannotCreateCountryWithEmptyName()
      {
         //Arrange and Act
         var country = new Country(string.Empty, "es-ES");
      }

      [TestMethod()]
      [ExpectedException(typeof (ArgumentNullException))]
      public void CannotCreateCountryWithEmptyIsoCode()
      {
         //Arrange and Act
         var country = new Country("spain", string.Empty);
      }

      [TestMethod()]
      [ExpectedException(typeof (ArgumentNullException))]
      public void CannotCreateCountryWithWhitespaceName()
      {
         //Arrange and Act
         var country = new Country(" ", "es-ES");
      }

      [TestMethod()]
      [ExpectedException(typeof (ArgumentNullException))]
      public void CannotCreateCountryWithWhitespaceIsoCode()
      {
         //Arrange and Act
         var country = new Country("spain", " ");
      }

   }

}