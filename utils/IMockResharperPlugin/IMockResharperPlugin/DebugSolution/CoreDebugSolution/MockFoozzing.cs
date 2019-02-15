using System;
using System.Threading.Tasks;
using InterfaceMocks;

namespace CoreDebugSolution
{
    public interface IExampleType { }

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

    public interface IMockMethod
    {
        void VoidVoid();
        Task ResponseTaskVoid();
    }
    public interface IMockMethodWithParam<T>
    {
        void ParamType(double justOne);
        void ParamGeneric(T justOne);
        Task ParamTypeResponseTask(double justOne);
        void ParamTuple(string justOne, int singleInt);
        void ParamTypeGeneric(string justOne, T oneT);
    }

    public interface IMockMethodWithResponse<T>
    {
        T ResponseGeneric();
        string ResponseType();
        Task<T> ResponseTaskGeneric();
        Task<double> ResponseTaskType();
    }

    public interface IMockMethodWithParamAndResponse<T>
    {
        Task<string> ParamTypeResponseTaskType(int incoming);
        int ParamTupleResponseType(char c, string yeppers, IExampleType longer);
    }

    public interface ITheRestOfThings<T> : IBugs<T>, IEdgeCases, IEventExample<T>, IActionExample<T>
    { }
    public interface IMockMethods<T> : IMockMethod, IMockMethodWithParam<T>, IMockMethodWithResponse<T>, IMockMethodWithParamAndResponse<T>
    { }

    public interface IFromMe<T> : IMockMethods<T>, ITheRestOfThings<T>
    {
        void NeedsMe();
    }

    public partial class MockFromMe<T> : IFromMe<T> {
        private MockMethod _voidVoid;
        private MockMethod _responseTaskVoid;
        private MockMethodWithParam<double> _paramType;
        private MockMethodWithParam<T> _paramGeneric;
        private MockMethodWithParam<double> _paramTypeResponseTask;
        private MockMethodWithParam<Tuple<string, int>> _paramTuple;
        private MockMethodWithParam<Tuple<string, T>> _paramTypeGeneric;
        private MockMethodWithResponse<T> _responseGeneric;
        private MockMethodWithResponse<string> _responseType;
        private MockMethodWithResponse<T> _responseTaskGeneric;
        private MockMethodWithResponse<double> _responseTaskType;
        private MockMethodWithParamAndResponse<int, string> _paramTypeResponseTaskType;
        private MockMethodWithParamAndResponse<Tuple<char, string, IExampleType>, int> _paramTupleResponseType;
        private MockMethodWithParam<Func<Task<T>>> _funcTaskGeneric;
        private MockMethodWithParamAndResponse<Func<Task<T>>, T> _funcTaskGenericResponseTaskGeneric;
        private MockMethodWithParamAndResponse<int, string> _sameNameDifParamsInt;
        private MockMethodWithParamAndResponse<double, string> _sameNameDifParamsDouble;
        private MockMethodWithParamAndResponse<Action<T>, Action<T>> _actionGenericResponseActionGeneric;
        private MockMethodWithParamAndResponse<Action<bool>, Action<bool>> _actionTypeResponseActionType;
        private MockMethod _needsMe;
        private MockFromMe() { }
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
        public int ParamTupleResponseType(char c, string yeppers, IExampleType longer) => _paramTupleResponseType.Invoke(new Tuple<char, string, IExampleType>(c, yeppers, longer));
        public void AssertParamTypeResponseTaskTypeInvokedWith(int incoming) => _paramTypeResponseTaskType.AssertInvokedWith(incoming);
        public void AssertParamTupleResponseTypeInvokedWith(char c, string yeppers, IExampleType longer) => _paramTupleResponseType.AssertInvokedWith(new Tuple<char, string, IExampleType>(c, yeppers, longer));
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
        public void NeedsMe() => _needsMe.Invoke();
        public void AssertNeedsMeInvoked() => _needsMe.AssertInvoked();

