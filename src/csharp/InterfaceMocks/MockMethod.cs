using InterfaceMocks.Exceptions;
using System;
using System.Threading.Tasks;

namespace InterfaceMocks
{
    /// <summary>
    /// <see cref="MockMethod"/> is used in an interface mock for methods that have no arguments and a <see cref="Void"/> or <see cref="Task"/> return type.
    /// </summary>
    public sealed class MockMethod : MockMethodBase
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