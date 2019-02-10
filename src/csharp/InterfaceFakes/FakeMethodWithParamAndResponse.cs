using System;
using System.Threading.Tasks;

namespace InterfaceFakes
{
    /// <summary>
    /// <see cref="T:InterfaceFakes.FakeMethodWithParamAndResponse" /> is used in an interface fake for methods that have arguments and return a non-<see cref="Task"/> type.
    /// <example>
    /// This class is used to fake an interface of the signature <code>TResponse MethodName(TParam param)</code>
    /// A full example of how this would be used:
    /// <code>
    /// public interface ISomeInterface{
    ///     string MethodName(double value);
    /// }
    /// public class FakeSomeInterface : ISomeInterface{
    /// 
    ///     private FakeMethodWithParamAndResponse&lt;double, string> _methodName;
    /// 
    ///     private FakeTobeFakeed(){}
    /// 
    ///     public string MethodName(double value) => _methodName.Invoked(value);
    /// 
    ///     public class Builder{
    ///         private FakeMethodWithResponse&lt;double, string> _methodName;= new FakeMethodWithResponse&lt;double, string>("FakeSomeInterface#MethodName");
    /// 
    ///         public Builder MethodName(params string[] responseValues){
    ///             _methodName.UpdateInvocation(responseValues);
    ///             return this;
    ///         }
    ///
    ///         public FakeSomeInterface Build{
    ///             return new FakeSomeInterface{
    ///                 _methodName = _methodName
    ///             }
    ///         }
    ///     }
    /// }
    /// </code>
    /// This is the style use successfully in multiple projects. The available ReSharper plugin simplifies creating the Fake Object.
    /// </example>
    /// </summary>
    public sealed class FakeMethodWithParamAndResponse<TParam, TResponse> : FakeMethodBase, IFakeMethodWithParamAndResponse<TParam, TResponse>
    {
        private readonly IFakeMethodWithParam<TParam> _paramMethod;
        private readonly IFakeMethodWithResponse<TResponse> _responseMethod;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeMethodWithParamAndResponse{TParam,TResponse}"/>.
        /// </summary>
        /// <param name="name">Name to identify in exception messages</param>
        public FakeMethodWithParamAndResponse(string name) : this(name, new FakeMethodWithParam<TParam>(name), new FakeMethodWithResponse<TResponse>(name)) { }

        private FakeMethodWithParamAndResponse(string name, IFakeMethodWithParam<TParam> paramMethod, IFakeMethodWithResponse<TResponse> responseMethod) : base(name)
        {
            _paramMethod = paramMethod;
            _responseMethod = responseMethod;
        }

        /// <inheritdoc />
        public void UpdateInvocation(params TResponse[] valuesToReturn)
        {
            _paramMethod.UpdateInvocation();
            _responseMethod.UpdateInvocation(valuesToReturn);
        }

        /// <inheritdoc />
        public void UpdateInvocation(params Func<TResponse>[] responses)
        {
            _paramMethod.UpdateInvocation();
            _responseMethod.UpdateInvocation(responses);
        }

        /// <inheritdoc />
        public TResponse Invoke(TParam value)
        {
            MethodInvoked();
            _paramMethod.Invoke(value);
            return _responseMethod.Invoke();
        }

        /// <inheritdoc />
        public Task<TResponse> InvokeTask(TParam value) => Task.FromResult(Invoke(value));

        /// <inheritdoc />
        public void AssertCustom(Action<TParam> assertion) => _paramMethod.AssertCustom(assertion);

        /// <inheritdoc />
        public void AssertInvokedWith(TParam expected) => _paramMethod.AssertInvokedWith(expected);
    }

    /// <summary>
    /// Interface for methods that have arguments <typeparamref name="TParam"/> and a <typeparamref name="TResponse"/> return type.
    /// </summary>
    public interface IFakeMethodWithParamAndResponse<TParam, TResponse>
    {
        /// <summary>
        /// Updates invoking the method to return the provided values
        /// </summary>
        /// <param name="valuesToReturn">Array of values to return</param>
        void UpdateInvocation(params TResponse[] valuesToReturn);
        /// <summary>
        /// Updates invoking the method to return the result of the funcs.
        /// </summary>
        /// <param name="funcs">Array of values to return</param>
        void UpdateInvocation(params Func<TResponse>[] funcs);
        /// <summary>
        /// Called when the fakeed method is invoked.
        /// </summary>
        /// <param name="value">The value the method was invoked with</param>
        TResponse Invoke(TParam value);
        /// <summary>
        /// Called when the fakeed method is invoked async.
        /// </summary>
        /// <param name="value">The value the method was invoked with</param>
        /// <returns>An Awaitable</returns>
        Task<TResponse> InvokeTask(TParam value);
        /// <summary>
        /// Allows a custom assert to be run against stored arguments.
        /// </summary>
        /// <param name="assertion">The assertion to run against a stored argument.</param>
        void AssertCustom(Action<TParam> assertion);
        /// <summary>
        /// Allows to assert that a specific value was an argument.
        /// </summary>
        /// <param name="expected">The expected argument.</param>
        void AssertInvokedWith(TParam expected);
    }
}