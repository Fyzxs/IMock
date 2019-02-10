using InterfaceFakes;
using System;
using System.Threading.Tasks;

namespace InterfaceFakesTests.Fakes
{
    public sealed partial class FakeFakeMethodWithResponse<TResponse> : IFakeMethodWithResponse<TResponse>
    {
        public class Builder
        {
            private readonly FakeMethodWithParam<TResponse[]> _updateInvocationTResponseItem = new FakeMethodWithParam<TResponse[]>("FakeFakeMethodWithResponse#UpdateInvocation_TResponse");
            private readonly FakeMethodWithParam<Func<TResponse>[]> _updateInvocationFuncItem = new FakeMethodWithParam<Func<TResponse>[]>("FakeFakeMethodWithResponse#UpdateInvocation_Func");
            private readonly FakeMethodWithResponse<TResponse> _invokeItem = new FakeMethodWithResponse<TResponse>("FakeFakeMethodWithResponse#Invoke");
            private readonly FakeMethodWithResponse<TResponse> _invokeTaskItem = new FakeMethodWithResponse<TResponse>("FakeFakeMethodWithResponse#InvokeTask");

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
            public FakeFakeMethodWithResponse<TResponse> Build()
            {
                return new FakeFakeMethodWithResponse<TResponse>
                {
                    _updateInvocationTResponse = _updateInvocationTResponseItem,
                    _updateInvocationFunc = _updateInvocationFuncItem,
                    _invoke = _invokeItem,
                    _invokeTask = _invokeTaskItem

                };
            }
        }

        private FakeMethodWithParam<TResponse[]> _updateInvocationTResponse;
        private FakeMethodWithParam<Func<TResponse>[]> _updateInvocationFunc;
        private FakeMethodWithResponse<TResponse> _invokeTask;
        private FakeMethodWithResponse<TResponse> _invoke;

        private FakeFakeMethodWithResponse() { }

        public void UpdateInvocation(params TResponse[] valuesToReturn) => _updateInvocationTResponse.Invoke(valuesToReturn);

        public void UpdateInvocation(params Func<TResponse>[] funcs) => _updateInvocationFunc.Invoke(funcs);

        public TResponse Invoke() => _invoke.Invoke();

        public Task<TResponse> InvokeTask() => _invokeTask.InvokeTask();

        public void AssertUpdateInvocationResponseInvokedWith(params TResponse[] expected) => _updateInvocationTResponse.AssertInvokedWith(expected);

        public void AssertUpdateInvocationFuncInvoked() => _updateInvocationFunc.AssertInvoked();
    }
}