using System;
using System.Threading.Tasks;
using InterfaceMocks;

namespace CoreDebugSolution
{
    public interface IExample
    {
        MockMethod KeepsReference();
    }

    public interface IAllVariations<T>
    {
        //BEG MockMethod
        void VoidVoid();
        Task ResponseTaskVoid();
        //END MockMethod
        //BEG MockMethodWithParam
        void ParamType(double justOne);
        void ParamGeneric(T justOne);
        Task ParamTypeResponseTask(double justOne);
        void ParamTuple(string justOne, int singleInt);
        void ParamTypeGeneric(string justOne, T oneT);
        //END MockMethodWithParam
        //BEG MockMethodWithResponse
        T ResponseGeneric();
        string ResponseType();
        Task<T> ResponseTaskGeneric();
        Task<double> ResponseTaskType();
        //END MockMethodWithResponse
        //BEG MockMethodWithParamAndResponse
        Task<string> ParamTypeResponseTaskType(int incoming);
        int ParamTupleResponseType(char c, string yeppers, IExample longer);
        //END MockMethodWithParamAndResponse
        //BEG EdgeCases
        string SameNameDifParams(int one);
        string SameNameDifParams(double one);
        //END EdgeCases
    }

    public partial class MockAllVariations<T> : IAllVariations<T> {
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
        private MockMethodWithParamAndResponse<Tuple<char, string, IExample>, int> _paramTupleResponseType;
        private MockMethodWithParamAndResponse<int, string> _sameNameDifParamsint;
        private MockMethodWithParamAndResponse<double, string> _sameNameDifParamsdouble;
        private MockAllVariations() { }
        public void VoidVoid() => _voidVoid.Invoke();
        public Task ResponseTaskVoid() => _responseTaskVoid.InvokeTask();
        public void ParamType(double justOne) => _paramType.Invoke(justOne);
        public void ParamGeneric(T justOne) => _paramGeneric.Invoke(justOne);
        public Task ParamTypeResponseTask(double justOne) => _paramTypeResponseTask.InvokeTask(justOne);
        public void ParamTuple(string justOne, int singleInt) => _paramTuple.Invoke(new Tuple<string, int>(justOne, singleInt));
        public void ParamTypeGeneric(string justOne, T oneT) => _paramTypeGeneric.Invoke(new Tuple<string, T>(justOne, oneT));
        public T ResponseGeneric() => _responseGeneric.Invoke();
        public string ResponseType() => _responseType.Invoke();
        public Task<T> ResponseTaskGeneric() => _responseTaskGeneric.InvokeTask();
        public Task<double> ResponseTaskType() => _responseTaskType.InvokeTask();
        public Task<string> ParamTypeResponseTaskType(int incoming) => _paramTypeResponseTaskType.InvokeTask(incoming);
        public int ParamTupleResponseType(char c, string yeppers, IExample longer) => _paramTupleResponseType.Invoke(new Tuple<char, string, IExample>(c, yeppers, longer));
        public string SameNameDifParams(int one) => _sameNameDifParamsint.Invoke(one);
        public string SameNameDifParams(double one) => _sameNameDifParamsdouble.Invoke(one);

        public class Builder {
            private readonly MockMethod _voidVoid = new MockMethod("MockAllVariations#VoidVoid");
            private readonly MockMethod _responseTaskVoid = new MockMethod("MockAllVariations#ResponseTaskVoid");
            private readonly MockMethodWithParam<double> _paramType = new MockMethodWithParam<double>("MockAllVariations#ParamType");
            private readonly MockMethodWithParam<T> _paramGeneric = new MockMethodWithParam<T>("MockAllVariations#ParamGeneric");
            private readonly MockMethodWithParam<double> _paramTypeResponseTask = new MockMethodWithParam<double>("MockAllVariations#ParamTypeResponseTask");
            private readonly MockMethodWithParam<Tuple<string, int>> _paramTuple = new MockMethodWithParam<Tuple<string, int>>("MockAllVariations#ParamTuple");
            private readonly MockMethodWithParam<Tuple<string, T>> _paramTypeGeneric = new MockMethodWithParam<Tuple<string, T>>("MockAllVariations#ParamTypeGeneric");
            private readonly MockMethodWithResponse<T> _responseGeneric = new MockMethodWithResponse<T>("MockAllVariations#ResponseGeneric");
            private readonly MockMethodWithResponse<string> _responseType = new MockMethodWithResponse<string>("MockAllVariations#ResponseType");
            private readonly MockMethodWithResponse<T> _responseTaskGeneric = new MockMethodWithResponse<T>("MockAllVariations#ResponseTaskGeneric");
            private readonly MockMethodWithResponse<double> _responseTaskType = new MockMethodWithResponse<double>("MockAllVariations#ResponseTaskType");
            private readonly MockMethodWithParamAndResponse<int, string> _paramTypeResponseTaskType = new MockMethodWithParamAndResponse<int, string>("MockAllVariations#ParamTypeResponseTaskType");
            private readonly MockMethodWithParamAndResponse<Tuple<char, string, IExample>, int> _paramTupleResponseType = new MockMethodWithParamAndResponse<Tuple<char, string, IExample>, int>("MockAllVariations#ParamTupleResponseType");
            private readonly MockMethodWithParamAndResponse<int, string> _sameNameDifParamsint = new MockMethodWithParamAndResponse<int, string>("MockAllVariations#SameNameDifParamsint");
            private readonly MockMethodWithParamAndResponse<double, string> _sameNameDifParamsdouble = new MockMethodWithParamAndResponse<double, string>("MockAllVariations#SameNameDifParamsdouble");

            public MockAllVariations<T> Build()
            {
                return new MockAllVariations<T>
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
                    _sameNameDifParamsint = _sameNameDifParamsint,
                    _sameNameDifParamsdouble = _sameNameDifParamsdouble
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

            public Builder SameNameDifParamsint(params string[] responseValues)
            {
                _sameNameDifParamsint.UpdateInvocation(responseValues);
                return this;
            }

            public Builder SameNameDifParamsint(params Func<string>[] responseValues)
            {
                _sameNameDifParamsint.UpdateInvocation(responseValues);
                return this;
            }

            public Builder SameNameDifParamsdouble(params string[] responseValues)
            {
                _sameNameDifParamsdouble.UpdateInvocation(responseValues);
                return this;
            }

            public Builder SameNameDifParamsdouble(params Func<string>[] responseValues)
            {
                _sameNameDifParamsdouble.UpdateInvocation(responseValues);
                return this;
            }
        }
    }
}