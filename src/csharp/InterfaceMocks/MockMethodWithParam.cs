using System;
using System.Threading.Tasks;

namespace InterfaceMocks
{
    public class MockMethodWithParam<TParam> : MockMethodBase, IMockMethodWithParam<TParam>
    {
        private readonly string _name;
        private readonly IStickyLastList<Action> _lambdas;
        private readonly IStickyLastList<TParam> _values;

        public MockMethodWithParam(string name) : this(name, new StickyLastList<Action>(() => throw new TestException(name)), new StickyLastList<TParam>()) { }
        public MockMethodWithParam(string name, IStickyLastList<Action> lambdas, IStickyLastList<TParam> values) : base(name)
        {
            _name = name;
            _lambdas = lambdas;
            _values = values;
        }

        public void UpdateInvocation() => UpdateInvocation(() => { });

        public void UpdateInvocation(params Action[] action) => _lambdas.SetTo(action);


        public void Invoke(TParam value)
        {
            _values.Add(value);
            InvokedCount++;
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
            AssertIf(actual.Equals(expected), $"Expected {_name} to be invoked with {expected} but was actually invoked with {actual}");
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