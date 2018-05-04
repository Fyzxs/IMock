using InterfaceMocks.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterfaceMocks
{
    /// <summary>
    /// <see cref="T:InterfaceMocks.MockMethodWithResponse" /> is used in an interface mock for methods that have no arguments and return a non-<see cref="Task"/> type.
    /// <example>
    /// This class is used to mock an interface of the signature <code>TResponse MethodName()</code>
    /// A full example of how this would be used:
    /// <code>
    /// public interface ISomeInterface{
    ///     string MethodName();
    /// }
    /// public class MockSomeInterface : ISomeInterface{
    /// 
    ///     private MockMethodWithResponse&lt;string> _methodName;
    /// 
    ///     private MockTobeMocked(){}
    /// 
    ///     public string MethodName() => _methodName.Invoked();
    /// 
    ///     public class Builder{
    ///         private MockMethodWithResponse&lt;string> _methodName;= new MockMethodWithResponse&lt;string>("MockSomeInterface#MethodName");
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
    public sealed class MockMethodWithResponse<TResponse> : MockMethodBase, IMockMethodWithResponse<TResponse>
    {
        private readonly IStickyLastList<Func<TResponse>> _funcs;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockMethodWithParam{TParam}"/>.
        /// </summary>
        /// <param name="name">Name to identify in exception messages</param>
        public MockMethodWithResponse(string name) : base(name) => _funcs = new StickyLastList<Func<TResponse>>(() => throw new TestException(name));

        /// <inheritdoc />
        public void UpdateInvocation(params TResponse[] valuesToReturn) => UpdateInvocation(FuncWrapper(valuesToReturn));

        /// <inheritdoc />
        public void UpdateInvocation(params Func<TResponse>[] funcs) => _funcs.SetTo(funcs);

        /// <inheritdoc />
        public TResponse Invoke()
        {
            MethodInvoked();
            return _funcs.Next()();
        }

        /// <inheritdoc />
        public Task<TResponse> InvokeTask() => Task.FromResult(Invoke());

        private Func<TResponse>[] FuncWrapper(IReadOnlyList<TResponse> valuesToReturn)
        {
            int length = valuesToReturn.Count;
            Func<TResponse>[] funcs = new Func<TResponse>[length];

            for (int index = 0; index < length; index++)
            {
                int scopeIndex = index;
                funcs[index] = () => valuesToReturn[scopeIndex];
            }
            return funcs;
        }
    }

    /// <summary>
    /// Interface for methods that have no arguments and a return type <typeparamref name="TResponse"/>.
    /// </summary>
    public interface IMockMethodWithResponse<TResponse>
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
        /// Called when the mocked method is invoked.
        /// </summary>
        /// <returns>The value specified from <see cref="UpdateInvocation(TResponse[])"/> or  <see cref="UpdateInvocation(Func&lt;TResponse>[])"/></returns>
        TResponse Invoke();
        /// <summary>
        /// Called when the mocked method is invoked async.
        /// </summary>
        /// <returns>The value specified from <see cref="UpdateInvocation(TResponse[])"/> or  <see cref="UpdateInvocation(Func&lt;TResponse>[])"/> as an awaitable</returns>
        Task<TResponse> InvokeTask();
    }
}