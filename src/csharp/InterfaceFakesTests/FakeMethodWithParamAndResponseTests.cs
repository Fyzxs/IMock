using FluentAssertions;
using InterfaceFakes;
using InterfaceFakesTests.Fakes;
using InterfaceFakesTests.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace InterfaceFakesTests
{
    [TestClass]
    public sealed class FakeMethodWithParamAndResponseTests
    {
        [TestMethod, TestCategory("unit")]
        public void Invoke_ShouldCallInvokeOnBothFakes_AndReturnTrue()
        {
            // Arrange
            FakeFakeMethodWithParam<string> fakeFakeMethodWithParam = new FakeFakeMethodWithParam<string>.Builder().Invoke().Build();
            FakeFakeMethodWithResponse<bool> fakeFakeMethodWithResponse = new FakeFakeMethodWithResponse<bool>.Builder().Invoke(true).Build();

            FakeMethodWithParamAndResponse<string, bool> subject = new ReflectionObject<FakeMethodWithParamAndResponse<string, bool>>("methodName", fakeFakeMethodWithParam, fakeFakeMethodWithResponse).Object();

            // Act
            bool actual = subject.Invoke("expected");

            // Assert
            actual.Should().BeTrue();
        }
        [TestMethod, TestCategory("unit")]
        public void Invoke_ShouldFlagInvoked_AndNotExcept()
        {
            // Arrange
            FakeFakeMethodWithParam<string> fakeFakeMethodWithParam = new FakeFakeMethodWithParam<string>.Builder().Invoke().Build();
            FakeFakeMethodWithResponse<bool> fakeFakeMethodWithResponse = new FakeFakeMethodWithResponse<bool>.Builder().Invoke(true).Build();

            FakeMethodWithParamAndResponse<string, bool> subject = new ReflectionObject<FakeMethodWithParamAndResponse<string, bool>>("methodName", fakeFakeMethodWithParam, fakeFakeMethodWithResponse).Object();

            // Act
            bool actual = subject.Invoke("expected");

            // Assert
            subject.AssertInvoked();

        }

        [TestMethod, TestCategory("unit")]
        public void Invoke_ShouldCallInvokeOnBothFakes_AndReturnFalse()
        {
            // Arrange
            FakeFakeMethodWithParam<string> fakeFakeMethodWithParam = new FakeFakeMethodWithParam<string>.Builder().Invoke().Build();
            FakeFakeMethodWithResponse<bool> fakeFakeMethodWithResponse = new FakeFakeMethodWithResponse<bool>.Builder().Invoke(false).Build();

            FakeMethodWithParamAndResponse<string, bool> subject = new ReflectionObject<FakeMethodWithParamAndResponse<string, bool>>("methodName", fakeFakeMethodWithParam, fakeFakeMethodWithResponse).Object();

            // Act
            bool actual = subject.Invoke("expected");

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod, TestCategory("unit")]
        public async Task InvokeTask_ShouldCallInvokeOnBothFakes_AndReturnTrue()
        {
            // Arrange
            FakeFakeMethodWithParam<string> fakeFakeMethodWithParam = new FakeFakeMethodWithParam<string>.Builder().Invoke().Build();
            FakeFakeMethodWithResponse<bool> fakeFakeMethodWithResponse = new FakeFakeMethodWithResponse<bool>.Builder().Invoke(true).Build();

            FakeMethodWithParamAndResponse<string, bool> subject = new ReflectionObject<FakeMethodWithParamAndResponse<string, bool>>("methodName", fakeFakeMethodWithParam, fakeFakeMethodWithResponse).Object();

            // Act
            bool actual = await subject.InvokeTask("expected");

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod, TestCategory("unit")]
        public async Task InvokeTask_ShouldCallInvokeOnBothFakes_AndReturnFalse()
        {
            // Arrange
            FakeFakeMethodWithParam<string> fakeFakeMethodWithParam = new FakeFakeMethodWithParam<string>.Builder().Invoke().Build();
            FakeFakeMethodWithResponse<bool> fakeFakeMethodWithResponse = new FakeFakeMethodWithResponse<bool>.Builder().Invoke(false).Build();

            FakeMethodWithParamAndResponse<string, bool> subject = new ReflectionObject<FakeMethodWithParamAndResponse<string, bool>>("methodName", fakeFakeMethodWithParam, fakeFakeMethodWithResponse).Object();

            // Act
            bool actual = await subject.InvokeTask("expected");

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod, TestCategory("unit")]
        public void AssertCustom_ShouldInvokeOnParamFake()
        {
            // Arrange
            FakeFakeMethodWithParam<string> fakeFakeMethodWithParam = new FakeFakeMethodWithParam<string>.Builder().AssertCustom().Build();
            FakeFakeMethodWithResponse<bool> fakeFakeMethodWithResponse = new FakeFakeMethodWithResponse<bool>.Builder().Build();

            FakeMethodWithParamAndResponse<string, bool> subject = new ReflectionObject<FakeMethodWithParamAndResponse<string, bool>>("methodName", fakeFakeMethodWithParam, fakeFakeMethodWithResponse).Object();


            // Act
            subject.AssertCustom(o => o.Should().Be("expected"));

            // Assert
            fakeFakeMethodWithParam.AssertAssertCustomInvoked();
        }

        [TestMethod, TestCategory("unit")]
        public void AssertInvokedWith_ShouldInvokeOnParamFake()
        {
            // Arrange
            FakeFakeMethodWithParam<string> fakeFakeMethodWithParam = new FakeFakeMethodWithParam<string>.Builder().AssertInvokedWith().Build();
            FakeFakeMethodWithResponse<bool> fakeFakeMethodWithResponse = new FakeFakeMethodWithResponse<bool>.Builder().Build();

            FakeMethodWithParamAndResponse<string, bool> subject = new ReflectionObject<FakeMethodWithParamAndResponse<string, bool>>("methodName", fakeFakeMethodWithParam, fakeFakeMethodWithResponse).Object();


            // Act
            subject.AssertInvokedWith("expected");

            // Assert
            fakeFakeMethodWithParam.AssertAssertInvokedWithInvokedWith("expected");
        }

        [TestMethod, TestCategory("unit")]
        public void UpdateInvocation_Param_ShouldInvokeOnFakes()
        {
            // Arrange
            bool[] expected = { true, false };
            FakeFakeMethodWithParam<string> fakeFakeMethodWithParam = new FakeFakeMethodWithParam<string>.Builder().UpdateInvocation().Build();
            FakeFakeMethodWithResponse<bool> fakeFakeMethodWithResponse = new FakeFakeMethodWithResponse<bool>.Builder().UpdateInvocationWithTResponse().Build();

            FakeMethodWithParamAndResponse<string, bool> subject = new ReflectionObject<FakeMethodWithParamAndResponse<string, bool>>("methodName", fakeFakeMethodWithParam, fakeFakeMethodWithResponse).Object();

            // Act
            subject.UpdateInvocation(expected);

            // Assert
            fakeFakeMethodWithParam.AssertUpdateInvocationInvoked();
            fakeFakeMethodWithResponse.AssertUpdateInvocationResponseInvokedWith(expected);
        }

        [TestMethod, TestCategory("unit")]
        public void UpdateInvocation_funcs_ShouldInvokeOnFakes()
        {
            // Arrange
            FakeFakeMethodWithParam<string> fakeFakeMethodWithParam = new FakeFakeMethodWithParam<string>.Builder().UpdateInvocation().Build();
            FakeFakeMethodWithResponse<bool> fakeFakeMethodWithResponse = new FakeFakeMethodWithResponse<bool>.Builder().UpdateInvocationWithFunc().Build();

            FakeMethodWithParamAndResponse<string, bool> subject = new ReflectionObject<FakeMethodWithParamAndResponse<string, bool>>("methodName", fakeFakeMethodWithParam, fakeFakeMethodWithResponse).Object();

            // Act
            subject.UpdateInvocation(() => true);

            // Assert
            fakeFakeMethodWithParam.AssertUpdateInvocationInvoked();
            fakeFakeMethodWithResponse.AssertUpdateInvocationFuncInvoked();
        }

        //todo:Funcational tests which esnures we test the UpdateInvocation + Invoke/InvokeTask temporal pairing that exist in FakeMethodWithParamAndResponse
    }
}