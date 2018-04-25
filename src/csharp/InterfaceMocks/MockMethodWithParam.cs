using InterfaceMocks.Exceptions;
using System;
using System.Threading.Tasks;

namespace InterfaceMocks
{
    /// <summary>
    /// <see cref="T:InterfaceMocks.MockMethodWithParam" /> is used in an interface mock for methods that have arguments and a <see cref="T:System.Void" /> or <see cref="T:System.Threading.Tasks.Task" /> return type.
    /// <example>
    /// This class is used to mock an interface of the signature <code>void MethodName(TParam value)</code>
    /// A full example of how this would be used:
    /// <code>
    /// public interface ISomeInterface{
    ///     void MethodName(string value);
    /// }
    /// public class MockSomeInterface : ISomeInterface{
    /// 
    ///     private MockMethodWithParam&lt;string> _methodName;
    /// 
    ///     private MockTobeMocked(){}
    /// 
    ///     public void MethodName(string value) => _methodName.Invoked(value);
    /// 
    ///     public class Builder{
    ///         private MockMethodWithParam&lt;string> _methodName;= new MockMethodWithParam&lt;string>("MockSomeInterface#MethodName");
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
    public sealed class MockMethodWithParam<TParam> : MockMethodBase, IMockMethodWithParam<TParam>
    {
        private readonly string _name;
        private readonly IStickyLastList<Action> _lambdas;
        private readonly IStickyLastList<TParam> _values;
        private readonly IAsserter _asserter;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockMethodWithParam{TParam}"/>.
        /// </summary>
        /// <param name="name">Name to identify in exception messages</param>
        public MockMethodWithParam(string name) : this(name, new StickyLastList<Action>(() => throw new TestException(name)), new StickyLastList<TParam>(), new Asserter()) { }
        private MockMethodWithParam(string name, IStickyLastList<Action> lambdas, IStickyLastList<TParam> values, IAsserter asserter) : base(name)
        {
            _name = name;
            _lambdas = lambdas;
            _values = values;
            _asserter = asserter;
        }

        /// <inheritdoc />
        public void UpdateInvocation() => UpdateInvocation(() => { });

        /// <inheritdoc />
        public void UpdateInvocation(params Action[] actions) => _lambdas.SetTo(actions);

        /// <inheritdoc />
        public void Invoke(TParam value)
        {
            _values.Add(value);
            MethodInvoked();
            _lambdas.Next()();
        }

        /// <inheritdoc />
        public Task InvokeTask(TParam value) => Task.Run(() => Invoke(value));

        /// <inheritdoc />
        public void AssertCustom(Action<TParam> assertion) => assertion(GetValueInOrderOfExecution());


        /// <inheritdoc />
        public void AssertInvokedWith(TParam expected)
        {
            TParam actual = GetValueInOrderOfExecution();
            _asserter.AssertIf(!actual.Equals(expected), $"Expected {_name} to be invoked with {expected} but was actually invoked with {actual}");
        }

        private TParam GetValueInOrderOfExecution()
        {
            if (_values.IsEmpty()) AssertInvoked();
            return _values.Next();
        }

    }

    /// <summary>
    /// Interface for methods that have arguments <typeparamref name="TParam"/> and a <see cref="T:System.Void" /> or <see cref="T:System.Threading.Tasks.Task" /> return type.
    /// </summary>
    public interface IMockMethodWithParam<TParam>
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
        /// Called when the mocked method is invoked.
        /// </summary>
        /// <param name="value">The value the method was invoked with</param>
        void Invoke(TParam value);
        /// <summary>
        /// Called when the mocked method is invoked async.
        /// </summary>
        /// <param name="value">The value the method was invoked with</param>
        /// <returns>An Awaitable</returns>
        Task InvokeTask(TParam value);
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