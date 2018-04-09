using System;
using System.Threading.Tasks;

namespace InterfaceMocks
{
    public class MockMethod : MockMethodBase
    {
        private Action _action;

        public MockMethod(string name) : base(name) => _action = () => throw new TestException(name);

        public void UpdateInvocation() => _action = () => { };

        public void Invoke()
        {
            InvokedCount++;
            _action();
        }

        public Task InvokeTask()
        {
            InvokedCount++;
            _action();
            return Task.Run(() => { });
        }
    }
}