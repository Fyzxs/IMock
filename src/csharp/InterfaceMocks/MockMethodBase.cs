using System;

namespace InterfaceMocks
{
    public abstract class MockMethodBase
    {
        private readonly string _name;

        protected int InvokedCount = 0;

        protected MockMethodBase(string name) => _name = name;

        public void AssertInvoked() => AssertIf(Invoked(), $"{_name} was expected but not invoked.");

        private bool Invoked() => 0 < InvokedCount;
        public void InvokedCountMatches(int count) => AssertIf(count == InvokedCount, $"{_name} [InvokedCount={InvokedCount}] does not match expected [count={count}].");

        protected void AssertIf(bool condition, string message)
        {
            if (condition) return;
            throw new Exception(message);
        }
    }
}