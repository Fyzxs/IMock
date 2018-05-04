using FluentAssertions;
using InterfaceMocks;
using InterfaceMocksTests.Mocks;
using InterfaceMocksTests.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace InterfaceMocksTests
{
    [TestClass]
    public class MockMethodWithParamAndResponseTests
    {
        [TestMethod, TestCategory("unit")]
        public void Invoke_ShouldCallInvokeOnBothMocks_AndReturnTrue()
        {
            // Arrange
            MockMockMethodWithParam<string> mockMockMethodWithParam = new MockMockMethodWithParam<string>.Builder().Invoke().Build();
            MockMockMethodWithResponse<bool> mockMockMethodWithResponse = new MockMockMethodWithResponse<bool>.Builder().Invoke(true).Build();

            MockMethodWithParamAndResponse<string, bool> subject = new ReflectionObject<MockMethodWithParamAndResponse<string, bool>>("methodName", mockMockMethodWithParam, mockMockMethodWithResponse).Object();

            // Act
            bool actual = subject.Invoke("expected");

            // Assert
            actual.Should().BeTrue();
        }
        [TestMethod, TestCategory("unit")]
        public void Invoke_ShouldFlagInvoked_AndNotExcept()
        {
            // Arrange
            MockMockMethodWithParam<string> mockMockMethodWithParam = new MockMockMethodWithParam<string>.Builder().Invoke().Build();
            MockMockMethodWithResponse<bool> mockMockMethodWithResponse = new MockMockMethodWithResponse<bool>.Builder().Invoke(true).Build();

            MockMethodWithParamAndResponse<string, bool> subject = new ReflectionObject<MockMethodWithParamAndResponse<string, bool>>("methodName", mockMockMethodWithParam, mockMockMethodWithResponse).Object();

            // Act
            bool actual = subject.Invoke("expected");

            // Assert
            subject.AssertInvoked();

        }

        [TestMethod, TestCategory("unit")]
        public void Invoke_ShouldCallInvokeOnBothMocks_AndReturnFalse()
        {
            // Arrange
            MockMockMethodWithParam<string> mockMockMethodWithParam = new MockMockMethodWithParam<string>.Builder().Invoke().Build();
            MockMockMethodWithResponse<bool> mockMockMethodWithResponse = new MockMockMethodWithResponse<bool>.Builder().Invoke(false).Build();

            MockMethodWithParamAndResponse<string, bool> subject = new ReflectionObject<MockMethodWithParamAndResponse<string, bool>>("methodName", mockMockMethodWithParam, mockMockMethodWithResponse).Object();

            // Act
            bool actual = subject.Invoke("expected");

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod, TestCategory("unit")]
        public async Task InvokeTask_ShouldCallInvokeOnBothMocks_AndReturnTrue()
        {
            // Arrange
            MockMockMethodWithParam<string> mockMockMethodWithParam = new MockMockMethodWithParam<string>.Builder().Invoke().Build();
            MockMockMethodWithResponse<bool> mockMockMethodWithResponse = new MockMockMethodWithResponse<bool>.Builder().Invoke(true).Build();

            MockMethodWithParamAndResponse<string, bool> subject = new ReflectionObject<MockMethodWithParamAndResponse<string, bool>>("methodName", mockMockMethodWithParam, mockMockMethodWithResponse).Object();

            // Act
            bool actual = await subject.InvokeTask("expected");

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod, TestCategory("unit")]
        public async Task InvokeTask_ShouldCallInvokeOnBothMocks_AndReturnFalse()
        {
            // Arrange
            MockMockMethodWithParam<string> mockMockMethodWithParam = new MockMockMethodWithParam<string>.Builder().Invoke().Build();
            MockMockMethodWithResponse<bool> mockMockMethodWithResponse = new MockMockMethodWithResponse<bool>.Builder().Invoke(false).Build();

            MockMethodWithParamAndResponse<string, bool> subject = new ReflectionObject<MockMethodWithParamAndResponse<string, bool>>("methodName", mockMockMethodWithParam, mockMockMethodWithResponse).Object();

            // Act
            bool actual = await subject.InvokeTask("expected");

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod, TestCategory("unit")]
        public void AssertCustom_ShouldInvokeOnParamMock()
        {
            // Arrange
            MockMockMethodWithParam<string> mockMockMethodWithParam = new MockMockMethodWithParam<string>.Builder().AssertCustom().Build();
            MockMockMethodWithResponse<bool> mockMockMethodWithResponse = new MockMockMethodWithResponse<bool>.Builder().Build();

            MockMethodWithParamAndResponse<string, bool> subject = new ReflectionObject<MockMethodWithParamAndResponse<string, bool>>("methodName", mockMockMethodWithParam, mockMockMethodWithResponse).Object();


            // Act
            subject.AssertCustom(o => o.Should().Be("expected"));

            // Assert
            mockMockMethodWithParam.AssertAssertCustomInvoked();
        }

        [TestMethod, TestCategory("unit")]
        public void AssertInvokedWith_ShouldInvokeOnParamMock()
        {
            // Arrange
            MockMockMethodWithParam<string> mockMockMethodWithParam = new MockMockMethodWithParam<string>.Builder().AssertInvokedWith().Build();
            MockMockMethodWithResponse<bool> mockMockMethodWithResponse = new MockMockMethodWithResponse<bool>.Builder().Build();

            MockMethodWithParamAndResponse<string, bool> subject = new ReflectionObject<MockMethodWithParamAndResponse<string, bool>>("methodName", mockMockMethodWithParam, mockMockMethodWithResponse).Object();


            // Act
            subject.AssertInvokedWith("expected");

            // Assert
            mockMockMethodWithParam.AssertAssertInvokedWithInvokedWith("expected");
        }

        [TestMethod, TestCategory("unit")]
        public void UpdateInvocation_Param_ShouldInvokeOnMocks()
        {
            // Arrange
            bool[] expected = { true, false };
            MockMockMethodWithParam<string> mockMockMethodWithParam = new MockMockMethodWithParam<string>.Builder().UpdateInvocation().Build();
            MockMockMethodWithResponse<bool> mockMockMethodWithResponse = new MockMockMethodWithResponse<bool>.Builder().UpdateInvocationWithTResponse().Build();

            MockMethodWithParamAndResponse<string, bool> subject = new ReflectionObject<MockMethodWithParamAndResponse<string, bool>>("methodName", mockMockMethodWithParam, mockMockMethodWithResponse).Object();

            // Act
            subject.UpdateInvocation(expected);

            // Assert
            mockMockMethodWithParam.AssertUpdateInvocationInvoked();
            mockMockMethodWithResponse.AssertUpdateInvocationResponseInvokedWith(expected);
        }

        [TestMethod, TestCategory("unit")]
        public void UpdateInvocation_funcs_ShouldInvokeOnMocks()
        {
            // Arrange
            MockMockMethodWithParam<string> mockMockMethodWithParam = new MockMockMethodWithParam<string>.Builder().UpdateInvocation().Build();
            MockMockMethodWithResponse<bool> mockMockMethodWithResponse = new MockMockMethodWithResponse<bool>.Builder().UpdateInvocationWithFunc().Build();

            MockMethodWithParamAndResponse<string, bool> subject = new ReflectionObject<MockMethodWithParamAndResponse<string, bool>>("methodName", mockMockMethodWithParam, mockMockMethodWithResponse).Object();

            // Act
            subject.UpdateInvocation(() => true);

            // Assert
            mockMockMethodWithParam.AssertUpdateInvocationInvoked();
            mockMockMethodWithResponse.AssertUpdateInvocationFuncInvoked();
        }


        //todo:Funcational tests which esnures we test the UpdateInvocation + Invoke/InvokeTask temporal pairing that exist in MockMethodWithParamAndResponse
    }
}