using System;
using System.Threading.Tasks;
using CoreDebugSolution;
using InterfaceFakes;

namespace DebugSolution.Sample
{
    public interface IEventExample<T>
    {
        
        //event EventHandler DoEventing;
        //event Action<T> DoActioning;
        //event Action<bool> ActionSpecfiedType;
        
    }

    public interface IActionExample<T>
    {
        Action<T> ActionGenericResponseActionGeneric(Action<T> input);
        Action<bool> ActionTypeResponseActionType(Action<bool> input);
    }

    public interface IEdgeCases
    {
        string SameNameDifParams(int one);
        string SameNameDifParams(double one);
    }
    public interface IBugNumberNine<T>
    {
        void FuncTaskGeneric(Func<Task<T>> input);
        Task<T> FuncTaskGenericResponseTaskGeneric(Func<Task<T>> input);
    }

    public interface IBugs<T> : IBugNumberNine<T>{}

    public interface IFakeMethod
    {
        void VoidVoid();
        Task ResponseTaskVoid();
    }
    public interface IFakeMethodWithParam<T>
    {
        void ParamType(double justOne);
        void ParamGeneric(T justOne);
        Task ParamTypeResponseTask(double justOne);
        void ParamTuple(string justOne, int singleInt);
        void ParamTypeGeneric(string justOne, T oneT);
    }

    public interface IFakeMethodWithResponse<T>
    {
        T ResponseGeneric();
        string ResponseType();
        Task<T> ResponseTaskGeneric();
        Task<double> ResponseTaskType();
    }

    public interface IFakeMethodWithParamAndResponse<T>
    {
        Task<string> ParamTypeResponseTaskType(int incoming);
        int ParamTupleResponseType(char c, string yeppers, IFooz longer);
    }

    public interface ITheRestOfThings<T> : IBugs<T>, IEdgeCases, IEventExample<T>, IActionExample<T>
    { }
    public interface IFakeMethods<T> : IFakeMethod, IFakeMethodWithParam<T>, IFakeMethodWithResponse<T>, IFakeMethodWithParamAndResponse<T>
    { }

    public interface IFoozzing<T> : IFakeMethods<T>, ITheRestOfThings<T>
    {
    }

