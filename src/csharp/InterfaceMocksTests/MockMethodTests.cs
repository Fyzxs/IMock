using FluentAssertions;
using InterfaceMocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace InterfaceMocksTests
{
    [TestClass]
    public class MockMethodTests
    {
        [TestMethod, TestCategory("unit")]
        public void InvokeShouldThrowExceptionWithMethodNameIfInvocationNotUpdated()
        {
            // Arrange
            MockMethod subject = new MockMethod("methodName");

            // Act
            Action actual = () => subject.Invoke();

            // Assert
            actual.Should().ThrowExactly<TestException>().WithMessage("If you want to use methodName, configure via Builder.");
        }

        [TestMethod, TestCategory("unit")]
        public void InvokeShouldNotThrowExceptionIfInvocationUpdated()
        {
            // Arrange
            MockMethod subject = new MockMethod("methodName");
            subject.UpdateInvocation();

            // Act
            Action actual = () => subject.Invoke();

            // Assert
            actual.Should().NotThrow();
        }

        [TestMethod, TestCategory("unit")]
        public void InvokeTaskShouldThrowExceptionWithMethodNameIfInvocationNotUpdated()
        {
            // Arrange
            MockMethod subject = new MockMethod("methodName");

            // Act
            Func<Task> actual = async () => await subject.InvokeTask();

            // Assert
            actual.Should().ThrowExactly<TestException>().WithMessage("If you want to use methodName, configure via Builder.");
        }

        [TestMethod, TestCategory("unit")]
        public void InvokeTaskShouldNotThrowExceptionIfInvocationUpdated()
        {
            // Arrange
            MockMethod subject = new MockMethod("methodName");
            subject.UpdateInvocation();

            // Act
            Func<Task> actual = async () => await subject.InvokeTask();

            // Assert
            actual.Should().NotThrow();
        }

    }
}