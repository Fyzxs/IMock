using System;
using System.Threading.Tasks;

namespace InterfaceMocks
{
    public class MockMethodWithResponse<TResponse> : MockMethodBase, IMockMethodWithResponse<TResponse>
    {
        private Func<TResponse>[] _funcs;
        private int _funcIndex;

        public MockMethodWithResponse(string name) : base(name) => _funcs = new Func<TResponse>[] { () => throw new TestException(name) };

        public void UpdateInvocation(params TResponse[] valuesToReturn) => UpdateInvocation(FuncWrapper(valuesToReturn));

        private static Func<TResponse>[] FuncWrapper(TResponse[] valuesToReturn)
        {
            int length = valuesToReturn.Length;
            Func<TResponse>[] funcs = new Func<TResponse>[length];

            for (int index = 0; index < length; index++)
            {
                int scopeIndex = index;
                funcs[index] = () => valuesToReturn[scopeIndex];
            }
            return funcs;
        }

        private TResponse ExecuteFunc()
        {
            int length = _funcs.Length;
            if (length <= _funcIndex) return _funcs[length - 1]();

            return _funcs[_funcIndex++]();
        }

        public void UpdateInvocation(params Func<TResponse>[] funcs) => _funcs = funcs;

        public TResponse Invoke()
        {
            InvokedCount++;
            return ExecuteFunc();
        }

        public Task<TResponse> InvokeTask() => Task.FromResult(Invoke());
    }

    public interface IMockMethodWithResponse<TResponse>
    {
        void UpdateInvocation(params TResponse[] valuesToReturn);
        void UpdateInvocation(params Func<TResponse>[] funcs);
        TResponse Invoke();
        Task<TResponse> InvokeTask();
    }
}