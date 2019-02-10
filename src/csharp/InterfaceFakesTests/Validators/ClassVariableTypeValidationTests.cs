using FluentAssertions;
using InterfaceFakes.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace InterfaceFakesTests.Validators
{
    [TestClass]
    public class ClassVariableTypeValidationTests
    {
        [TestMethod, TestCategory("unit")]
        public void ShouldNotThrowWithValidVariable()
        {
            //Arrange
            ClassVariableTypeValidation subject = new ClassVariableTypeValidation().Add<string>("_value");

            //Act
            Action action = () => subject.AssertExpectedVariables(new VariabledClass());

            //Assert
            action.Should().NotThrow();
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldThrowWithInvalidVariableName()
        {
            //Arrange
            ClassVariableTypeValidation subject = new ClassVariableTypeValidation().Add<string>("_value2");

            //Act
            Action action = () => subject.AssertExpectedVariables(new VariabledClass());

            //Assert
            action.Should().Throw<Exception>().WithMessage("Expected [name=_value2] to be of [type=string] but found null");
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldThrowWithInvalidVariableType()
        {
            //Arrange
            ClassVariableTypeValidation subject = new ClassVariableTypeValidation().Add<Action>("_value");

            //Act
            Action action = () => subject.AssertExpectedVariables(new VariabledClass());

            //Assert
            action.Should().Throw<Exception>().WithMessage("Expected [name=_value] to be of [type=Action] but found [type=String]");
        }

        private class VariabledClass
        {
            private readonly string _value;

            public VariabledClass() : this("string") { }

            private VariabledClass(string value)
            {
                _value = value;
            }
        }
    }
}