    public partial class FakeFoozzing<T> : IFoozzing<T> {
        private FakeMethod _voidVoid;
        private FakeMethod _responseTaskVoid;
        private FakeMethodWithParam<double> _paramType;
        private FakeMethodWithParam<T> _paramGeneric;
        private FakeMethodWithParam<double> _paramTypeResponseTask;
        private FakeMethodWithParam<Tuple<string, int>> _paramTuple;
        private FakeMethodWithParam<Tuple<string, T>> _paramTypeGeneric;
        private FakeMethodWithResponse<T> _responseGeneric;
        private FakeMethodWithResponse<string> _responseType;
        private FakeMethodWithResponse<T> _responseTaskGeneric;
        private FakeMethodWithResponse<double> _responseTaskType;
        private FakeMethodWithParamAndResponse<int, string> _paramTypeResponseTaskType;
        private FakeMethodWithParamAndResponse<Tuple<char, string, IFooz>, int> _paramTupleResponseType;
        private FakeMethodWithParam<Func<Task<T>>> _funcTaskGeneric;
        private FakeMethodWithParamAndResponse<Func<Task<T>>, T> _funcTaskGenericResponseTaskGeneric;
        private FakeMethodWithParamAndResponse<int, string> _sameNameDifParamsInt;
        private FakeMethodWithParamAndResponse<double, string> _sameNameDifParamsDouble;
        private FakeMethodWithParamAndResponse<Action<T>, Action<T>> _actionGenericResponseActionGeneric;
        private FakeMethodWithParamAndResponse<Action<bool>, Action<bool>> _actionTypeResponseActionType;
        private FakeFoozzing() { }
        public void VoidVoid() => _voidVoid.Invoke();
        public Task ResponseTaskVoid() => _responseTaskVoid.InvokeTask();
        public void AssertVoidVoidInvoked() => _voidVoid.AssertInvoked();
        public void AssertResponseTaskVoidInvoked() => _responseTaskVoid.AssertInvoked();
        public void ParamType(double justOne) => _paramType.Invoke(justOne);
        public void ParamGeneric(T justOne) => _paramGeneric.Invoke(justOne);
        public Task ParamTypeResponseTask(double justOne) => _paramTypeResponseTask.InvokeTask(justOne);
        public void ParamTuple(string justOne, int singleInt) => _paramTuple.Invoke(new Tuple<string, int>(justOne, singleInt));
        public void ParamTypeGeneric(string justOne, T oneT) => _paramTypeGeneric.Invoke(new Tuple<string, T>(justOne, oneT));
        public void AssertParamTypeInvokedWith(double justOne) => _paramType.AssertInvokedWith(justOne);
        public void AssertParamGenericInvokedWith(T justOne) => _paramGeneric.AssertInvokedWith(justOne);
        public void AssertParamTypeResponseTaskInvokedWith(double justOne) => _paramTypeResponseTask.AssertInvokedWith(justOne);
        public void AssertParamTupleInvokedWith(string justOne, int singleInt) => _paramTuple.AssertInvokedWith(new Tuple<string, int>(justOne, singleInt));
        public void AssertParamTypeGenericInvokedWith(string justOne, T oneT) => _paramTypeGeneric.AssertInvokedWith(new Tuple<string, T>(justOne, oneT));
        public T ResponseGeneric() => _responseGeneric.Invoke();
        public string ResponseType() => _responseType.Invoke();
        public Task<T> ResponseTaskGeneric() => _responseTaskGeneric.InvokeTask();
        public Task<double> ResponseTaskType() => _responseTaskType.InvokeTask();
        public void AssertResponseGenericInvoked() => _responseGeneric.AssertInvoked();
        public void AssertResponseTypeInvoked() => _responseType.AssertInvoked();
        public void AssertResponseTaskGenericInvoked() => _responseTaskGeneric.AssertInvoked();
        public void AssertResponseTaskTypeInvoked() => _responseTaskType.AssertInvoked();
        public Task<string> ParamTypeResponseTaskType(int incoming) => _paramTypeResponseTaskType.InvokeTask(incoming);
        public int ParamTupleResponseType(char c, string yeppers, IFooz longer) => _paramTupleResponseType.Invoke(new Tuple<char, string, IFooz>(c, yeppers, longer));
        public void AssertParamTypeResponseTaskTypeInvokedWith(int incoming) => _paramTypeResponseTaskType.AssertInvokedWith(incoming);
        public void AssertParamTupleResponseTypeInvokedWith(char c, string yeppers, IFooz longer) => _paramTupleResponseType.AssertInvokedWith(new Tuple<char, string, IFooz>(c, yeppers, longer));
        public void FuncTaskGeneric(Func<Task<T>> input) => _funcTaskGeneric.Invoke(input);
        public Task<T> FuncTaskGenericResponseTaskGeneric(Func<Task<T>> input) => _funcTaskGenericResponseTaskGeneric.InvokeTask(input);
        public void AssertFuncTaskGenericInvokedWith(Func<Task<T>> input) => _funcTaskGeneric.AssertInvokedWith(input);
        public void AssertFuncTaskGenericResponseTaskGenericInvokedWith(Func<Task<T>> input) => _funcTaskGenericResponseTaskGeneric.AssertInvokedWith(input);
        public string SameNameDifParams(int one) => _sameNameDifParamsInt.Invoke(one);
        public string SameNameDifParams(double one) => _sameNameDifParamsDouble.Invoke(one);
        public void AssertSameNameDifParamsIntInvokedWith(int one) => _sameNameDifParamsInt.AssertInvokedWith(one);
        public void AssertSameNameDifParamsDoubleInvokedWith(double one) => _sameNameDifParamsDouble.AssertInvokedWith(one);
        public Action<T> ActionGenericResponseActionGeneric(Action<T> input) => _actionGenericResponseActionGeneric.Invoke(input);
        public Action<bool> ActionTypeResponseActionType(Action<bool> input) => _actionTypeResponseActionType.Invoke(input);
        public void AssertActionGenericResponseActionGenericInvokedWith(Action<T> input) => _actionGenericResponseActionGeneric.AssertInvokedWith(input);
        public void AssertActionTypeResponseActionTypeInvokedWith(Action<bool> input) => _actionTypeResponseActionType.AssertInvokedWith(input);

