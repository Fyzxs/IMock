using FluentAssertions;
using InterfaceMocks.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace InterfaceMocksTests.Validators
{
    [TestClass]
    public class ChainValidationTests
    {
        [TestMethod, TestCategory("unit")]
        public void ShouldNotThrowGivenValidChain()
        {
            //Arrange
            ChainValidation subject = new ChainValidation();
            subject.Add<LinkA>().Add<LinkB>();

            //Act
            Action action = () => subject.AssertExpectedChain(new LinkHead());

            //Assert
            action.Should().NotThrow();
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldThrowGivenInvalidChain()
        {
            //Arrange
            ChainValidation subject = new ChainValidation();
            subject.Add<LinkA>().Add<LinkA>();

            //Act
            Action action = () => subject.AssertExpectedChain(new LinkHead());

            //Assert
            action.Should().Throw<Exception>().WithMessage("Expected [name=_nextAction] to be of [type=LinkA] but found [type=LinkB]");
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldAcceptCustomNames()
        {
            //Arrange
            ChainValidation subject = new ChainValidation();
            subject.Add<LinkC>().Add<LinkA>("_someOtherName");

            //Act
            Action action = () => subject.AssertExpectedChain(new LinkCustom());

            //Assert
            action.Should().Throw<Exception>().WithMessage("Expected [name=_someOtherName] to be of [type=LinkA] but found [type=LinkB]");
        }

        private interface ILink { }

        private class LinkHead : ILink
        {
            private readonly ILink _nextAction;

            public LinkHead() : this(new LinkA(new LinkB())) { }

            private LinkHead(ILink nextAction)
            {
                _nextAction = nextAction;
            }
        }

        private class LinkCustom : ILink
        {
            private readonly ILink _nextAction;

            public LinkCustom() : this(new LinkC(new LinkB())) { }

            private LinkCustom(ILink nextAction)
            {
                _nextAction = nextAction;
            }
        }

        private class LinkA : ILink
        {
            private readonly ILink _nextAction;

            public LinkA(ILink nextAction)
            {
                _nextAction = nextAction;
            }
        }

        private class LinkC : ILink
        {
            private readonly ILink _someOtherName;

            public LinkC(ILink nextAction)
            {
                _someOtherName = nextAction;
            }
        }

        private class LinkB : ILink
        { }
    }
}