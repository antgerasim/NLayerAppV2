
namespace Domain.MainBoundedContext.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CountryAgg;
    using Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification;


    [TestClass()]
    public class CountrySpecificationsTests
    {
        [TestMethod()]
        public void CountrySpecificationsNullTextReturnTrueSpecification()
        {
            //Arrange and Act
            var specification = CountrySpecifications.CountryFullText(null);

            //Assert
            Assert.IsNotNull(specification);
            Assert.IsInstanceOfType(specification, typeof(TrueSpecification<Country>));
        }
        [TestMethod()]
        public void CountrySpecificationsEmptyTextReturnTrueSpecification()
        {
            //Arrange and Act
            var specification = CountrySpecifications.CountryFullText(string.Empty);

            //Assert
            Assert.IsNotNull(specification);
            Assert.IsInstanceOfType(specification, typeof(TrueSpecification<Country>));
        }
        [TestMethod()]
        public void CountrySpecificationNotEmptyTextReturnCompisiteSpecification()
        {
            //Arrange and Act
            var specification = CountrySpecifications.CountryFullText("Spain");

            //Assert
            Assert.IsNotNull(specification);
            Assert.IsInstanceOfType(specification, typeof(AndSpecification<Country>));
        }
    }
}
