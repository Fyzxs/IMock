using FluentAssertions;
using InterfaceFakes;
using InterfaceFakes.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace InterfaceFakesTests
{
    [TestClass]
    public class FakeMethodTests
    {
        [TestMethod, TestCategory("unit")]
        public void Invoke_ShouldThrowExceptionWithMethodNameIfInvocationNotUpdated()
        {
            // Arrange
            FakeMethod subject = new FakeMethod("methodName");

            // Act
            Action actual = () => subject.Invoke();

            // Assert
            actual.Should().ThrowExactly<TestException>().WithMessage("If you want to use methodName, configure via Builder.");
        }

        [TestMethod, TestCategory("unit")]
        public void Invoke_ShouldNotThrowExceptionIfInvocationUpdated()
        {
            // Arrange
            FakeMethod subject = new FakeMethod("methodName");
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
            FakeMethod subject = new FakeMethod("methodName");
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
            FakeMethod subject = new FakeMethod("methodName");

            // Act
            Func<Task> actual = async () => await subject.InvokeTask();

            // Assert
            actual.Should().ThrowExactly<TestException>().WithMessage("If you want to use methodName, configure via Builder.");
        }

        [TestMethod, TestCategory("unit")]
        public void InvokeTask_ShouldNotThrowExceptionIfInvocationUpdated()
        {
            // Arrange
            FakeMethod subject = new FakeMethod("methodName");
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
            FakeMethod subject = new FakeMethod("methodName");
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
            FakeMethod subject = new FakeMethod("methodName");
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