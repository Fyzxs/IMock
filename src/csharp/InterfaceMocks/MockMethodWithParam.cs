using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterfaceMocks
{
    public class MockMethodWithParam<TParam> : MockMethodBase<TParam>, IMockMethodWithParam<TParam>
    {
        private readonly string _name;
        private Action[] _actions;
        private readonly List<TParam> _values = new List<TParam>();
        private int _actionIndex;
        private int _valueIndex;

        public MockMethodWithParam(string name) : base(name)
        {
            _name = name;
            _actions = new Action[] { () => throw new TestException(name) };
        }

        public void UpdateInvocation() => UpdateInvocation(() => { });

        public void UpdateInvocation(params Action[] action) => _actions = action;

        private void ExecuteAction()
        {
            if (_actions.Length == _actionIndex)
            {
                _actions[_actions.Length - 1]();
                return;
            }

            _actions[_actionIndex++]();
        }

        public void Invoke(TParam value)
        {
            _values.Add(value);
            InvokedCount++;
            ExecuteAction();
        }

        public Task InvokeTask(TParam value) => Task.Run(() => Invoke(value));

        private TParam GetValueInOrderOfExecution()
        {
            if (!_values.Any()) AssertInvoked();
            return _values.Count <= _valueIndex
                ? _values[_valueIndex - 1]
                : _values[_valueIndex++];
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