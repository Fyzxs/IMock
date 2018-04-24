using InterfaceMocks.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterfaceMocks
{
    /// <summary>
    /// <see cref="T:InterfaceMocks.MockMethodWithResponse" /> is used in an interface mock for methods that have no arguments and return a non-<see cref="Task"/> type.
    /// </summary>
    public sealed class MockMethodWithResponse<TResponse> : MockMethodBase, IMockMethodWithResponse<TResponse>
    {
        private readonly IStickyLastList<Func<TResponse>> _funcs;

        public MockMethodWithResponse(string name) : base(name) => _funcs = new StickyLastList<Func<TResponse>>(() => throw new TestException(name));

        public void UpdateInvocation(params TResponse[] valuesToReturn) => UpdateInvocation(FuncWrapper(valuesToReturn));

        public void UpdateInvocation(params Func<TResponse>[] funcs) => _funcs.SetTo(funcs);

        public TResponse Invoke()
        {
            MethodInvoked();
            return _funcs.Next()();
        }

        public Task<TResponse> InvokeTask() => Task.FromResult(Invoke());

        private Func<TResponse>[] FuncWrapper(IReadOnlyList<TResponse> valuesToReturn)
        {
            int length = valuesToReturn.Count;
            Func<TResponse>[] funcs = new Func<TResponse>[length];

            for (int index = 0; index < length; index++)
            {
                int scopeIndex = index;
                funcs[index] = () => valuesToReturn[scopeIndex];
            }
            return funcs;
        }
    }

    public interface IMockMethodWithResponse<TResponse>
    {
        void UpdateInvocation(params TResponse[] valuesToReturn);
        void UpdateInvocation(params Func<TResponse>[] funcs);
        TResponse Invoke();
        Task<TResponse> InvokeTask();
    }
}