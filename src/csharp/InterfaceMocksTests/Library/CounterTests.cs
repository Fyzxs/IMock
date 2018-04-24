using FluentAssertions;
using InterfaceMocks.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterfaceMocksTests.Library
{
    [TestClass]
    public class CounterTests
    {
        [TestMethod, TestCategory("unit")]
        public void Value_ShouldReturnCurrentValue()
        {
            //Arrange
            Counter subject = new Counter();

            //Act
            long actual = subject.Value();

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod, TestCategory("unit")]
        public void increment_ShouldIncrementValue()
        {
            //Arrange
            Counter subject = new Counter();

            //Act
            subject.Increment();

            //Assert
            long actual = subject.Value();
            actual.Should().Be(1);
        }
    }
}