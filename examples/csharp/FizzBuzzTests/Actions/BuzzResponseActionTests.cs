using FizzBuzzExample.Actions;
using FizzBuzzExample.Library.Texts;
using FizzBuzzExampleTests.Fluent;
using FizzBuzzExampleTests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FizzBuzzExampleTests.Actions
{
    [TestClass]
    public class BuzzResponseActionTests
    {
        [TestMethod, TestCategory("unit")]
        public void ShouldReturnBuzzTextGivenEvenlyDivisibleByFive()
        {
            //Arrange
            FakeInt fakeInt = new FakeInt.Builder().RawValue(5).Build();
            FakeResponseAction fakeResponseAction = new FakeResponseAction.Builder().Build();
            BuzzResponseAction subject = new BuzzResponseAction(fakeResponseAction);

            //Act
            Text actual = subject.Act(fakeInt);

            //Assert
            actual.Should().Be("Buzz");
        }
        [TestMethod, TestCategory("unit")]
        public void ShouldReturnNextActionGivenNotEvenlyDivisibleByFive()
        {
            //Arrange
            FakeText fakeText = new FakeText.Builder().Build();
            FakeInt fakeInt = new FakeInt.Builder().RawValue(5 + 1).Build();
            FakeResponseAction fakeResponseAction = new FakeResponseAction.Builder().Act(fakeText).Build();
            BuzzResponseAction subject = new BuzzResponseAction(fakeResponseAction);

            //Act
            Text actual = subject.Act(fakeInt);

            //Assert
            actual.Should().BeSameAs(fakeText);
        }
    }
}