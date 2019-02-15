using FluentAssertions;
using InterfaceMocks;
using InterfaceMocks.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace InterfaceMocksTests
{
    [TestClass]
    public sealed class MockMethodTests
    {
        [TestMethod, TestCategory("unit")]
        public void Invoke_ShouldThrowExceptionWithMethodNameIfInvocationNotUpdated()
        {
            // Arrange
            MockMethod subject = new MockMethod("methodName");

            // Act
            Action actual = () => subject.Invoke();

            // Assert
            actual.Should().ThrowExactly<TestException>().WithMessage("If you want to use methodName, configure via Builder.");
        }

        [TestMethod, TestCategory("unit")]
        public void Invoke_ShouldNotThrowExceptionIfInvocationUpdated()
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
        public void Invoke_ShouldHaveMultipleInvocations()
        {
            // Arrange
            MockMethod subject = new MockMethod("methodName");
            subject.UpdateInvocation(() => { }, () => throw new Exception("Second Invocation"));

            // Act
            Action actual = () => subject.Invoke();
            Action thrower = () => subject.Invoke();

            // Assert
            actual.Should().NotThrow();
            thrower.Should().ThrowExactly<Exception>().WithMessage("Second Invocation");
        }

        [TestMethod, TestCategory("unit")]
        public void InvokeTask_ShouldThrowExceptionWithMethodNameIfInvocationNotUpdated()
        {
            // Arrange
            MockMethod subject = new MockMethod("methodName");

            // Act
            Func<Task> actual = async () => await subject.InvokeTask();

            // Assert
            actual.Should().ThrowExactly<TestException>().WithMessage("If you want to use methodName, configure via Builder.");
        }

        [TestMethod, TestCategory("unit")]
        public void InvokeTask_ShouldNotThrowExceptionIfInvocationUpdated()
        {
            // Arrange
            MockMethod subject = new MockMethod("methodName");
            subject.UpdateInvocation();

            // Act
            Func<Task> actual = async () => await subject.InvokeTask();

            // Assert
            actual.Should().NotThrow();
        }

        [TestMethod, TestCategory("unit")]
        public void InvokeTask_ShouldHaveMultipleInvocations()
        {
            // Arrange
            MockMethod subject = new MockMethod("methodName");
            subject.UpdateInvocation(() => { }, () => throw new Exception("Second Invocation"));

            // Act
            Func<Task> actual = async () => await subject.InvokeTask();
            Func<Task> thrower = async () => await subject.InvokeTask();

            // Assert
            actual.Should().NotThrow();
            thrower.Should().ThrowExactly<Exception>().WithMessage("Second Invocation");
        }

        [TestMethod, TestCategory("unit")]
        public void InvokeTask_ShouldTrackInvocationWithExceptionThrown()
        {
            // Arrange
            MockMethod subject = new MockMethod("methodName");
            subject.UpdateInvocation(() => { }, () => throw new Exception("Second Invocation"));

            // Act
            Func<Task> actual = async () => await subject.InvokeTask();
            Func<Task> thrower = async () => await subject.InvokeTask();

            // Assert
            actual.Should().NotThrow();
            thrower.Should().ThrowExactly<Exception>().WithMessage("Second Invocation");
            subject.AssertInvokedCountMatches(2);
        }
    }
}