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

        public MockMethodWithParam(string name) : this(name, new StickyLastList<Action>(() => throw new TestException(name)), new StickyLastList<TParam>(), new Asserter()) { }
        private MockMethodWithParam(string name, IStickyLastList<Action> lambdas, IStickyLastList<TParam> values, IAsserter asserter) : base(name)
        {
            _name = name;
            _lambdas = lambdas;
            _values = values;
            _asserter = asserter;
        }

        public void UpdateInvocation() => UpdateInvocation(() => { });

        public void UpdateInvocation(params Action[] action) => _lambdas.SetTo(action);


        public void Invoke(TParam value)
        {
            _values.Add(value);
            MethodInvoked();
            _lambdas.Next()();
        }

        public Task InvokeTask(TParam value) => Task.Run(() => Invoke(value));

        private TParam GetValueInOrderOfExecution()
        {
            if (_values.IsEmpty()) AssertInvoked();
            return _values.Next();
        }

        public void AssertCustom(Action<TParam> assertion) => assertion(GetValueInOrderOfExecution());

        public void AssertInvokedWith(TParam expected)
        {
            TParam actual = GetValueInOrderOfExecution();
            _asserter.AssertIf(!actual.Equals(expected), $"Expected {_name} to be invoked with {expected} but was actually invoked with {actual}");
        }
    }

    public interface IMockMethodWithParam<TParam>
    {
        void UpdateInvocation();
        void UpdateInvocation(params Action[] action);
        void Invoke(TParam value);
        Task InvokeTask(TParam value);
        void AssertCustom(Action<TParam> assertion);
        void AssertInvokedWith(TParam expected);
    }
}