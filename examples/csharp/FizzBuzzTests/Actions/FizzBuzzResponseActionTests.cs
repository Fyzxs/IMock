using FizzBuzzExample.Actions;
using FizzBuzzExample.Library.Texts;
using FizzBuzzExampleTests.Fluent;
using FizzBuzzExampleTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FizzBuzzExampleTests.Actions {
    [TestClass]
    public class FizzBuzzResponseActionTests
    {
        [TestMethod, TestCategory("unit")]
        public void ShouldReturnFizzBuzzTextGivenEvenlyDivisibleByThreeAndFive()
        {
            //Arrange
            MockInt mockInt = new MockInt.Builder().RawValue(3 * 5).Build();
            MockResponseAction mockResponseAction = new MockResponseAction.Builder().Build();
            FizzBuzzResponseAction subject = new FizzBuzzResponseAction(mockResponseAction);

            //Act
            Text actual = subject.Act(mockInt);

            //Assert
            actual.Should().Be("FizzBuzz");
        }
        [TestMethod, TestCategory("unit")]
        public void ShouldReturnNextActionGivenNotEvenlyDivisibleByThree()
        {
            //Arrange
            MockText mockText = new MockText.Builder().Build();
            MockInt mockInt = new MockInt.Builder().RawValue(3 * 5 + 1).Build();
            MockResponseAction mockResponseAction = new MockResponseAction.Builder().Act(mockText).Build();
            FizzBuzzResponseAction subject = new FizzBuzzResponseAction(mockResponseAction);

            //Act
            Text actual = subject.Act(mockInt);

            //Assert
            actual.Should().BeSameAs(mockText);
        }
    }
}