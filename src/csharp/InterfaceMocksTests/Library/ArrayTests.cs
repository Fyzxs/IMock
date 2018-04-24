using FluentAssertions;
using InterfaceMocks.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterfaceMocksTests.Library
{
    [TestClass]
    public class ArrayTests
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