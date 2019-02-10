using InterfaceFakes;
using System;
using System.Threading.Tasks;

namespace InterfaceFakesTests.Fakes
{
    public sealed partial class FakeFakeMethodWithParam<TParam> : IFakeMethodWithParam<TParam>
    {
        public sealed class Builder
        {
            private readonly FakeMethod _updateInvocationItem = new FakeMethod("FakeMethodWithParam#UpdateInvocation");
            private readonly FakeMethodWithParam<TParam> _invokeItem = new FakeMethodWithParam<TParam>("FakeMethodWithParam#Invoke");
            private readonly FakeMethodWithParam<TParam> _invokeTaskItem = new FakeMethodWithParam<TParam>("FakeMethodWithParam#InvokeTask");
            private readonly FakeMethodWithParam<TParam> _assertInvokedWithItem = new FakeMethodWithParam<TParam>("FakeMethodWithParam#AssertInvokedWith");
            private readonly FakeMethodWithParam<Action<TParam>> _assertCustomItem = new FakeMethodWithParam<Action<TParam>>("FakeMethodWithParam#AssertCustom");

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

            public FakeFakeMethodWithParam<TParam> Build()
            {
                return new FakeFakeMethodWithParam<TParam>
                {
                    _updateInvocation = _updateInvocationItem,
                    _invoke = _invokeItem,
                    _invokeTask = _invokeTaskItem,
                    _assertInvokedWith = _assertInvokedWithItem,
                    _assertCustom = _assertCustomItem
                };
            }
        }

        private FakeMethod _updateInvocation;
        private FakeMethodWithParam<TParam> _invoke;
        private FakeMethodWithParam<TParam> _invokeTask;
        private FakeMethodWithParam<TParam> _assertInvokedWith;
        private FakeMethodWithParam<Action<TParam>> _assertCustom;

        private FakeFakeMethodWithParam() { }

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