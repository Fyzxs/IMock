using FluentAssertions;
using InterfaceMocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using InterfaceMocks.Exceptions;

namespace InterfaceMocksTests
{
    [TestClass]
    public class MockMethodWithResponseTests
    {
        [TestMethod, TestCategory("unit")]
        public void Invoke_ShouldThrowExceptionWithMethodNameIfInvocationNotUpdated()
        {
            // Arrange
            MockMethodWithResponse<string> subject = new MockMethodWithResponse<string>("methodName");

            // Act
            Action actual = () => subject.Invoke();

            // Assert
            actual.Should().ThrowExactly<TestException>().WithMessage("If you want to use methodName, configure via Builder.");
        }

        [TestMethod, TestCategory("unit")]
        public void Invoke_ShouldReturnValuePassedIntoUpdateInvocation()
        {
            // Arrange
            string expected = "result";
            MockMethodWithResponse<string> subject = new MockMethodWithResponse<string>("methodName");
            subject.UpdateInvocation(expected);

            // Act
            string actual = subject.Invoke();

            // Assert
            actual.Should().Be(expected);
        }

        [TestMethod, TestCategory("unit")]
        public void Invoke_ShouldReturnSameValueWhenInvokedMultipleTimesAndSetupWithOneResponse()
        {
            // Arrange
            string expected = "result";
            MockMethodWithResponse<string> subject = new MockMethodWithResponse<string>("methodName");
            subject.UpdateInvocation(expected);

            // Act
            string actual1 = subject.Invoke();
            string actual2 = subject.Invoke();
            string actual3 = subject.Invoke();


            // Assert
            actual1.Should().Be(expected);
            actual2.Should().Be(expected);
            actual3.Should().Be(expected);
        }
        [TestMethod, TestCategory("unit")]
        public void Invoke_ShouldReturnValuesInOrderPassedIntoUpdateInvocationWhenInvokedMultipleTimes()
        {
            // Arrange
            string expected1 = "result1";
            string expected2 = "result2";
            string expected3 = "result3";
            MockMethodWithResponse<string> subject = new MockMethodWithResponse<string>("methodName");
            subject.UpdateInvocation(expected1, expected2, expected3);

            // Act
            string actual1 = subject.Invoke();
            string actual2 = subject.Invoke();
            string actual3 = subject.Invoke();

            // Assert
            actual1.Should().Be(expected1);
            actual2.Should().Be(expected2);
            actual3.Should().Be(expected3);
        }

        [TestMethod, TestCategory("unit")]
        public void Invoke_ShouldThrowExceptionWhenUpdateInvocationSetUpForThat()
        {
            // Arrange
            MockMethodWithResponse<string> subject = new MockMethodWithResponse<string>("methodName");


            subject.UpdateInvocation(() => throw new Exception("I throw this"));

            // Act
            Action actual = () => subject.Invoke();

            // Assert
            actual.Should().ThrowExactly<Exception>().WithMessage("I throw this");
        }

        [TestMethod, TestCategory("unit")]
        public void InvokeTask_ShouldThrowExceptionWithMethodNameIfInvocationNotUpdated()
        {
            // Arrange
            MockMethodWithResponse<string> subject = new MockMethodWithResponse<string>("methodName");

            // Act
            Func<Task<string>> actual = async () => await subject.InvokeTask();

            // Assert
            actual.Should().ThrowExactly<TestException>().WithMessage("If you want to use methodName, configure via Builder.");
        }

        [TestMethod, TestCategory("unit")]
        public async Task InvokeTask_ShouldReturnValuePassedIntoUpdateInvocation()
        {
            // Arrange
            string expected = "result";
            MockMethodWithResponse<string> subject = new MockMethodWithResponse<string>("methodName");
            subject.UpdateInvocation(expected);

            // Act
            string actual = await subject.InvokeTask();

            // Assert
            actual.Should().Be(expected);
        }

        [TestMethod, TestCategory("unit")]
        public async Task InvokeTask_ShouldReturnSameValueWhenInvokedMultipleTimesAndSetupWithOneResponse()
        {
            // Arrange
            string expected = "result";
            MockMethodWithResponse<string> subject = new MockMethodWithResponse<string>("methodName");
            subject.UpdateInvocation(expected);

            // Act
            string actual1 = await subject.InvokeTask();
            string actual2 = await subject.InvokeTask();
            string actual3 = await subject.InvokeTask();


            // Assert
            actual1.Should().Be(expected);
            actual2.Should().Be(expected);
            actual3.Should().Be(expected);
        }

        [TestMethod, TestCategory("unit")]
        public async Task InvokeTask_ShouldReturnValuesInOrderPassedIntoUpdateInvocationWhenInvokedMultipleTimes()
        {
            // Arrange
            string expected1 = "result1";
            string expected2 = "result2";
            string expected3 = "result3";
            MockMethodWithResponse<string> subject = new MockMethodWithResponse<string>("methodName");
            subject.UpdateInvocation(expected1, expected2, expected3);

            // Act
            string actual1 = await subject.InvokeTask();
            string actual2 = await subject.InvokeTask();
            string actual3 = await subject.InvokeTask();

            // Assert
            actual1.Should().Be(expected1);
            actual2.Should().Be(expected2);
            actual3.Should().Be(expected3);
        }

        [TestMethod, TestCategory("unit")]
        public void InvokeTask_ShouldThrowExceptionWhenUpdateInvocationSetUpForThat()
        {
            // Arrange
            MockMethodWithResponse<string> subject = new MockMethodWithResponse<string>("methodName");


            subject.UpdateInvocation(() => throw new Exception("I throw this"));

            // Act
            Func<Task<string>> actual = async () => await subject.InvokeTask();

            // Assert
            actual.Should().ThrowExactly<Exception>().WithMessage("I throw this");
        }
    }
}