        public class Builder {
            private readonly FakeMethod _voidVoid = new FakeMethod("FakeFakeMethod#VoidVoid");
            private readonly FakeMethod _responseTaskVoid = new FakeMethod("FakeFakeMethod#ResponseTaskVoid");
            private readonly FakeMethodWithParam<double> _paramType = new FakeMethodWithParam<double>("FakeFakeMethodWithParam#ParamType");
            private readonly FakeMethodWithParam<T> _paramGeneric = new FakeMethodWithParam<T>("FakeFakeMethodWithParam#ParamGeneric");
            private readonly FakeMethodWithParam<double> _paramTypeResponseTask = new FakeMethodWithParam<double>("FakeFakeMethodWithParam#ParamTypeResponseTask");
            private readonly FakeMethodWithParam<Tuple<string, int>> _paramTuple = new FakeMethodWithParam<Tuple<string, int>>("FakeFakeMethodWithParam#ParamTuple");
            private readonly FakeMethodWithParam<Tuple<string, T>> _paramTypeGeneric = new FakeMethodWithParam<Tuple<string, T>>("FakeFakeMethodWithParam#ParamTypeGeneric");
            private readonly FakeMethodWithResponse<T> _responseGeneric = new FakeMethodWithResponse<T>("FakeFakeMethodWithResponse#ResponseGeneric");
            private readonly FakeMethodWithResponse<string> _responseType = new FakeMethodWithResponse<string>("FakeFakeMethodWithResponse#ResponseType");
            private readonly FakeMethodWithResponse<T> _responseTaskGeneric = new FakeMethodWithResponse<T>("FakeFakeMethodWithResponse#ResponseTaskGeneric");
            private readonly FakeMethodWithResponse<double> _responseTaskType = new FakeMethodWithResponse<double>("FakeFakeMethodWithResponse#ResponseTaskType");
            private readonly FakeMethodWithParamAndResponse<int, string> _paramTypeResponseTaskType = new FakeMethodWithParamAndResponse<int, string>("FakeFakeMethodWithParamAndResponse#ParamTypeResponseTaskType");
            private readonly FakeMethodWithParamAndResponse<Tuple<char, string, IFooz>, int> _paramTupleResponseType = new FakeMethodWithParamAndResponse<Tuple<char, string, IFooz>, int>("FakeFakeMethodWithParamAndResponse#ParamTupleResponseType");
            private readonly FakeMethodWithParam<Func<Task<T>>> _funcTaskGeneric = new FakeMethodWithParam<Func<Task<T>>>("FakeBugNumberNine#FuncTaskGeneric");
            private readonly FakeMethodWithParamAndResponse<Func<Task<T>>, T> _funcTaskGenericResponseTaskGeneric = new FakeMethodWithParamAndResponse<Func<Task<T>>, T>("FakeBugNumberNine#FuncTaskGenericResponseTaskGeneric");
            private readonly FakeMethodWithParamAndResponse<int, string> _sameNameDifParamsInt = new FakeMethodWithParamAndResponse<int, string>("FakeEdgeCases#SameNameDifParamsInt");
            private readonly FakeMethodWithParamAndResponse<double, string> _sameNameDifParamsDouble = new FakeMethodWithParamAndResponse<double, string>("FakeEdgeCases#SameNameDifParamsDouble");
            private readonly FakeMethodWithParamAndResponse<Action<T>, Action<T>> _actionGenericResponseActionGeneric = new FakeMethodWithParamAndResponse<Action<T>, Action<T>>("FakeActionExample#ActionGenericResponseActionGeneric");
            private readonly FakeMethodWithParamAndResponse<Action<bool>, Action<bool>> _actionTypeResponseActionType = new FakeMethodWithParamAndResponse<Action<bool>, Action<bool>>("FakeActionExample#ActionTypeResponseActionType");

