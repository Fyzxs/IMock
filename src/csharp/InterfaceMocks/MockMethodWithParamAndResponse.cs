using System;
using System.Threading.Tasks;

namespace InterfaceMocks
{
    public class MockMethodWithParamAndResponse<TParam, TResponse> : MockMethodBase<TResponse>
    {
        private readonly IMockMethodWithParam<TParam> _paramMethod;
        private readonly IMockMethodWithResponse<TResponse> _responseMethod;

        public MockMethodWithParamAndResponse(string name) : this(name, new MockMethodWithParam<TParam>(name), new MockMethodWithResponse<TResponse>(name)) { }

        public MockMethodWithParamAndResponse(string name, IMockMethodWithParam<TParam> paramMethod, IMockMethodWithResponse<TResponse> responseMethod) : base(name)
        {
            _paramMethod = paramMethod;
            _responseMethod = responseMethod;
        }

        public void UpdateInvocation(params TResponse[] valuesToReturn)
        {
            _paramMethod.UpdateInvocation();
            _responseMethod.UpdateInvocation(valuesToReturn);
        }

        public void UpdateInvocation(params Func<TResponse>[] responses)
        {
            _paramMethod.UpdateInvocation();
            _responseMethod.UpdateInvocation(responses);
        }

        public TResponse Invoke(TParam value)
        {
            _paramMethod.Invoke(value);
            return _responseMethod.Invoke();
        }

        public Task<TResponse> InvokeTask(TParam value) => Task.FromResult(Invoke(value));

        public void AssertCustom(Action<TParam> assertion) => _paramMethod.AssertCustom(assertion);

        public void AssertInvokedWith(TParam expected) => _paramMethod.AssertInvokedWith(expected);
    }
}