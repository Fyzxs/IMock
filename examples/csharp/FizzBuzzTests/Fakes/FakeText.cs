using FizzBuzzExample.Library.Texts;
using InterfaceFakes;

namespace FizzBuzzExampleTests.Fakes
{
    public class FakeText : Text
    {
        public class Builder
        {
            private readonly FakeMethodWithResponse<string> _fakeRawValue = new FakeMethodWithResponse<string>("FakeText#RawValue");

            public Builder RawValue(params string[] returnValue)
            {
                _fakeRawValue.UpdateInvocation(returnValue);
                return this;
            }

            public FakeText Build()
            {
                return new FakeText()
                {
                    _rawValue = _fakeRawValue
                };
            }
        }

        private FakeMethodWithResponse<string> _rawValue;
        private FakeText() { }

        protected override string RawValue() => _rawValue.Invoke();
    }
}