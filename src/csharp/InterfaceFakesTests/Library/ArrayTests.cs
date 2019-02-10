using FluentAssertions;
using InterfaceFakes.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterfaceFakesTests.Library
{
    [TestClass]
    public sealed class ArrayTests
    {
        [TestMethod, TestCategory("unit")]
        public void ShouldBeArrayOfType()
        {
            //Arrange
            TestArray subject = new TestArray();

            //Act
            object[] actual = subject;

            //Assert
            actual.Should().NotBeNull();
        }

        private class TestArray : Array<object>
        {
            protected override object[] Value() => new object[0];
        }
    }
}