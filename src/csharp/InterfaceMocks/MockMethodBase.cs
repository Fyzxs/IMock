using InterfaceMocks.Exceptions;
using InterfaceMocks.Library;

namespace InterfaceMocks
{
    /// <summary>
    /// Abstract class with core common functionality for mocking methods.
    /// </summary>
    public abstract class MockMethodBase
    {
        private readonly string _name;
        private readonly IAsserter _asserter;
        private readonly ICounter _invokedCounter;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockMethodBase"/> class.
        /// </summary>
        /// <param name="name"></param>
        protected MockMethodBase(string name) : this(name, new Counter(), new Asserter()) { }

        private MockMethodBase(string name, ICounter counter, IAsserter asserter)
        {
            _name = name;
            _invokedCounter = counter;
            _asserter = asserter;
        }

        /// <summary>
        /// Assert that the mocked method has been invoked.
        /// </summary>
        public void AssertInvoked() => _asserter.AssertIf(0 == _invokedCounter.Value(), $"{_name} was expected but not invoked.");

        /// <summary>
        /// Assert the mocked method has been invoked the expected number of times.
        /// </summary>
        /// <param name="expectedInvokeCount">Expected invoke count</param>
        public void AssertInvokedCountMatches(int expectedInvokeCount) => _asserter.AssertIf(expectedInvokeCount != _invokedCounter.Value(), $"{_name} [InvokedCount={_invokedCounter.Value()}] does not match expected [count={expectedInvokeCount}].");

        /// <summary>
        /// Updates information that a method has been invoked.
        /// </summary>
        protected void MethodInvoked() => _invokedCounter.Increment();
    }
}