        public class Builder {
            private readonly MockMethod _voidVoid = new MockMethod("MockMockMethod#VoidVoid");
            private readonly MockMethod _responseTaskVoid = new MockMethod("MockMockMethod#ResponseTaskVoid");
            private readonly MockMethodWithParam<double> _paramType = new MockMethodWithParam<double>("MockMockMethodWithParam#ParamType");
            private readonly MockMethodWithParam<T> _paramGeneric = new MockMethodWithParam<T>("MockMockMethodWithParam#ParamGeneric");
            private readonly MockMethodWithParam<double> _paramTypeResponseTask = new MockMethodWithParam<double>("MockMockMethodWithParam#ParamTypeResponseTask");
            private readonly MockMethodWithParam<Tuple<string, int>> _paramTuple = new MockMethodWithParam<Tuple<string, int>>("MockMockMethodWithParam#ParamTuple");
            private readonly MockMethodWithParam<Tuple<string, T>> _paramTypeGeneric = new MockMethodWithParam<Tuple<string, T>>("MockMockMethodWithParam#ParamTypeGeneric");
            private readonly MockMethodWithResponse<T> _responseGeneric = new MockMethodWithResponse<T>("MockMockMethodWithResponse#ResponseGeneric");
            private readonly MockMethodWithResponse<string> _responseType = new MockMethodWithResponse<string>("MockMockMethodWithResponse#ResponseType");
            private readonly MockMethodWithResponse<T> _responseTaskGeneric = new MockMethodWithResponse<T>("MockMockMethodWithResponse#ResponseTaskGeneric");
            private readonly MockMethodWithResponse<double> _responseTaskType = new MockMethodWithResponse<double>("MockMockMethodWithResponse#ResponseTaskType");
            private readonly MockMethodWithParamAndResponse<int, string> _paramTypeResponseTaskType = new MockMethodWithParamAndResponse<int, string>("MockMockMethodWithParamAndResponse#ParamTypeResponseTaskType");
            private readonly MockMethodWithParamAndResponse<Tuple<char, string, IExampleType>, int> _paramTupleResponseType = new MockMethodWithParamAndResponse<Tuple<char, string, IExampleType>, int>("MockMockMethodWithParamAndResponse#ParamTupleResponseType");
            private readonly MockMethodWithParam<Func<Task<T>>> _funcTaskGeneric = new MockMethodWithParam<Func<Task<T>>>("MockBugNumberNine#FuncTaskGeneric");
            private readonly MockMethodWithParamAndResponse<Func<Task<T>>, T> _funcTaskGenericResponseTaskGeneric = new MockMethodWithParamAndResponse<Func<Task<T>>, T>("MockBugNumberNine#FuncTaskGenericResponseTaskGeneric");
            private readonly MockMethodWithParamAndResponse<int, string> _sameNameDifParamsInt = new MockMethodWithParamAndResponse<int, string>("MockEdgeCases#SameNameDifParamsInt");
            private readonly MockMethodWithParamAndResponse<double, string> _sameNameDifParamsDouble = new MockMethodWithParamAndResponse<double, string>("MockEdgeCases#SameNameDifParamsDouble");
            private readonly MockMethodWithParamAndResponse<Action<T>, Action<T>> _actionGenericResponseActionGeneric = new MockMethodWithParamAndResponse<Action<T>, Action<T>>("MockActionExample#ActionGenericResponseActionGeneric");
            private readonly MockMethodWithParamAndResponse<Action<bool>, Action<bool>> _actionTypeResponseActionType = new MockMethodWithParamAndResponse<Action<bool>, Action<bool>>("MockActionExample#ActionTypeResponseActionType");
            private readonly MockMethod _needsMe = new MockMethod("MockFromMe#NeedsMe");

            public MockFromMe<T> Build()
            {
                return new MockFromMe<T>
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
                    _needsMe = _needsMe,
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

            public Builder NeedsMe()
            {
                _needsMe.UpdateInvocation();
                return this;
            }

            public Builder NeedsMe(params Action[] actions)
            {
                _needsMe.UpdateInvocation(actions);
                return this;
            }
        }
    }
}