using FluentAssertions;
using InterfaceFakes.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterfaceFakesTests.Library
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