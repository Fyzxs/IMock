﻿using FluentAssertions;
using InterfaceFakesTests.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace InterfaceFakesTests.Reflection
{
    [TestClass]
    public class PrivateConstructorInfoTests
    {
        [TestMethod, TestCategory("unit")]
        public void CtorInfo_ShouldReturnConstructorInfo()
        {
            //Arrange
            PrivateConstructorInfo privateConstructorInfo = new PrivateConstructorInfo(typeof(WithPrivateCtor), new object[0]);

            //Act
            ConstructorInfo ctorInfo = privateConstructorInfo.CtorInfo();

            //Assert
            ctorInfo.Should().NotBeNull();
        }

        private class WithPrivateCtor
        {
            private WithPrivateCtor() { }
        }
    }
}