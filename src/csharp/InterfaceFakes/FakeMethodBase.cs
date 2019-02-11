using InterfaceFakes.Exceptions;
using InterfaceFakes.Library;

namespace InterfaceFakes
{
    /// <summary>
    /// Abstract class with core common functionality for fakeing methods.
    /// </summary>
    public abstract class FakeMethodBase
    {
        private readonly string _name;
        private readonly IAsserter _asserter;
        private readonly ICounter _invokedCounter;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeMethodBase"/> class.
        /// </summary>
        /// <param name="name"></param>
        protected FakeMethodBase(string name) : this(name, new Counter(), new Asserter()) { }

        private FakeMethodBase(string name, ICounter counter, IAsserter asserter)
        {
            _name = name;
            _invokedCounter = counter;
            _asserter = asserter;
        }

        /// <summary>
        /// Assert that the faked method has been invoked.
        /// </summary>
        public void AssertInvoked() => _asserter.AssertIf(0 == _invokedCounter.Value(), $"{_name} was expected but not invoked.");

        /// <summary>
        /// Assert the faked method has been invoked the expected number of times.
        /// </summary>
        /// <param name="expectedInvokeCount">Expected invoke count</param>
        public void AssertInvokedCountMatches(int expectedInvokeCount) => _asserter.AssertIf(expectedInvokeCount != _invokedCounter.Value(), $"{_name} [InvokedCount={_invokedCounter.Value()}] does not match expected [count={expectedInvokeCount}].");

        /// <summary>
        /// Updates information that a method has been invoked.
        /// </summary>
        protected void MethodInvoked() => _invokedCounter.Increment();
    }
}