            public FakeFoozzing<T> Build()
            {
                return new FakeFoozzing<T>
                {
                    _voidVoid = _voidVoid,
                    _responseTaskVoid = _responseTaskVoid,
                    _paramType = _paramType,
                    _paramGeneric = _paramGeneric,
                    _paramTypeResponseTask = _paramTypeResponseTask,
                    _paramTuple = _paramTuple,
                    _paramTypeGeneric = _paramTypeGeneric,
                    _responseGeneric = _responseGeneric,
                    _responseType = _responseType,
                    _responseTaskGeneric = _responseTaskGeneric,
                    _responseTaskType = _responseTaskType,
                    _paramTypeResponseTaskType = _paramTypeResponseTaskType,
                    _paramTupleResponseType = _paramTupleResponseType,
                    _funcTaskGeneric = _funcTaskGeneric,
                    _funcTaskGenericResponseTaskGeneric = _funcTaskGenericResponseTaskGeneric,
                    _sameNameDifParamsInt = _sameNameDifParamsInt,
                    _sameNameDifParamsDouble = _sameNameDifParamsDouble,
                    _actionGenericResponseActionGeneric = _actionGenericResponseActionGeneric,
                    _actionTypeResponseActionType = _actionTypeResponseActionType,
                };
            }

            public Builder VoidVoid()
            {
                _voidVoid.UpdateInvocation();
                return this;
            }

            public Builder VoidVoid(params Action[] actions)
            {
                _voidVoid.UpdateInvocation(actions);
                return this;
            }

            public Builder ResponseTaskVoid()
            {
                _responseTaskVoid.UpdateInvocation();
                return this;
            }

            public Builder ResponseTaskVoid(params Action[] actions)
            {
                _responseTaskVoid.UpdateInvocation(actions);
                return this;
            }

            public Builder ParamType()
            {
                _paramType.UpdateInvocation();
                return this;
            }

            public Builder ParamType(params Action[] actions)
            {
                _paramType.UpdateInvocation(actions);
                return this;
            }

            public Builder ParamGeneric()
            {
                _paramGeneric.UpdateInvocation();
                return this;
            }

            public Builder ParamGeneric(params Action[] actions)
            {
                _paramGeneric.UpdateInvocation(actions);
                return this;
            }

            public Builder ParamTypeResponseTask()
            {
                _paramTypeResponseTask.UpdateInvocation();
                return this;
            }

            public Builder ParamTypeResponseTask(params Action[] actions)
            {
                _paramTypeResponseTask.UpdateInvocation(actions);
                return this;
            }

            public Builder ParamTuple()
            {
                _paramTuple.UpdateInvocation();
                return this;
            }

            public Builder ParamTuple(params Action[] actions)
            {
                _paramTuple.UpdateInvocation(actions);
                return this;
            }

            public Builder ParamTypeGeneric()
            {
                _paramTypeGeneric.UpdateInvocation();
                return this;
            }

            public Builder ParamTypeGeneric(params Action[] actions)
            {
                _paramTypeGeneric.UpdateInvocation(actions);
                return this;
            }

            public Builder ResponseGeneric(params T[] responseValues)
            {
                _responseGeneric.UpdateInvocation(responseValues);
                return this;
            }

