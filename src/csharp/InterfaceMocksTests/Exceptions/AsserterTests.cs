using FluentAssertions;
using InterfaceMocks.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace InterfaceMocksTests.Exceptions
{
    [TestClass]
    public class AsserterTests
    {
        [TestMethod, TestCategory("unit")]
        public void AssertIf_ShouldNotThrowWhenFalseCondition()
        {
            //Arrange
            Asserter subject = new Asserter();

            //Act
            Action action = () => subject.AssertIf(false, "Asserting");

            //Assert
            action.Should().NotThrow();
        }

        [TestMethod, TestCategory("unit")]
        public void AssertIf_ShouldThrowWhenTrueCondition()
        {
            //Arrange
            Asserter subject = new Asserter();

            //Act
            Action action = () => subject.AssertIf(true, "Asserted");

            //Assert
            action.Should().Throw<Exception>().WithMessage("Asserted");
        }
    }
}