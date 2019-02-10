using InterfaceFakes.Exceptions;
using System;
using System.Threading.Tasks;

namespace InterfaceFakes
{
    /// <summary>
    /// <see cref="T:InterfaceFakes.FakeMethod" /> is used in an interface fake for methods that have no arguments and a <see cref="T:System.Void" /> or <see cref="T:System.Threading.Tasks.Task" /> return type.
    /// <example>
    /// This class is used to fake an interface of the signature <code>void MethodName()</code>
    /// A full example of how this would be used:
    /// <code>
    /// public interface ISomeInterface{
    ///     void MethodName();
    /// }
    /// public class FakeSomeInterface : ISomeInterface{
    /// 
    ///     private FakeMethod _methodName;
    /// 
    ///     private FakeTobeFakeed(){}
    /// 
    ///     public void MethodName() => _methodName.Invoked();
    /// 
    ///     public class Builder{
    /// 
    ///         private FakeMethod _methodName = new FakeMethod("FakeSomeInterface#MethodName");
    /// 
    ///         public Builder MethodName(){
    ///             _methodName.UpdateInvocation();
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
    public sealed class FakeMethod : FakeMethodBase, IFakeMethod
    {
        private readonly IStickyLastList<Action> _lambdas;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeMethod"/> class.
        /// </summary>
        /// <param name="name"></param>
        public FakeMethod(string name) : base(name) => _lambdas = new StickyLastList<Action>(() => throw new TestException(name));


        ///<inheritdoc/>
        public void UpdateInvocation() => UpdateInvocation(() => { });

        ///<inheritdoc/>
        public void UpdateInvocation(params Action[] actions) => _lambdas.SetTo(actions);

        ///<inheritdoc/>
        public void Invoke()
        {
            MethodInvoked();
            _lambdas.Next()();
        }

        ///<inheritdoc/>
        public Task InvokeTask() => Task.Run(() => { Invoke(); });
    }

    /// <summary>
    /// Interface for methods that have no arguments and a <see cref="T:System.Void" /> or <see cref="T:System.Threading.Tasks.Task" /> return type.
    /// </summary>
    public interface IFakeMethod
    {
        /// <summary>
        /// Updates invoking the method to not throw an exception.
        /// </summary>
        void UpdateInvocation();

        /// <summary>
        /// Updates invoking the method to perform the action on subsequent calls.
        /// </summary>
        /// <param name="actions">The actions to perform on subsequent method calls.</param>
        void UpdateInvocation(params Action[] actions);

        /// <summary>
        /// Called when the fakeed method is invoked.
        /// </summary>
        void Invoke();

        /// <summary>
        /// Called when the fakeed method is invoked async.
        /// </summary>
        /// <returns>An Awaitable</returns>
        Task InvokeTask();
    }
}