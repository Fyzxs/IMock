using FizzBuzzExample.Actions;
using FizzBuzzExampleTests.Fluent;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FizzBuzzExampleTests.Actions {
    [TestClass]
    public class StrategyResponseActionTests
    {
        [TestMethod, TestCategory("unit")]
        public void ShouldValidateChain()
        {
            new ChainValidation()
                .Add<FizzBuzzResponseAction>()
                .Add<BuzzResponseAction>()
                .Add<FizzResponseAction>()
                .Add<NumberResponseAction>()
                .AssertExpectedChain(new StrategyResponseAction());
        }
    }
}