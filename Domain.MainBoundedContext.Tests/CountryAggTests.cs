

namespace Domain.MainBoundedContext.Tests
{

    using System;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CountryAgg;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass()]
    public class CountryAggTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotCreateCountryWithNullName()
        {
            //Arrange and Act
            Country country = new Country(null, "es-ES");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotCreateCountryWithNullISOCode()
        {
            //Arrange and Act
            Country country = new Country("spain", null);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotCreateCountryWithEmptyName()
        {
            //Arrange and Act
            Country country = new Country(string.Empty, "es-ES");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotCreateCountryWithEmptyISOCode()
        {
            //Arrange and Act
            Country country = new Country("spain", string.Empty);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotCreateCountryWithWhitespaceName()
        {
            //Arrange and Act
            Country country = new Country(" ", "es-ES");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotCreateCountryWithWhitespaceISOCode()
        {
            //Arrange and Act
            Country country = new Country("spain", " ");
        }
    }
}
