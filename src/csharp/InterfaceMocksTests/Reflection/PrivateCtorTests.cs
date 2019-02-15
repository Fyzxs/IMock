using FluentAssertions;
using InterfaceMocks.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterfaceMocksTests.Reflection
{
    [TestClass]
    public sealed class PrivateCtorTests
    {
        [TestMethod, TestCategory("unit")]
        public void ShouldBeOfType()
        {
            //Arrange
            PrivateCtor<WithPrivateCtor> subject = new PrivateCtor<WithPrivateCtor>();

            //Act
            WithPrivateCtor actual = subject;

            //Assert
            actual.Should().NotBeNull();
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        private class WithPrivateCtor
        {
            private WithPrivateCtor() { }
        }
    }
}