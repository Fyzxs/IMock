using FizzBuzzExample.Actions;
using FizzBuzzExample.Library.Texts;
using FizzBuzzExampleTests.Fluent;
using FizzBuzzExampleTests.Mocks;
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
            MockInt mockInt = new MockInt.Builder().RawValue(5).Build();
            MockResponseAction mockResponseAction = new MockResponseAction.Builder().Build();
            BuzzResponseAction subject = new BuzzResponseAction(mockResponseAction);

            //Act
            Text actual = subject.Act(mockInt);

            //Assert
            actual.Should().Be("Buzz");
        }
        [TestMethod, TestCategory("unit")]
        public void ShouldReturnNextActionGivenNotEvenlyDivisibleByFive()
        {
            //Arrange
            MockText mockText = new MockText.Builder().Build();
            MockInt mockInt = new MockInt.Builder().RawValue(5 + 1).Build();
            MockResponseAction mockResponseAction = new MockResponseAction.Builder().Act(mockText).Build();
            BuzzResponseAction subject = new BuzzResponseAction(mockResponseAction);

            //Act
            Text actual = subject.Act(mockInt);

            //Assert
            actual.Should().BeSameAs(mockText);
        }
    }
}