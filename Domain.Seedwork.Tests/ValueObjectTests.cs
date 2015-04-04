//===================================================================================
// Microsoft Developer & Platform Evangelism
//=================================================================================== 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// Copyright (c) Microsoft Corporation.  All Rights Reserved.
// This code is released under the terms of the MS-LPL license, 
// http://microsoftnlayerapp.codeplex.com/license
//===================================================================================


namespace Domain.Seedwork.Tests
{
    using Domain.Seedwork.Tests.Classes;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass()]
    public class ValueObjectTests
    {
        [TestMethod()]
        public void IdenticalDataEqualsIsTrueTest()
        {
            //Arrange
            Address address1 = new Address("streetLine1", "streetLine2", "city", "zipcode");
            Address address2 = new Address("streetLine1", "streetLine2", "city", "zipcode");

            //Act
            bool resultEquals = address1.Equals(address2);
            bool resultEqualsSimetric = address2.Equals(address1);
            bool resultEqualsOnThis = address1.Equals(address1);

            //Assert
            Assert.IsTrue(resultEquals);
            Assert.IsTrue(resultEqualsSimetric);
            Assert.IsTrue(resultEqualsOnThis);
        }

        [TestMethod()]
        public void IdenticalDataEqualOperatorIsTrueTest()
        {
            //Arraneg
            Address address1 = new Address("streetLine1", "streetLine2", "city", "zipcode");
            Address address2 = new Address("streetLine1", "streetLine2", "city", "zipcode");

            //Act
            bool resultEquals = (address1 == address2);
            bool resultEqualsSimetric = (address2 == address1);
            bool resultEqualsOnThis = (address1 == address1);

            //Assert
            Assert.IsTrue(resultEquals);
            Assert.IsTrue(resultEqualsSimetric);
            Assert.IsTrue(resultEqualsOnThis);
        }

        [TestMethod()]
        public void IdenticalDataIsNotEqualOperatorIsFalseTest()
        {
            //Arraneg
            Address address1 = new Address("streetLine1", "streetLine2", "city", "zipcode");
            Address address2 = new Address("streetLine1", "streetLine2", "city", "zipcode");

            //Act
            bool resultEquals = (address1 != address2);
            bool resultEqualsSimetric = (address2 != address1);
            bool resultEqualsOnThis = (address1 != address1);

            //Assert
            Assert.IsFalse(resultEquals);
            Assert.IsFalse(resultEqualsSimetric);
            Assert.IsFalse(resultEqualsOnThis);
        }

        [TestMethod()]
        public void DiferentDataEqualsIsFalseTest()
        {
            //Arrange
            Address address1 = new Address("streetLine1", "streetLine2", "city", "zipcode");
            Address address2 = new Address("streetLine2", "streetLine1", "city", "zipcode");

            //Act
            bool result = address1.Equals(address2);
            bool resultSimetric = address2.Equals(address1);

            //Assert
            Assert.IsFalse(result);
            Assert.IsFalse(resultSimetric);
        }

        [TestMethod()]
        public void DiferentDataIsNotEqualOperatorIsTrueTest()
        {
            //Arrange
            Address address1 = new Address("streetLine1", "streetLine2", "city", "zipcode");
            Address address2 = new Address("streetLine2", "streetLine1", "city", "zipcode");

            //Act
            bool result = (address1 != address2);
            bool resultSimetric = (address2 != address1);

            //Assert
            Assert.IsTrue(result);
            Assert.IsTrue(resultSimetric);
        }

        [TestMethod()]
        public void DiferentDataEqualOperatorIsFalseTest()
        {
            //Arrange
            Address address1 = new Address("streetLine1", "streetLine2", "city", "zipcode");
            Address address2 = new Address("streetLine2", "streetLine1", "city", "zipcode");

            //Act
            bool result = (address1 == address2);
            bool resultSimetric = (address2 == address1);

            //Assert
            Assert.IsFalse(result);
            Assert.IsFalse(resultSimetric);
        }

        [TestMethod()]
        public void SameDataInDiferentPropertiesIsEqualsFalseTest()
        {
            //Arrange
            Address address1 = new Address("streetLine1", "streetLine2",null, null);
            Address address2 = new Address("streetLine2", "streetLine1", null, null);

            //Act
            bool result = address1.Equals(address2);
            

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void SameDataInDiferentPropertiesEqualOperatorFalseTest()
        {
            //Arrange
            Address address1 = new Address("streetLine1", "streetLine2", null, null);
            Address address2 = new Address("streetLine2", "streetLine1", null, null);

            //Act
            bool result = (address1 == address2);


            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void DiferentDataInDiferentPropertiesProduceDiferentHashCodeTest()
        {
            //Arrange
            Address address1 = new Address("streetLine1", "streetLine2", null, null);
            Address address2 = new Address("streetLine2", "streetLine1", null, null);

            //Act
            int address1HashCode = address1.GetHashCode();
            int address2HashCode = address2.GetHashCode();


            //Assert
            Assert.AreNotEqual(address1HashCode, address2HashCode);
        }
        [TestMethod()]
        public void SameDataInDiferentPropertiesProduceDiferentHashCodeTest()
        {
            //Arrange
            Address address1 = new Address("streetLine1", null, null, "streetLine1");
            Address address2 = new Address(null, "streetLine1", "streetLine1", null);

            //Act
            int address1HashCode = address1.GetHashCode();
            int address2HashCode = address2.GetHashCode();


            //Assert
            Assert.AreNotEqual(address1HashCode, address2HashCode);
        }
        [TestMethod()]
        public void SameReferenceEqualsTrueTest()
        {
            //Arrange
            Address address1 = new Address("streetLine1", null, null, "streetLine1");
            Address address2 = address1;


            //Act
            if (!address1.Equals(address2))
                Assert.Fail();

            if (!(address1 == address2))
                Assert.Fail();

        }
        [TestMethod()]
        public void SameDataSameHashCodeTest()
        {
            //Arrange
            Address address1 = new Address("streetLine1", "streetLine2", null, null);
            Address address2 = new Address("streetLine1", "streetLine2", null, null);

            //Act
            int address1HashCode = address1.GetHashCode();
            int address2HashCode = address2.GetHashCode();


            //Assert
            Assert.AreEqual(address1HashCode, address2HashCode);
        }

        [TestMethod()]
        public void SelfReferenceNotProduceInfiniteLoop()
        {
            //Arrange
            SelfReference aReference = new SelfReference();
            SelfReference bReference = new SelfReference();

            //Act
            aReference.Value = bReference;
            bReference.Value = aReference;

            //Assert

            Assert.AreNotEqual(aReference, bReference);
        }
    }
}