            public Builder ResponseGeneric(params Func<T>[] responseValues)
            {
                _responseGeneric.UpdateInvocation(responseValues);
                return this;
            }

            public Builder ResponseType(params string[] responseValues)
            {
                _responseType.UpdateInvocation(responseValues);
                return this;
            }

            public Builder ResponseType(params Func<string>[] responseValues)
            {
                _responseType.UpdateInvocation(responseValues);
                return this;
            }

            public Builder ResponseTaskGeneric(params T[] responseValues)
            {
                _responseTaskGeneric.UpdateInvocation(responseValues);
                return this;
            }

            public Builder ResponseTaskGeneric(params Func<T>[] responseValues)
            {
                _responseTaskGeneric.UpdateInvocation(responseValues);
                return this;
            }

            public Builder ResponseTaskType(params double[] responseValues)
            {
                _responseTaskType.UpdateInvocation(responseValues);
                return this;
            }

            public Builder ResponseTaskType(params Func<double>[] responseValues)
            {
                _responseTaskType.UpdateInvocation(responseValues);
                return this;
            }

            public Builder ParamTypeResponseTaskType(params string[] responseValues)
            {
                _paramTypeResponseTaskType.UpdateInvocation(responseValues);
                return this;
            }

            public Builder ParamTypeResponseTaskType(params Func<string>[] responseValues)
            {
                _paramTypeResponseTaskType.UpdateInvocation(responseValues);
                return this;
            }

            public Builder ParamTupleResponseType(params int[] responseValues)
            {
                _paramTupleResponseType.UpdateInvocation(responseValues);
                return this;
            }

            public Builder ParamTupleResponseType(params Func<int>[] responseValues)
            {
                _paramTupleResponseType.UpdateInvocation(responseValues);
                return this;
            }

            public Builder FuncTaskGeneric()
            {
                _funcTaskGeneric.UpdateInvocation();
                return this;
            }

            public Builder FuncTaskGeneric(params Action[] actions)
            {
                _funcTaskGeneric.UpdateInvocation(actions);
                return this;
            }

            public Builder FuncTaskGenericResponseTaskGeneric(params T[] responseValues)
            {
                _funcTaskGenericResponseTaskGeneric.UpdateInvocation(responseValues);
                return this;
            }

            public Builder FuncTaskGenericResponseTaskGeneric(params Func<T>[] responseValues)
            {
                _funcTaskGenericResponseTaskGeneric.UpdateInvocation(responseValues);
                return this;
            }

            public Builder SameNameDifParamsInt(params string[] responseValues)
            {
                _sameNameDifParamsInt.UpdateInvocation(responseValues);
                return this;
            }

            public Builder SameNameDifParamsInt(params Func<string>[] responseValues)
            {
                _sameNameDifParamsInt.UpdateInvocation(responseValues);
                return this;
            }

            public Builder SameNameDifParamsDouble(params string[] responseValues)
            {
                _sameNameDifParamsDouble.UpdateInvocation(responseValues);
                return this;
            }

            public Builder SameNameDifParamsDouble(params Func<string>[] responseValues)
            {
                _sameNameDifParamsDouble.UpdateInvocation(responseValues);
                return this;
            }

            public Builder ActionGenericResponseActionGeneric(params Action<T>[] responseValues)
            {
                _actionGenericResponseActionGeneric.UpdateInvocation(responseValues);
                return this;
            }

            public Builder ActionGenericResponseActionGeneric(params Func<Action<T>>[] responseValues)
            {
                _actionGenericResponseActionGeneric.UpdateInvocation(responseValues);
                return this;
            }

            public Builder ActionTypeResponseActionType(params Action<bool>[] responseValues)
            {
                _actionTypeResponseActionType.UpdateInvocation(responseValues);
                return this;
            }

            public Builder ActionTypeResponseActionType(params Func<Action<bool>>[] responseValues)
            {
                _actionTypeResponseActionType.UpdateInvocation(responseValues);
                return this;
            }
        }
    }
}