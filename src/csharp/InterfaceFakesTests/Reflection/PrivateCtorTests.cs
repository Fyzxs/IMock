using FluentAssertions;
using InterfaceFakes.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterfaceFakesTests.Reflection
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