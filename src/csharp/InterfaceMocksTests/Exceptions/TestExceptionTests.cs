using FluentAssertions;
using InterfaceMocks.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterfaceMocksTests.Exceptions
{
    [TestClass]
    public sealed class TestExceptionTests
    {
        [TestMethod, TestCategory("unit")]
        public void Message_ShouldHaveNameInMethod()
        {
            //Arrange
            TestException subject = new TestException("The Name");

            //Act
            string actual = subject.Message;

            //Assert
            actual.Should().Be($"If you want to use The Name, configure via Builder.");
        }
    }
}