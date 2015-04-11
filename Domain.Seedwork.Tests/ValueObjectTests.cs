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

using Domain.Seedwork.Tests.Classes;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Seedwork.Tests
{

   [TestClass()]
   public class ValueObjectTests
   {

      [TestMethod()]
      public void IdenticalDataEqualsIsTrueTest()
      {
         //Arrange
         var address1 = new Address("streetLine1", "streetLine2", "city", "zipcode");
         var address2 = new Address("streetLine1", "streetLine2", "city", "zipcode");

         //Act
         var resultEquals = address1.Equals(address2);
         var resultEqualsSimetric = address2.Equals(address1);
         var resultEqualsOnThis = address1.Equals(address1);

         //Assert
         Assert.IsTrue(resultEquals);
         Assert.IsTrue(resultEqualsSimetric);
         Assert.IsTrue(resultEqualsOnThis);
      }

      [TestMethod()]
      public void IdenticalDataEqualOperatorIsTrueTest()
      {
         //Arraneg
         var address1 = new Address("streetLine1", "streetLine2", "city", "zipcode");
         var address2 = new Address("streetLine1", "streetLine2", "city", "zipcode");

         //Act
         var resultEquals = (address1 == address2);
         var resultEqualsSimetric = (address2 == address1);
         var resultEqualsOnThis = (address1 == address1);

         //Assert
         Assert.IsTrue(resultEquals);
         Assert.IsTrue(resultEqualsSimetric);
         Assert.IsTrue(resultEqualsOnThis);
      }

      [TestMethod()]
      public void IdenticalDataIsNotEqualOperatorIsFalseTest()
      {
         //Arraneg
         var address1 = new Address("streetLine1", "streetLine2", "city", "zipcode");
         var address2 = new Address("streetLine1", "streetLine2", "city", "zipcode");

         //Act
         var resultEquals = (address1 != address2);
         var resultEqualsSimetric = (address2 != address1);
         var resultEqualsOnThis = (address1 != address1);

         //Assert
         Assert.IsFalse(resultEquals);
         Assert.IsFalse(resultEqualsSimetric);
         Assert.IsFalse(resultEqualsOnThis);
      }

      [TestMethod()]
      public void DiferentDataEqualsIsFalseTest()
      {
         //Arrange
         var address1 = new Address("streetLine1", "streetLine2", "city", "zipcode");
         var address2 = new Address("streetLine2", "streetLine1", "city", "zipcode");

         //Act
         var result = address1.Equals(address2);
         var resultSimetric = address2.Equals(address1);

         //Assert
         Assert.IsFalse(result);
         Assert.IsFalse(resultSimetric);
      }

      [TestMethod()]
      public void DiferentDataIsNotEqualOperatorIsTrueTest()
      {
         //Arrange
         var address1 = new Address("streetLine1", "streetLine2", "city", "zipcode");
         var address2 = new Address("streetLine2", "streetLine1", "city", "zipcode");

         //Act
         var result = (address1 != address2);
         var resultSimetric = (address2 != address1);

         //Assert
         Assert.IsTrue(result);
         Assert.IsTrue(resultSimetric);
      }

      [TestMethod()]
      public void DiferentDataEqualOperatorIsFalseTest()
      {
         //Arrange
         var address1 = new Address("streetLine1", "streetLine2", "city", "zipcode");
         var address2 = new Address("streetLine2", "streetLine1", "city", "zipcode");

         //Act
         var result = (address1 == address2);
         var resultSimetric = (address2 == address1);

         //Assert
         Assert.IsFalse(result);
         Assert.IsFalse(resultSimetric);
      }

      [TestMethod()]
      public void SameDataInDiferentPropertiesIsEqualsFalseTest()
      {
         //Arrange
         var address1 = new Address("streetLine1", "streetLine2", null, null);
         var address2 = new Address("streetLine2", "streetLine1", null, null);

         //Act
         var result = address1.Equals(address2);

         //Assert
         Assert.IsFalse(result);
      }

      [TestMethod()]
      public void SameDataInDiferentPropertiesEqualOperatorFalseTest()
      {
         //Arrange
         var address1 = new Address("streetLine1", "streetLine2", null, null);
         var address2 = new Address("streetLine2", "streetLine1", null, null);

         //Act
         var result = (address1 == address2);

         //Assert
         Assert.IsFalse(result);
      }

      [TestMethod()]
      public void DiferentDataInDiferentPropertiesProduceDiferentHashCodeTest()
      {
         //Arrange
         var address1 = new Address("streetLine1", "streetLine2", null, null);
         var address2 = new Address("streetLine2", "streetLine1", null, null);

         //Act
         var address1HashCode = address1.GetHashCode();
         var address2HashCode = address2.GetHashCode();

         //Assert
         Assert.AreNotEqual(address1HashCode, address2HashCode);
      }

      [TestMethod()]
      public void SameDataInDiferentPropertiesProduceDiferentHashCodeTest()
      {
         //Arrange
         var address1 = new Address("streetLine1", null, null, "streetLine1");
         var address2 = new Address(null, "streetLine1", "streetLine1", null);

         //Act
         var address1HashCode = address1.GetHashCode();
         var address2HashCode = address2.GetHashCode();

         //Assert
         Assert.AreNotEqual(address1HashCode, address2HashCode);
      }

      [TestMethod()]
      public void SameReferenceEqualsTrueTest()
      {
         //Arrange
         var address1 = new Address("streetLine1", null, null, "streetLine1");
         var address2 = address1;

         //Act
         if (!address1.Equals(address2)) { Assert.Fail(); }

         if (!(address1 == address2)) { Assert.Fail(); }

      }

      [TestMethod()]
      public void SameDataSameHashCodeTest()
      {
         //Arrange
         var address1 = new Address("streetLine1", "streetLine2", null, null);
         var address2 = new Address("streetLine1", "streetLine2", null, null);

         //Act
         var address1HashCode = address1.GetHashCode();
         var address2HashCode = address2.GetHashCode();

         //Assert
         Assert.AreEqual(address1HashCode, address2HashCode);
      }

      [TestMethod()]
      public void SelfReferenceNotProduceInfiniteLoop()
      {
         //Arrange
         var aReference = new SelfReference();
         var bReference = new SelfReference();

         //Act
         aReference.Value = bReference;
         bReference.Value = aReference;

         //Assert

         Assert.AreNotEqual(aReference, bReference);
      }

   }

}