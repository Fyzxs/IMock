using InterfaceMocks;
using System;
using System.Threading.Tasks;

namespace InterfaceMocksTests.Mocks
{
    public sealed partial class MockMockMethodWithResponse<TResponse> : IMockMethodWithResponse<TResponse>
    {
        public sealed class Builder
        {
            private readonly MockMethodWithParam<TResponse[]> _updateInvocationTResponseItem = new MockMethodWithParam<TResponse[]>("MockMockMethodWithResponse#UpdateInvocation_TResponse");
            private readonly MockMethodWithParam<Func<TResponse>[]> _updateInvocationFuncItem = new MockMethodWithParam<Func<TResponse>[]>("MockMockMethodWithResponse#UpdateInvocation_Func");
            private readonly MockMethodWithResponse<TResponse> _invokeItem = new MockMethodWithResponse<TResponse>("MockMockMethodWithResponse#Invoke");
            private readonly MockMethodWithResponse<TResponse> _invokeTaskItem = new MockMethodWithResponse<TResponse>("MockMockMethodWithResponse#InvokeTask");

            public Builder InvokeTask(TResponse response)
            {
                _invokeTaskItem.UpdateInvocation(response);
                return this;
            }

            public Builder Invoke(TResponse response)
            {
                _invokeItem.UpdateInvocation(response);
                return this;
            }
            public Builder UpdateInvocationWithTResponse()
            {
                _updateInvocationTResponseItem.UpdateInvocation();
                return this;
            }
            public Builder UpdateInvocationWithFunc()
            {
                _updateInvocationFuncItem.UpdateInvocation();
                return this;
            }
            public MockMockMethodWithResponse<TResponse> Build()
            {
                return new MockMockMethodWithResponse<TResponse>
                {
                    _updateInvocationTResponse = _updateInvocationTResponseItem,
                    _updateInvocationFunc = _updateInvocationFuncItem,
                    _invoke = _invokeItem,
                    _invokeTask = _invokeTaskItem

                };
            }
        }

        private MockMethodWithParam<TResponse[]> _updateInvocationTResponse;
        private MockMethodWithParam<Func<TResponse>[]> _updateInvocationFunc;
        private MockMethodWithResponse<TResponse> _invokeTask;
        private MockMethodWithResponse<TResponse> _invoke;

        private MockMockMethodWithResponse() { }

        public void UpdateInvocation(params TResponse[] valuesToReturn) => _updateInvocationTResponse.Invoke(valuesToReturn);

        public void UpdateInvocation(params Func<TResponse>[] funcs) => _updateInvocationFunc.Invoke(funcs);

        public TResponse Invoke() => _invoke.Invoke();

        public Task<TResponse> InvokeTask() => _invokeTask.InvokeTask();

        public void AssertUpdateInvocationResponseInvokedWith(params TResponse[] expected) => _updateInvocationTResponse.AssertInvokedWith(expected);

        public void AssertUpdateInvocationFuncInvoked() => _updateInvocationFunc.AssertInvoked();
    }
}