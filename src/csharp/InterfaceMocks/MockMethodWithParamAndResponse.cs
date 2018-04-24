using System;
using System.Threading.Tasks;

namespace InterfaceMocks
{
    /// <summary>
    /// <see cref="T:InterfaceMocks.MockMethodWithParamAndResponse" /> is used in an interface mock for methods that have arguments and return a non-<see cref="Task"/> type.
    /// <example>
    /// This class is used to mock an interface of the signature <code>TResponse MethodName(TParam param)</code>
    /// A full example of how this would be used:
    /// <code>
    /// public interface ISomeInterface{
    ///     string MethodName(double value);
    /// }
    /// public class MockSomeInterface : ISomeInterface{
    /// 
    ///     private MockMethodWithParamAndResponse&lt;double, string> _methodName;
    /// 
    ///     private MockTobeMocked(){}
    /// 
    ///     public string MethodName(double value) => _methodName.Invoked(value);
    /// 
    ///     public class Builder{
    ///         private MockMethodWithResponse&lt;double, string> _methodName;= new MockMethodWithResponse&lt;double, string>("MockSomeInterface#MethodName");
    /// 
    ///         public Builder MethodName(params string[] responseValues){
    ///             _methodName.UpdateInvocation(responseValues);
    ///             return this;
    ///         }
    ///
    ///         public MockSomeInterface Build{
    ///             return new MockSomeInterface{
    ///                 _methodName = _methodName
    ///             }
    ///         }
    ///     }
    /// }
    /// </code>
    /// This is the style use successfully in multiple projects. The available ReSharper plugin simplifies creating the Mock Object.
    /// </example>
    /// </summary>
    public sealed class MockMethodWithParamAndResponse<TParam, TResponse> : MockMethodBase
    {
        private readonly IMockMethodWithParam<TParam> _paramMethod;
        private readonly IMockMethodWithResponse<TResponse> _responseMethod;

        public MockMethodWithParamAndResponse(string name) : this(name, new MockMethodWithParam<TParam>(name), new MockMethodWithResponse<TResponse>(name)) { }

        private MockMethodWithParamAndResponse(string name, IMockMethodWithParam<TParam> paramMethod, IMockMethodWithResponse<TResponse> responseMethod) : base(name)
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