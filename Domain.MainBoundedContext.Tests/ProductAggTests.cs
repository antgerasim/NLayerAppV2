

namespace Domain.MainBoundedContext.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.ProductAgg;
    using System.ComponentModel.DataAnnotations;

    [TestClass()]
    public class ProductAggTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotCreateAProductWithNullTitle()
        {
            //Arrange and Act
            var product = new Book(null, "the description","publisher","isbn");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotCreateAProductWithNullDescription()
        {
            //Arrange and Act
            var product = new Book("the title", null,"publisher","isbn");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotCreateAProductWithNullPublisher()
        {
            //Arrange and Act
            var product = new Book("the title", "description", null, "isbn");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotCreateAProductWithNullIsbn()
        {
            //Arrange and Act
            var product = new Book("the title", "description", "publisher", null);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotCreateAProductWithEmptyTitle()
        {
            //Arrange and Act
            var product = new Book(string.Empty, "the description","publisher","isbn");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotCreateAProductWithEmptyDescription()
        {
            //Arrange and Act
            var product = new Book("the title", string.Empty,"publisher","isbn");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotCreateAProductWithEmptyPublisher()
        {
            //Arrange and Act
            var product = new Book("the title", "description", string.Empty, "isbn");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotCreateAProductWithEmptyIsbn()
        {
            //Arrange and Act
            var product = new Book("the title", "description", "publisher",string.Empty);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotCreateAProductWithWhitespaceTitle()
        {
            //Arrange and Act
            var product = new Book(" ", "the description","publisher","isbn");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotCreateAProductWithWhitespaceDescription()
        {
            //Arrange and Act
            var product = new Book("the title", " ","publisher","isbn");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotCreateAProductWithWhitespacePublisher()
        {
            //Arrange and Act
            var product = new Book("the title", "description", " ", "isbn");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotCreateAProductWithWhitespaceISBN()
        {
            //Arrange and Act
            var product = new Book("the title","description", "publisher", " ");
        }
    }
}
