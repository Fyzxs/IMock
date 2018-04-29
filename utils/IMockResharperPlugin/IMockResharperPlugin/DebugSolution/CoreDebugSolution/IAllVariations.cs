﻿using System;
using System.Threading.Tasks;
using CoreDebugSolution;
using InterfaceMocks;

namespace DebugSolution.Sample
{
    public interface IFoozzing<T>
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
        int ParamTupleResponseType(char c, string yeppers, IFooz longer);
        //END MockMethodWithParamAndResponse
        //BEG EdgeCases
        string SameNameDifParams(int one);
        string SameNameDifParams(double one);
        //END EdgeCases
    }

    public partial class MockFoozzing<T> : IFoozzing<T> {
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
        private MockMethodWithParamAndResponse<Tuple<char, string, IFooz>, int> _paramTupleResponseType;
        private MockMethodWithParamAndResponse<int, string> _sameNameDifParamsInt;
        private MockMethodWithParamAndResponse<double, string> _sameNameDifParamsDouble;
        private MockFoozzing() { }
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
        public int ParamTupleResponseType(char c, string yeppers, IFooz longer) => _paramTupleResponseType.Invoke(new Tuple<char, string, IFooz>(c, yeppers, longer));
        public string SameNameDifParams(int one) => _sameNameDifParamsInt.Invoke(one);
        public string SameNameDifParams(double one2) => _sameNameDifParamsDouble.Invoke(one2);

        public class Builder {
            private readonly MockMethod _voidVoid = new MockMethod("MockFoozzing#VoidVoid");
            private readonly MockMethod _responseTaskVoid = new MockMethod("MockFoozzing#ResponseTaskVoid");
            private readonly MockMethodWithParam<double> _paramType = new MockMethodWithParam<double>("MockFoozzing#ParamType");
            private readonly MockMethodWithParam<T> _paramGeneric = new MockMethodWithParam<T>("MockFoozzing#ParamGeneric");
            private readonly MockMethodWithParam<double> _paramTypeResponseTask = new MockMethodWithParam<double>("MockFoozzing#ParamTypeResponseTask");
            private readonly MockMethodWithParam<Tuple<string, int>> _paramTuple = new MockMethodWithParam<Tuple<string, int>>("MockFoozzing#ParamTuple");
            private readonly MockMethodWithParam<Tuple<string, T>> _paramTypeGeneric = new MockMethodWithParam<Tuple<string, T>>("MockFoozzing#ParamTypeGeneric");
            private readonly MockMethodWithResponse<T> _responseGeneric = new MockMethodWithResponse<T>("MockFoozzing#ResponseGeneric");
            private readonly MockMethodWithResponse<string> _responseType = new MockMethodWithResponse<string>("MockFoozzing#ResponseType");
            private readonly MockMethodWithResponse<T> _responseTaskGeneric = new MockMethodWithResponse<T>("MockFoozzing#ResponseTaskGeneric");
            private readonly MockMethodWithResponse<double> _responseTaskType = new MockMethodWithResponse<double>("MockFoozzing#ResponseTaskType");
            private readonly MockMethodWithParamAndResponse<int, string> _paramTypeResponseTaskType = new MockMethodWithParamAndResponse<int, string>("MockFoozzing#ParamTypeResponseTaskType");
            private readonly MockMethodWithParamAndResponse<Tuple<char, string, IFooz>, int> _paramTupleResponseType = new MockMethodWithParamAndResponse<Tuple<char, string, IFooz>, int>("MockFoozzing#ParamTupleResponseType");
            private readonly MockMethodWithParamAndResponse<int, string> _sameNameDifParamsInt = new MockMethodWithParamAndResponse<int, string>("MockFoozzing#SameNameDifParamsInt");
            private readonly MockMethodWithParamAndResponse<double, string> _sameNameDifParamsDouble = new MockMethodWithParamAndResponse<double, string>("MockFoozzing#SameNameDifParamsDouble");

            public MockFoozzing<T> Build()
            {
                return new MockFoozzing<T>
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
                    _sameNameDifParamsInt = _sameNameDifParamsInt,
                    _sameNameDifParamsDouble = _sameNameDifParamsDouble
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
        }

        public void AssertVoidVoidInvoked() => _voidVoid.AssertInvoked();
        public void AssertResponseTaskVoidInvoked() => _responseTaskVoid.AssertInvoked();
        public void AssertParamTypeInvokedWith(double justOne) => _paramType.AssertInvokedWith(justOne);
        public void AssertParamGenericInvokedWith(T justOne) => _paramGeneric.AssertInvokedWith(justOne);
        public void AssertParamTypeResponseTaskInvokedWith(double justOne) => _paramTypeResponseTask.AssertInvokedWith(justOne);
        public void AssertParamTupleInvokedWith(string justOne, int singleInt) => _paramTuple.AssertInvokedWith(new Tuple<string, int>(justOne, singleInt));
        public void AssertParamTypeGenericInvokedWith(string justOne, T oneT) => _paramTypeGeneric.AssertInvokedWith(new Tuple<string, T>(justOne, oneT));
        public void AssertResponseGenericInvoked() => _responseGeneric.AssertInvoked();
        public void AssertResponseTypeInvoked() => _responseType.AssertInvoked();
        public void AssertResponseTaskGenericInvoked() => _responseTaskGeneric.AssertInvoked();
        public void AssertResponseTaskTypeInvoked() => _responseTaskType.AssertInvoked();
    }
}