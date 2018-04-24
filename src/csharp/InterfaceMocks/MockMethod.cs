using InterfaceMocks.Exceptions;
using System;
using System.Threading.Tasks;

namespace InterfaceMocks
{
    /// <summary>
    /// <see cref="T:InterfaceMocks.MockMethod" /> is used in an interface mock for methods that have no arguments and a <see cref="T:System.Void" /> or <see cref="T:System.Threading.Tasks.Task" /> return type.
    /// <example>
    /// This class is used to mock an interface of the signature <code>void MethodName()</code>
    /// A full example of how this would be used:
    /// <code>
    /// public interface ISomeInterface{
    ///     void MethodName();
    /// }
    /// public class MockSomeInterface : ISomeInterface{
    /// 
    ///     private MockMethod _methodName;
    /// 
    ///     private MockTobeMocked(){}
    /// 
    ///     public void MethodName() => _methodName.Invoked();
    /// 
    ///     public class Builder{
    /// 
    ///         private MockMethod _methodName = new MockMethod("MockSomeInterface#MethodName");
    /// 
    ///         public Builder MethodName(){
    ///             _methodName.UpdateInvocation();
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
    public sealed class MockMethod : MockMethodBase
    {
        private readonly IStickyLastList<Action> _lambdas;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockMethod"/> class.
        /// </summary>
        /// <param name="name"></param>
        public MockMethod(string name) : base(name) => _lambdas = new StickyLastList<Action>(() => throw new TestException(name));

        /// <summary>
        /// Updates invoking the method to not throw an exception.
        /// </summary>
        public void UpdateInvocation() => UpdateInvocation(() => { });

        /// <summary>
        /// Updates invoking the method to perform the action on subsequent calls.
        /// </summary>
        /// <param name="actions">The actions to perform on subsequent method calls.</param>
        public void UpdateInvocation(params Action[] actions) => _lambdas.SetTo(actions);

        /// <summary>
        /// Called when the mocked method is invoked.
        /// </summary>
        public void Invoke()
        {
            _lambdas.Next()();
            MethodInvoked();
        }

        /// <summary>
        /// Called when the mocked method is invoked async.
        /// </summary>
        /// <returns>An Awaitable</returns>
        public Task InvokeTask() => Task.Run(() => { Invoke(); });
    }

}