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
    using System;
    using System.Linq.Expressions;
    using System.Linq;

    using Domain.Seedwork.Tests.Classes;

    using Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Samples.NLayerApp.Domain.Seedwork;
    using System.Collections.Generic;

    /// <summary>
    /// Summary description for SpecificationTests
    /// </summary>
    [TestClass]
    public class SpecificationTests
    {

        [TestMethod]
        public void CreateNewDirectSpecificationTest()
        {
            //Arrange
            DirectSpecification<SampleEntity> adHocSpecification;
            Expression<Func<SampleEntity, bool>> spec = s => s.Id == Guid.NewGuid();

            //Act
            adHocSpecification = new DirectSpecification<SampleEntity>(spec);

            //Assert
            Assert.ReferenceEquals(new PrivateObject(adHocSpecification).GetField("_MatchingCriteria"), spec);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateDirectSpecificationNullSpecThrowArgumentNullExceptionTest()
        {
            //Arrange
            DirectSpecification<SampleEntity> adHocSpecification;
            Expression<Func<SampleEntity, bool>> spec = null;

            //Act
            adHocSpecification = new DirectSpecification<SampleEntity>(spec);
        }
        [TestMethod()]
        public void CreateAndSpecificationTest()
        {
            //Arrange
            DirectSpecification<SampleEntity> leftAdHocSpecification;
            DirectSpecification<SampleEntity> rightAdHocSpecification;

            var identifier = Guid.NewGuid();
            Expression<Func<SampleEntity, bool>> leftSpec = s => s.Id == identifier;
            Expression<Func<SampleEntity, bool>> rightSpec = s => s.SampleProperty.Length > 2;

            leftAdHocSpecification = new DirectSpecification<SampleEntity>(leftSpec);
            rightAdHocSpecification = new DirectSpecification<SampleEntity>(rightSpec);

            //Act
            AndSpecification<SampleEntity> composite = new AndSpecification<SampleEntity>(leftAdHocSpecification, rightAdHocSpecification);

            //Assert
            Assert.IsNotNull(composite.SatisfiedBy());
            Assert.ReferenceEquals(leftAdHocSpecification, composite.LeftSideSpecification);
            Assert.ReferenceEquals(rightAdHocSpecification, composite.RightSideSpecification);

            var list = new List<SampleEntity>();
            var sampleA = new SampleEntity() {  SampleProperty = "1" };
            sampleA.ChangeCurrentIdentity(identifier);

            var sampleB = new SampleEntity() { SampleProperty = "the sample property" };
            sampleB.ChangeCurrentIdentity(identifier);

            list.AddRange(new SampleEntity[] { sampleA, sampleB });
            

            var result = list.AsQueryable().Where(composite.SatisfiedBy()).ToList();

            Assert.IsTrue(result.Count == 1);
        }
        [TestMethod()]
        public void CreateOrSpecificationTest()
        {
            //Arrange
            DirectSpecification<SampleEntity> leftAdHocSpecification;
            DirectSpecification<SampleEntity> rightAdHocSpecification;

            var identifier = Guid.NewGuid();
            Expression<Func<SampleEntity, bool>> leftSpec = s => s.Id == identifier;
            Expression<Func<SampleEntity, bool>> rightSpec = s => s.SampleProperty.Length > 2;

            leftAdHocSpecification = new DirectSpecification<SampleEntity>(leftSpec);
            rightAdHocSpecification = new DirectSpecification<SampleEntity>(rightSpec);

            //Act
            OrSpecification<SampleEntity> composite = new OrSpecification<SampleEntity>(leftAdHocSpecification, rightAdHocSpecification);

            //Assert
            Assert.IsNotNull(composite.SatisfiedBy());
            Assert.ReferenceEquals(leftAdHocSpecification, composite.LeftSideSpecification);
            Assert.ReferenceEquals(rightAdHocSpecification, composite.RightSideSpecification);

            var list = new List<SampleEntity>();

            var sampleA = new SampleEntity() { SampleProperty = "1" };
            sampleA.ChangeCurrentIdentity(identifier);

            var sampleB = new SampleEntity() { SampleProperty = "the sample property" };
            sampleB.GenerateNewIdentity();

            list.AddRange(new SampleEntity[] { sampleA, sampleB });


            var result = list.AsQueryable().Where(composite.SatisfiedBy()).ToList();

            Assert.IsTrue(result.Count() == 2);

            

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateAndSpecificationNullLeftSpecThrowArgumentNullExceptionTest()
        {
            //Arrange
            DirectSpecification<SampleEntity> leftAdHocSpecification;
            DirectSpecification<SampleEntity> rightAdHocSpecification;

            Expression<Func<SampleEntity, bool>> leftSpec = s => s.Id == Guid.NewGuid();
            Expression<Func<SampleEntity, bool>> rightSpec = s => s.SampleProperty.Length > 2;

            leftAdHocSpecification = new DirectSpecification<SampleEntity>(leftSpec);
            rightAdHocSpecification = new DirectSpecification<SampleEntity>(rightSpec);

            //Act
            AndSpecification<SampleEntity> composite = new AndSpecification<SampleEntity>(null, rightAdHocSpecification);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateAndSpecificationNullRightSpecThrowArgumentNullExceptionTest()
        {
            //Arrange
            DirectSpecification<SampleEntity> leftAdHocSpecification;
            DirectSpecification<SampleEntity> rightAdHocSpecification;

            Expression<Func<SampleEntity, bool>> rightSpec = s => s.Id == Guid.NewGuid();
            Expression<Func<SampleEntity, bool>> leftSpec = s => s.SampleProperty.Length > 2;

            leftAdHocSpecification = new DirectSpecification<SampleEntity>(leftSpec);
            rightAdHocSpecification = new DirectSpecification<SampleEntity>(rightSpec);

            //Act
            AndSpecification<SampleEntity> composite = new AndSpecification<SampleEntity>(leftAdHocSpecification, null);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateOrSpecificationNullLeftSpecThrowArgumentNullExceptionTest()
        {
            //Arrange
            DirectSpecification<SampleEntity> leftAdHocSpecification;
            DirectSpecification<SampleEntity> rightAdHocSpecification;

            Expression<Func<SampleEntity, bool>> leftSpec = s => s.Id == Guid.NewGuid();
            Expression<Func<SampleEntity, bool>> rightSpec = s => s.SampleProperty.Length > 2;

            leftAdHocSpecification = new DirectSpecification<SampleEntity>(leftSpec);
            rightAdHocSpecification = new DirectSpecification<SampleEntity>(rightSpec);

            //Act
            OrSpecification<SampleEntity> composite = new OrSpecification<SampleEntity>(null, rightAdHocSpecification);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateOrSpecificationNullRightSpecThrowArgumentNullExceptionTest()
        {
            //Arrange
            DirectSpecification<SampleEntity> leftAdHocSpecification;
            DirectSpecification<SampleEntity> rightAdHocSpecification;

            Expression<Func<SampleEntity, bool>> rightSpec = s => s.Id == Guid.NewGuid();
            Expression<Func<SampleEntity, bool>> leftSpec = s => s.SampleProperty.Length > 2;

            leftAdHocSpecification = new DirectSpecification<SampleEntity>(leftSpec);
            rightAdHocSpecification = new DirectSpecification<SampleEntity>(rightSpec);

            //Act
            OrSpecification<SampleEntity> composite = new OrSpecification<SampleEntity>(leftAdHocSpecification, null);

        }
        [TestMethod]
        public void UseSpecificationLogicAndOperatorTest()
        {
            //Arrange
            DirectSpecification<SampleEntity> leftAdHocSpecification;
            DirectSpecification<SampleEntity> rightAdHocSpecification;

            var identifier = Guid.NewGuid();
            Expression<Func<SampleEntity, bool>> leftSpec = s => s.Id == identifier;
            Expression<Func<SampleEntity, bool>> rightSpec = s => s.SampleProperty.Length > 2;


            //Act
            leftAdHocSpecification = new DirectSpecification<SampleEntity>(leftSpec);
            rightAdHocSpecification = new DirectSpecification<SampleEntity>(rightSpec);

            ISpecification<SampleEntity> andSpec = leftAdHocSpecification & rightAdHocSpecification;
            
            //Assert

            var list = new List<SampleEntity>();
            var sampleA = new SampleEntity() { SampleProperty = "1" };
            sampleA.ChangeCurrentIdentity(identifier);

            var sampleB = new SampleEntity() { SampleProperty = "the sample property" };
            sampleB.GenerateNewIdentity();

            var sampleC = new SampleEntity() { SampleProperty = "the sample property" };
            sampleC.ChangeCurrentIdentity(identifier);

            list.AddRange(new SampleEntity[] { sampleA, sampleB, sampleC });

            var result = list.AsQueryable().Where(andSpec.SatisfiedBy()).ToList();

            Assert.IsTrue(result.Count == 1);
        }
        [TestMethod]
        public void UseSpecificationAndOperatorTest()
        {
            //Arrange
            DirectSpecification<SampleEntity> leftAdHocSpecification;
            DirectSpecification<SampleEntity> rightAdHocSpecification;

            var identifier = Guid.NewGuid();
            Expression<Func<SampleEntity, bool>> leftSpec = s => s.Id == identifier;
            Expression<Func<SampleEntity, bool>> rightSpec = s => s.SampleProperty.Length > 2;


            //Act
            leftAdHocSpecification = new DirectSpecification<SampleEntity>(leftSpec);
            rightAdHocSpecification = new DirectSpecification<SampleEntity>(rightSpec);

            ISpecification<SampleEntity> andSpec = leftAdHocSpecification && rightAdHocSpecification;

            var list = new List<SampleEntity>();

            var sampleA = new SampleEntity() { SampleProperty = "1" };
            sampleA.ChangeCurrentIdentity(identifier);

            var sampleB = new SampleEntity() { SampleProperty = "the sample property" };
            sampleB.GenerateNewIdentity();

            var sampleC = new SampleEntity() { SampleProperty = "the sample property" };
            sampleC.ChangeCurrentIdentity(identifier);

            list.AddRange(new SampleEntity[] { sampleA, sampleB, sampleC });


            var result = list.AsQueryable().Where(andSpec.SatisfiedBy()).ToList();

            Assert.IsTrue(result.Count == 1);

        }
        [TestMethod]
        public void UseSpecificationBitwiseOrOperatorTest()
        {
            //Arrange
            DirectSpecification<SampleEntity> leftAdHocSpecification;
            DirectSpecification<SampleEntity> rightAdHocSpecification;

            var identifier = Guid.NewGuid();
            Expression<Func<SampleEntity, bool>> leftSpec = s => s.Id == identifier;
            Expression<Func<SampleEntity, bool>> rightSpec = s => s.SampleProperty.Length > 2;


            //Act
            leftAdHocSpecification = new DirectSpecification<SampleEntity>(leftSpec);
            rightAdHocSpecification = new DirectSpecification<SampleEntity>(rightSpec);

            var orSpec = leftAdHocSpecification | rightAdHocSpecification;


            //Assert
            var list = new List<SampleEntity>();

            var sampleA = new SampleEntity() { SampleProperty = "1" };
            sampleA.ChangeCurrentIdentity(identifier);

            var sampleB = new SampleEntity() { SampleProperty = "the sample property" };
            sampleB.GenerateNewIdentity();

            list.AddRange(new SampleEntity[] { sampleA, sampleB });

            var result = list.AsQueryable().Where(orSpec.SatisfiedBy()).ToList();
        }
        [TestMethod]
        public void UseSpecificationOrOperatorTest()
        {
            //Arrange
            DirectSpecification<SampleEntity> leftAdHocSpecification;
            DirectSpecification<SampleEntity> rightAdHocSpecification;

            var identifier = Guid.NewGuid();
            Expression<Func<SampleEntity, bool>> leftSpec = s => s.Id == identifier;
            Expression<Func<SampleEntity, bool>> rightSpec = s => s.SampleProperty.Length > 2;

            
            //Act
            leftAdHocSpecification = new DirectSpecification<SampleEntity>(leftSpec);
            rightAdHocSpecification = new DirectSpecification<SampleEntity>(rightSpec);

            var orSpec = leftAdHocSpecification || rightAdHocSpecification;
            

            //Assert
            var list = new List<SampleEntity>();
            var sampleA = new SampleEntity() { SampleProperty = "1" };
            sampleA.ChangeCurrentIdentity(identifier);

            var sampleB = new SampleEntity() { SampleProperty = "the sample property" };
            sampleB.GenerateNewIdentity();

            list.AddRange(new SampleEntity[] { sampleA, sampleB });

            var result = list.AsQueryable().Where(orSpec.SatisfiedBy()).ToList();

            Assert.IsTrue(result.Count() == 2);
        }
        [TestMethod()]
        public void CreateNotSpecificationithSpecificationTest()
        {
            //Arrange
            Expression<Func<SampleEntity, bool>> specificationCriteria = t => t.Id == Guid.NewGuid();
            DirectSpecification<SampleEntity> specification = new DirectSpecification<SampleEntity>(specificationCriteria);

            //Act
            NotSpecification<SampleEntity> notSpec = new NotSpecification<SampleEntity>(specification);
            Expression<Func<SampleEntity, bool>> resultCriteria = new PrivateObject(notSpec).GetField("_OriginalCriteria") as Expression<Func<SampleEntity, bool>>;

            //Assert
            Assert.IsNotNull(notSpec);
            Assert.IsNotNull(resultCriteria);
            Assert.IsNotNull(notSpec.SatisfiedBy());

        }
        [TestMethod()]
        public void CreateNotSpecificationWithCriteriaTest()
        {
            //Arrange
            Expression<Func<SampleEntity, bool>> specificationCriteria = t => t.Id == Guid.NewGuid();


            //Act
            NotSpecification<SampleEntity> notSpec = new NotSpecification<SampleEntity>(specificationCriteria);

            //Assert
            Assert.IsNotNull(notSpec);
            Assert.IsNotNull(new PrivateObject(notSpec).GetField("_OriginalCriteria"));
        }
        [TestMethod()]
        public void CreateNotSpecificationFromNegationOperator()
        {
            //Arrange
            Expression<Func<SampleEntity, bool>> specificationCriteria = t => t.Id == Guid.NewGuid();


            //Act
            DirectSpecification<SampleEntity> spec = new DirectSpecification<SampleEntity>(specificationCriteria);
            ISpecification<SampleEntity> notSpec = !spec;

            //Assert
            Assert.IsNotNull(notSpec);

        }
        [TestMethod()]
        public void CheckNotSpecificationOperators()
        {
            //Arrange
            Expression<Func<SampleEntity, bool>> specificationCriteria = t => t.Id == Guid.NewGuid();


            //Act
            Specification<SampleEntity> spec = new DirectSpecification<SampleEntity>(specificationCriteria);
            Specification<SampleEntity> notSpec = !spec;
            ISpecification<SampleEntity> resultAnd = notSpec && spec;
            ISpecification<SampleEntity> resultOr = notSpec || spec;

            //Assert
            Assert.IsNotNull(notSpec);
            Assert.IsNotNull(resultAnd);
            Assert.IsNotNull(resultOr);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateNotSpecificationNullSpecificationThrowArgumentNullExceptionTest()
        {
            //Arrange
            NotSpecification<SampleEntity> notSpec;

            //Act
            notSpec = new NotSpecification<SampleEntity>((ISpecification<SampleEntity>)null);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateNotSpecificationNullCriteriaThrowArgumentNullExceptionTest()
        {
            //Arrange
            NotSpecification<SampleEntity> notSpec;

            //Act
            notSpec = new NotSpecification<SampleEntity>((Expression<Func<SampleEntity, bool>>)null);
        }
        [TestMethod()]
        public void CreateTrueSpecificationTest()
        {
            //Arrange
            ISpecification<SampleEntity> trueSpec = new TrueSpecification<SampleEntity>();
            bool expected = true;
            bool actual = trueSpec.SatisfiedBy().Compile()(new SampleEntity());
            //Assert
            Assert.IsNotNull(trueSpec);
            Assert.AreEqual(expected, actual);
        }
    }
}
