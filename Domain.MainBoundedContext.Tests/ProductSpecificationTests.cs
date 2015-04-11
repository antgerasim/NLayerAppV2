using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.ProductAgg;
using Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.MainBoundedContext.Tests
{

   [TestClass()]
   public class ProductSpecificationTests
   {

      [TestMethod()]
      public void ProductFullTextSpecificationEmptyDataReturnTrueSpecification()
      {
         //Arrange
         var productData = string.Empty;

         //Act
         var specification = ProductSpecifications.ProductFullText(productData);

         //Assert
         Assert.IsNotNull(specification);
         Assert.IsInstanceOfType(specification, typeof (TrueSpecification<Product>));
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
         Assert.IsInstanceOfType(specification, typeof (TrueSpecification<Product>));
      }

      [TestMethod()]
      public void ProductFullTextSpecificationNonEmptyDataReturnAndSpecification()
      {
         //Arrange
         var productData = "the product title or product description data";

         //Act
         var specification = ProductSpecifications.ProductFullText(productData);

         //Assert
         Assert.IsNotNull(specification);
         Assert.IsInstanceOfType(specification, typeof (AndSpecification<Product>));
      }

   }

}