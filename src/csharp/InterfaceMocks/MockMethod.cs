using System;
using System.Threading.Tasks;

namespace InterfaceMocks
{
    public class MockMethod : MockMethodBase
    {
        private readonly IStickyLastList<Action> _lambdas;

        public MockMethod(string name) : base(name) => _lambdas = new StickyLastList<Action>(() => throw new TestException(name));

        public void UpdateInvocation() => UpdateInvocation(() => { });

        public void UpdateInvocation(params Action[] actions) => _lambdas.SetTo(actions);

        public void Invoke()
        {
            _lambdas.Next()();
            InvokedCount++;
        }

        public Task InvokeTask() => Task.Run(() => { Invoke(); });
    }

}