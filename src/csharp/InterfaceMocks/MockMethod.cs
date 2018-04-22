using System;
using System.Threading.Tasks;

namespace InterfaceMocks
{
    public class MockMethod : MockMethodBase<Task>
    {
        private Action[] _actions;
        private int _actionIndex;

        public MockMethod(string name) : base(name) => _actions = new Action[] { () => throw new TestException(name) };

        public void UpdateInvocation() => UpdateInvocation(() => { });

        public void UpdateInvocation(params Action[] actions) => _actions = actions;

        public void Invoke()
        {
            InvokedCount++;
            ExecuteAction();
        }

        public Task InvokeTask()
        {
            return Task.Run(() => { Invoke(); });
        }

        private void ExecuteAction()
        {
            if (_actions.Length == _actionIndex)
            {
                _actions[_actions.Length - 1]();
                return;
            }

            _actions[_actionIndex++]();
        }
    }
}