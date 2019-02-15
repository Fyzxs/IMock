using InterfaceMocks;
using System;
using System.Threading.Tasks;

namespace InterfaceMocksTests.Mocks
{
    public sealed partial class MockMockMethodWithParam<TParam> : IMockMethodWithParam<TParam>
    {
        public sealed class Builder
        {
            private readonly MockMethod _updateInvocationItem = new MockMethod("MockMethodWithParam#UpdateInvocation");
            private readonly MockMethodWithParam<TParam> _invokeItem = new MockMethodWithParam<TParam>("MockMethodWithParam#Invoke");
            private readonly MockMethodWithParam<TParam> _invokeTaskItem = new MockMethodWithParam<TParam>("MockMethodWithParam#InvokeTask");
            private readonly MockMethodWithParam<TParam> _assertInvokedWithItem = new MockMethodWithParam<TParam>("MockMethodWithParam#AssertInvokedWith");
            private readonly MockMethodWithParam<Action<TParam>> _assertCustomItem = new MockMethodWithParam<Action<TParam>>("MockMethodWithParam#AssertCustom");

            public Builder AssertInvokedWith()
            {
                _assertInvokedWithItem.UpdateInvocation();
                return this;
            }
            public Builder AssertCustom()
            {
                _assertCustomItem.UpdateInvocation();
                return this;
            }
            public Builder InvokeTask()
            {
                _invokeTaskItem.UpdateInvocation();
                return this;
            }

            public Builder Invoke()
            {
                _invokeItem.UpdateInvocation();
                return this;
            }

            public Builder UpdateInvocation()
            {
                _updateInvocationItem.UpdateInvocation();
                return this;
            }

            public MockMockMethodWithParam<TParam> Build()
            {
                return new MockMockMethodWithParam<TParam>
                {
                    _updateInvocation = _updateInvocationItem,
                    _invoke = _invokeItem,
                    _invokeTask = _invokeTaskItem,
                    _assertInvokedWith = _assertInvokedWithItem,
                    _assertCustom = _assertCustomItem
                };
            }
        }

        private MockMethod _updateInvocation;
        private MockMethodWithParam<TParam> _invoke;
        private MockMethodWithParam<TParam> _invokeTask;
        private MockMethodWithParam<TParam> _assertInvokedWith;
        private MockMethodWithParam<Action<TParam>> _assertCustom;

        private MockMockMethodWithParam() { }

        public void UpdateInvocation() => _updateInvocation.Invoke();

        public void UpdateInvocation(params Action[] action) => _updateInvocation.Invoke();

        public void Invoke(TParam value) => _invoke.Invoke(value);

        public Task InvokeTask(TParam value) => _invokeTask.InvokeTask(value);

        public void AssertCustom(Action<TParam> assertion) => _assertCustom.Invoke(assertion);

        public void AssertInvokedWith(TParam expected) => _assertInvokedWith.Invoke(expected);


        public void AssertAssertCustomInvoked() => _assertCustom.AssertInvoked();

        public void AssertAssertInvokedWithInvokedWith(TParam expected) => _assertInvokedWith.AssertInvokedWith(expected);

        public void AssertUpdateInvocationInvoked() => _updateInvocation.AssertInvoked();
    }
}