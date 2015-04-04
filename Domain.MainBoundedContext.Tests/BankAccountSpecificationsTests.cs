
namespace Domain.MainBoundedContext.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.BankingModule.Aggregates.BankAccountAgg;
    using Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification;


    [TestClass()]
    public class BankAccountSpecificationsTests
    {
        [TestMethod()]
        public void BankAccountSpecificationNullBankAccountNumberReturnTrueSpec()
        {
            //Arrange
            ISpecification<BankAccount> spec = null;
            
            //Act
            spec = BankAccountSpecifications.BankAccountIbanNumber(null);

            //assert
            Assert.IsInstanceOfType(spec, typeof(TrueSpecification<BankAccount>));
        }
        [TestMethod()]
        public void BankAccountSpecificationEmptyBankAccountNumberReturnTrueSpec()
        {
            //Arrange
            ISpecification<BankAccount> spec = null;

            //Act
            spec = BankAccountSpecifications.BankAccountIbanNumber(null);

            //assert
            Assert.IsInstanceOfType(spec, typeof(TrueSpecification<BankAccount>));
        }

        [TestMethod()]
        public void BankAccountSpecificationValiBankAccountNumberReturnAndSpec()
        {
            //Arrange
            ISpecification<BankAccount> spec = null;

            //Act
            spec = BankAccountSpecifications.BankAccountIbanNumber("AB001");

            //assert
            Assert.IsInstanceOfType(spec, typeof(AndSpecification <BankAccount>));
            
        }
    }
}
