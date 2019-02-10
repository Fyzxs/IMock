using FluentAssertions;
using InterfaceFakes.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace InterfaceFakesTests.Validators
{
    [TestClass]
    public sealed class ChainValidationTests
    {
        [TestMethod, TestCategory("unit")]
        public void ShouldNotThrowGivenValidChain()
        {
            //Arrange
            ChainValidation subject = new ChainValidation()
                .NextClassInChain<LinkA>()
                .NextClassInChain<LinkB>();

            //Act
            Action action = () => subject.AssertExpectedChainOrder(new LinkHead());

            //Assert
            action.Should().NotThrow();
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldThrowGivenInvalidChain()
        {
            //Arrange
            ChainValidation subject = new ChainValidation()
                .NextClassInChain<LinkA>()
                .NextClassInChain<LinkA>();

            //Act
            Action action = () => subject.AssertExpectedChainOrder(new LinkHead());

            //Assert
            action.Should().Throw<Exception>().WithMessage("Expected [name=_nextAction] to be of [type=LinkA] but found [type=LinkB]");
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldAcceptCustomNames()
        {
            //Arrange
            ChainValidation subject = new ChainValidation()
                .NextClassInChain<LinkC>()
                .NextClassInChain<LinkA>("_someOtherName");

            //Act
            Action action = () => subject.AssertExpectedChainOrder(new LinkCustom());

            //Assert
            action.Should().Throw<Exception>().WithMessage("Expected [name=_someOtherName] to be of [type=LinkA] but found [type=LinkB]");
        }

        private interface ILink { }

        private class LinkHead : ILink
        {
            private readonly ILink _nextAction;

            public LinkHead() : this(new LinkA(new LinkB())) { }

            private LinkHead(ILink nextAction) => _nextAction = nextAction;
        }

        private class LinkCustom : ILink
        {
            private readonly ILink _nextAction;

            public LinkCustom() : this(new LinkC(new LinkB())) { }

            private LinkCustom(ILink nextAction) => _nextAction = nextAction;
        }

        private class LinkA : ILink
        {
            private readonly ILink _nextAction;

            public LinkA(ILink nextAction) => _nextAction = nextAction;
        }

        private class LinkC : ILink
        {
            private readonly ILink _someOtherName;

            public LinkC(ILink nextAction) => _someOtherName = nextAction;
        }

        private class LinkB : ILink
        { }
    }
}