using InterfaceMocks.Exceptions;

namespace InterfaceMocks
{
    public abstract class MockMethodBase
    {
        private readonly string _name;
        private readonly IAsserter _asserter;

        protected int InvokedCount = 0;

        protected MockMethodBase(string name) : this(name, new Asserter()) { }

        private MockMethodBase(string name, IAsserter asserter)
        {
            _name = name;
            _asserter = asserter;
        }

        public void AssertInvoked() => _asserter.AssertIf(Invoked(), $"{_name} was expected but not invoked.");

        public void InvokedCountMatches(int count) => _asserter.AssertIf(count == InvokedCount, $"{_name} [InvokedCount={InvokedCount}] does not match expected [count={count}].");

        private bool Invoked() => 0 < InvokedCount;
    }
}