
namespace Domain.MainBoundedContext.Tests
{
    using Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.ProductAgg;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass()]
    public class ProductSpecificationTests
    {
        [TestMethod()]
        public void ProductFullTextSpecificationEmptyDataReturnTrueSpecification()
        {
            //Arrange
            string productData = string.Empty;

            //Act
            var specification = ProductSpecifications.ProductFullText(productData);

            //Assert
            Assert.IsNotNull(specification);
            Assert.IsInstanceOfType(specification, typeof(TrueSpecification<Product>));
        }
        [TestMethod()]
        public void ProductFullTextSpecificationNullDataReturnTrueSpecification()
        {
            //Arrange
            string productData = null;

            //Act
            var specification = ProductSpecifications.ProductFullText(productData);

            //Assert
            Assert.IsNotNull(specification);
            Assert.IsInstanceOfType(specification, typeof(TrueSpecification<Product>));
        }
        [TestMethod()]
        public void ProductFullTextSpecificationNonEmptyDataReturnAndSpecification()
        {
            //Arrange
            string productData = "the product title or product description data";

            //Act
            var specification = ProductSpecifications.ProductFullText(productData);

            //Assert
            Assert.IsNotNull(specification);
            Assert.IsInstanceOfType(specification, typeof(AndSpecification<Product>));
        }
    }
}
