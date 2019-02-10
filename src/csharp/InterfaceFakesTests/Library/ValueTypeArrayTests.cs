using FluentAssertions;
using InterfaceFakes.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace InterfaceFakesTests.Library
{
    [TestClass]
    public sealed class ValueTypeArrayTests
    {
        [TestMethod, TestCategory("unit")]
        public void ShouldBeTypeOfInstances()
        {
            //Arrange
            ValueTypeArray subject = new ValueTypeArray("string", new object(), new SomeType());

            //Act
            Type[] actual = subject;

            //Assert
            actual.Length.Should().Be(3);
            actual[0].Should().Be(typeof(string));
            actual[1].Should().Be(typeof(object));
            actual[2].Should().Be(typeof(SomeType));
        }

        private class SomeType { }
    }
}