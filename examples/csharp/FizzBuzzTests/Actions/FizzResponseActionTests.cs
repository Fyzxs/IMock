using FizzBuzzExample.Actions;
using FizzBuzzExample.Library.Texts;
using FizzBuzzExampleTests.Fluent;
using FizzBuzzExampleTests.Mocks;
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
            MockInt mockInt = new MockInt.Builder().RawValue(3).Build();
            MockResponseAction mockResponseAction = new MockResponseAction.Builder().Build();
            FizzResponseAction subject = new FizzResponseAction(mockResponseAction);

            //Act
            Text actual = subject.Act(mockInt);

            //Assert
            actual.Should().Be("Fizz");
        }
        [TestMethod, TestCategory("unit")]
        public void ShouldReturnNextActionGivenNotEvenlyDivisibleByThree()
        {
            //Arrange
            MockText mockText = new MockText.Builder().Build();
            MockInt mockInt = new MockInt.Builder().RawValue(3 + 1).Build();
            MockResponseAction mockResponseAction = new MockResponseAction.Builder().Act(mockText).Build();
            FizzResponseAction subject = new FizzResponseAction(mockResponseAction);

            //Act
            Text actual = subject.Act(mockInt);

            //Assert
            actual.Should().BeSameAs(mockText);
        }
    }
}