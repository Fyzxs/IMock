using FizzBuzzExample.Actions;
using FizzBuzzExample.Library.Texts;
using FizzBuzzExampleTests.Fluent;
using FizzBuzzExampleTests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FizzBuzzExampleTests.Actions
{
    [TestClass]
    public class FizzResponseActionTests
    {
        [TestMethod, TestCategory("unit")]
        public void ShouldReturnFizzTextGivenEvenlyDivisibleByThree()
        {
            //Arrange
            FakeInt fakeInt = new FakeInt.Builder().RawValue(3).Build();
            FakeResponseAction fakeResponseAction = new FakeResponseAction.Builder().Build();
            FizzResponseAction subject = new FizzResponseAction(fakeResponseAction);

            //Act
            Text actual = subject.Act(fakeInt);

            //Assert
            actual.Should().Be("Fizz");
        }
        [TestMethod, TestCategory("unit")]
        public void ShouldReturnNextActionGivenNotEvenlyDivisibleByThree()
        {
            //Arrange
            FakeText fakeText = new FakeText.Builder().Build();
            FakeInt fakeInt = new FakeInt.Builder().RawValue(3 + 1).Build();
            FakeResponseAction fakeResponseAction = new FakeResponseAction.Builder().Act(fakeText).Build();
            FizzResponseAction subject = new FizzResponseAction(fakeResponseAction);

            //Act
            Text actual = subject.Act(fakeInt);

            //Assert
            actual.Should().BeSameAs(fakeText);
        }
    }
}