using FluentAssertions;
using InterfaceMocks;
using InterfaceMocks.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace InterfaceMocksTests
{
    [TestClass]
    public class MockMethodWithParamTests
    {
        //**** Invoke ****

        [TestMethod, TestCategory("unit")]
        public void Invoke_ShouldThrowExceptionWithMethodNameIfInvocationNotUpdated()
        {
            // Arrange
            string expected = "expected";
            MockMethodWithParam<string> subject = new MockMethodWithParam<string>("methodName");

            // Act
            Action actual = () => subject.Invoke(expected);

            // Assert
            actual.Should().ThrowExactly<TestException>().WithMessage("If you want to use methodName, configure via Builder.");
        }

        [TestMethod, TestCategory("unit")]
        public void Invoke_ShouldNotThrowExceptionIfInvocationUpdated()
        {
            // Arrange
            string expected = "expected";
            MockMethodWithParam<string> subject = new MockMethodWithParam<string>("methodName");
            subject.UpdateInvocation();

            // Act
            Action actual = () => subject.Invoke(expected);

            // Assert
            actual.Should().NotThrow();
        }

        [TestMethod, TestCategory("unit")]
        public void AssertInvokedWith_ShouldThrowWhenFalse_WithInvoke()
        {
            // Arrange
            MockMethodWithParam<string> subject = new MockMethodWithParam<string>("methodName");
            subject.UpdateInvocation();
            subject.Invoke("Not expected");

            // Act
            Action actual = () => subject.AssertInvokedWith("expected");

            // Assert
            actual.Should().Throw<Exception>().WithMessage("Expected methodName to be invoked with expected but was actually invoked with Not expected");
        }

        [TestMethod, TestCategory("unit")]
        public void AssertInvokedWith_ShouldNotThrowWhenTrue_WithInvoke()
        {
            // Arrange
            MockMethodWithParam<string> subject = new MockMethodWithParam<string>("methodName");
            subject.UpdateInvocation();
            subject.Invoke("expected");

            // Act
            Action actual = () => subject.AssertInvokedWith("expected");

            // Assert
            actual.Should().NotThrow();
        }

        [TestMethod, TestCategory("unit")]
        public void AssertCustom_ShouldThrowWhenFalse_WithInvoke()
        {
            // Arrange
            MockMethodWithParam<string> subject = new MockMethodWithParam<string>("methodName");
            subject.UpdateInvocation();
            subject.Invoke("Not expected");

            // Act
            Action actual = () => subject.AssertCustom(o => o.Equals("expected").Should().BeTrue("this is expected to throw"));

            // Assert
            actual.Should().Throw<Exception>();
        }

        [TestMethod, TestCategory("unit")]
        public void AssertCustom_ShouldNotThrowWhenTrue_WithInvoke()
        {
            // Arrange
            MockMethodWithParam<string> subject = new MockMethodWithParam<string>("methodName");
            subject.UpdateInvocation();
            subject.Invoke("expected");

            // Act
            Action actual = () => subject.AssertCustom(o => o.Equals("expected").Should().BeTrue());

            // Assert
            actual.Should().NotThrow();
        }

        [TestMethod, TestCategory("unit")]
        public void Invoke_ShouldThrowExceptionWhenUpdateInvocationSetUpForThat()
        {
            // Arrange
            string expected = "expected";
            MockMethodWithParam<string> subject = new MockMethodWithParam<string>("methodName");

            subject.UpdateInvocation(() => throw new Exception("I throw this"));

            // Act
            Action actual = () => subject.Invoke(expected);

            // Assert
            actual.Should().ThrowExactly<Exception>().WithMessage("I throw this");
        }

        [TestMethod, TestCategory("unit")]
        public void AssertInvokedWith_ShouldAssertInOrderOfInvocation_WithInvoke()
        {
            // Arrange
            string expected1 = "expected1";
            string expected2 = "expected2";
            MockMethodWithParam<string> subject = new MockMethodWithParam<string>("methodName");
            subject.UpdateInvocation();
            subject.Invoke(expected1);
            subject.Invoke(expected2);

            // Act
            Action actual = () => subject.AssertInvokedWith(expected1);
            Action actual2 = () => subject.AssertInvokedWith(expected2);

            // Assert
            actual.Should().NotThrow();
            actual2.Should().NotThrow();
        }

        [TestMethod, TestCategory("unit")]
        public void CustomAssert_ShouldAssertInOrderOfInvocation_WithInvoke()
        {
            // Arrange
            string expected1 = "expected1";
            string expected2 = "expected2";
            MockMethodWithParam<string> subject = new MockMethodWithParam<string>("methodName");
            subject.UpdateInvocation();
            subject.Invoke(expected1);
            subject.Invoke(expected2);

            // Act
            Action actual = () => subject.AssertCustom(o => o.Equals(expected1).Should().BeTrue());
            Action actual2 = () => subject.AssertCustom(o => o.Equals(expected2).Should().BeTrue());

            // Assert
            actual.Should().NotThrow();
            actual2.Should().NotThrow();
        }

        [TestMethod, TestCategory("unit")]
        public void Invoke_ShouldNotThrowWhenTrueWithMultipleInvokes()
        {
            // Arrange
            MockMethodWithParam<string> subject = new MockMethodWithParam<string>("methodName");
            subject.UpdateInvocation();

            // Act
            Action actual = () =>
            {
                subject.Invoke("expected");
                subject.Invoke("expected");
                subject.Invoke("expected");
            };

            // Assert
            actual.Should().NotThrow();
        }


        //**** InvokeTask ****

        [TestMethod, TestCategory("unit")]
        public void InvokeTask_ShouldNotThrowWhenTrueWithMultipleInvokes()
        {
            // Arrange
            MockMethodWithParam<string> subject = new MockMethodWithParam<string>("methodName");
            subject.UpdateInvocation();

            // Act
            Func<Task> actual = async () =>
            {
                await subject.InvokeTask("expected");
                await subject.InvokeTask("expected");
                await subject.InvokeTask("expected");
            };

            // Assert
            actual.Should().NotThrow();
        }

        [TestMethod, TestCategory("unit")]
        public async Task AssertInvokedWith_ShouldAssertInOrderOfInvocation_WithInvokeTask()
        {
            // Arrange
            string expected1 = "expected1";
            string expected2 = "expected2";
            MockMethodWithParam<string> subject = new MockMethodWithParam<string>("methodName");
            subject.UpdateInvocation();
            await subject.InvokeTask(expected1);
            await subject.InvokeTask(expected2);

            // Act
            Action actual = () => subject.AssertInvokedWith(expected1);
            Action actual2 = () => subject.AssertInvokedWith(expected2);

            // Assert
            actual.Should().NotThrow();
            actual2.Should().NotThrow();
        }

        [TestMethod, TestCategory("unit")]
        public async Task CustomAssert_ShouldAssertInOrderOfInvocation_WithInvokeTask()
        {
            // Arrange
            string expected1 = "expected1";
            string expected2 = "expected2";
            MockMethodWithParam<string> subject = new MockMethodWithParam<string>("methodName");
            subject.UpdateInvocation();
            await subject.InvokeTask(expected1);
            await subject.InvokeTask(expected2);

            // Act
            Action actual = () => subject.AssertCustom(o => o.Equals(expected1).Should().BeTrue());
            Action actual2 = () => subject.AssertCustom(o => o.Equals(expected2).Should().BeTrue());

            // Assert
            actual.Should().NotThrow();
            actual2.Should().NotThrow();
        }

        [TestMethod, TestCategory("unit")]
        public void InvokeTask_ShouldThrowExceptionWhenUpdateInvocationSetUpForThat()
        {
            // Arrange
            string expected = "expected";
            MockMethodWithParam<string> subject = new MockMethodWithParam<string>("methodName");

            subject.UpdateInvocation(() => throw new Exception("I throw this"));

            // Act
            Func<Task> actual = async () => await subject.InvokeTask(expected);

            // Assert
            actual.Should().ThrowExactly<Exception>().WithMessage("I throw this");
        }

        [TestMethod, TestCategory("unit")]
        public async Task AssertCustom_ShouldThrowWhenFalse_WithInvokeTask()
        {
            // Arrange
            MockMethodWithParam<string> subject = new MockMethodWithParam<string>("methodName");
            subject.UpdateInvocation();
            await subject.InvokeTask("Not expected");

            // Act
            Action actual = () => subject.AssertCustom(o => o.Equals("expected").Should().BeTrue("this is expected to throw"));

            // Assert
            actual.Should().Throw<Exception>();
        }

        [TestMethod, TestCategory("unit")]
        public async Task AssertCustom_ShouldNotThrowWhenTrue_WithInvokeTask()
        {
            // Arrange
            MockMethodWithParam<string> subject = new MockMethodWithParam<string>("methodName");
            subject.UpdateInvocation();
            await subject.InvokeTask("expected");

            // Act
            Action actual = () => subject.AssertCustom(o => o.Equals("expected").Should().BeTrue());

            // Assert
            actual.Should().NotThrow();
        }

        [TestMethod, TestCategory("unit")]
        public void InvokeTask_ShouldThrowExceptionWithMethodNameIfInvocationNotUpdated()
        {
            // Arrange
            string expected = "expected";
            MockMethodWithParam<string> subject = new MockMethodWithParam<string>("methodName");

            // Act
            Func<Task> actual = async () => await subject.InvokeTask(expected);

            // Assert
            actual.Should().ThrowExactly<TestException>().WithMessage("If you want to use methodName, configure via Builder.");
        }

        [TestMethod, TestCategory("unit")]
        public void InvokeTask_ShouldNotThrowExceptionIfInvocationUpdated()
        {
            // Arrange
            string expected = "expected";
            MockMethodWithParam<string> subject = new MockMethodWithParam<string>("methodName");
            subject.UpdateInvocation();

            // Act
            Func<Task> actual = async () => await subject.InvokeTask(expected);

            // Assert
            actual.Should().NotThrow();
        }

        [TestMethod, TestCategory("unit")]
        public async Task AssertInvokedWith_ShouldThrowWhenFalse_WithInvokeTask()
        {
            // Arrange
            MockMethodWithParam<string> subject = new MockMethodWithParam<string>("methodName");
            subject.UpdateInvocation();
            await subject.InvokeTask("Not expected");

            // Act
            Action actual = () => subject.AssertInvokedWith("expected");

            // Assert
            actual.Should().Throw<Exception>().WithMessage("Expected methodName to be invoked with expected but was actually invoked with Not expected");
        }

        [TestMethod, TestCategory("unit")]
        public async Task AssertInvokedWith_ShouldNotThrowWhenTrue_WithInvokeTask()
        {
            // Arrange
            MockMethodWithParam<string> subject = new MockMethodWithParam<string>("methodName");
            subject.UpdateInvocation();
            await subject.InvokeTask("expected");

            // Act
            Action actual = () => subject.AssertInvokedWith("expected");

            // Assert
            actual.Should().NotThrow();
        }

        [TestMethod, TestCategory("unit")]
        public void InvokeTask_ShouldTrackInvocationWithExceptionThrown()
        {
            // Arrange
            MockMethodWithParam<string> subject = new MockMethodWithParam<string>("methodName");
            subject.UpdateInvocation(() => { }, () => throw new Exception("Second Invocation"));

            // Act
            Func<Task> actual = async () => await subject.InvokeTask("");
            Func<Task> thrower = async () => await subject.InvokeTask("");

            // Assert
            actual.Should().NotThrow();
            thrower.Should().ThrowExactly<Exception>().WithMessage("Second Invocation");
            subject.AssertInvokedCountMatches(2);
        }
    }
}