using FizzBuzzExample.Library.Texts;
using InterfaceMocks;

namespace FizzBuzzExampleTests.Mocks
{
    public class MockText : Text
    {
        public class Builder
        {
            private readonly MockMethodWithResponse<string> _mockRawValue = new MockMethodWithResponse<string>("MockText#RawValue");

            public Builder RawValue(params string[] returnValue)
            {
                _mockRawValue.UpdateInvocation(returnValue);
                return this;
            }

            public MockText Build()
            {
                return new MockText()
                {
                    _rawValue = _mockRawValue
                };
            }
        }

        private MockMethodWithResponse<string> _rawValue;
        private MockText() { }

        protected override string RawValue() => _rawValue.Invoke();
    }
}