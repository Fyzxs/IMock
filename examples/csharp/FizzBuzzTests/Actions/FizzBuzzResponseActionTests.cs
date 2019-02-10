using FizzBuzzExample.Actions;
using FizzBuzzExample.Library.Texts;
using FizzBuzzExampleTests.Fluent;
using FizzBuzzExampleTests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FizzBuzzExampleTests.Actions {
    [TestClass]
    public class FizzBuzzResponseActionTests
    {
        [TestMethod, TestCategory("unit")]
        public void ShouldReturnFizzBuzzTextGivenEvenlyDivisibleByThreeAndFive()
        {
            //Arrange
            FakeInt fakeInt = new FakeInt.Builder().RawValue(3 * 5).Build();
            FakeResponseAction fakeResponseAction = new FakeResponseAction.Builder().Build();
            FizzBuzzResponseAction subject = new FizzBuzzResponseAction(fakeResponseAction);

            //Act
            Text actual = subject.Act(fakeInt);

            //Assert
            actual.Should().Be("FizzBuzz");
        }
        [TestMethod, TestCategory("unit")]
        public void ShouldReturnNextActionGivenNotEvenlyDivisibleByThree()
        {
            //Arrange
            FakeText fakeText = new FakeText.Builder().Build();
            FakeInt fakeInt = new FakeInt.Builder().RawValue(3 * 5 + 1).Build();
            FakeResponseAction fakeResponseAction = new FakeResponseAction.Builder().Act(fakeText).Build();
            FizzBuzzResponseAction subject = new FizzBuzzResponseAction(fakeResponseAction);

            //Act
            Text actual = subject.Act(fakeInt);

            //Assert
            actual.Should().BeSameAs(fakeText);
        }
    }
}