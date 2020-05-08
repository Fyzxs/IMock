using FluentAssertions;
using InterfaceMocks.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using InterfaceMocks.Exceptions;

namespace InterfaceMocksTests.Validators
{
    [TestClass]
    public sealed class ClassVariableTypeValidationTests
    {
        [TestMethod, TestCategory("unit")]
        public void ShouldNotThrowWithValidVariableInBaseClass()
        {
            //Arrange
            ClassVariableTypeValidation subject = new ClassVariableTypeValidation()
                .FieldShouldBeType<string>("_value");

            //Act
            Action action = () => subject.AssertFieldsAreExpectedTypeInBaseClass<BaseClass>(new DerivedClass());

            //Assert
            action.Should().NotThrow();
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldThrowWithInvalidVariableNameInBaseClass()
        {
            //Arrange
            ClassVariableTypeValidation subject = new ClassVariableTypeValidation()
                .FieldShouldBeType<string>("_value2");

            //Act
            Action action = () => subject.AssertFieldsAreExpectedTypeInBaseClass<BaseClass>(new DerivedClass());

            //Assert
            action.Should().Throw<Exception>().WithMessage("Expected field [name=_value2] to be of [type=string] but a field [name=_value2] was not found.");
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldThrowWithInvalidVariableTypeInBaseClass()
        {
            //Arrange
            ClassVariableTypeValidation subject = new ClassVariableTypeValidation()
                .FieldShouldBeType<Action>("_value");

            //Act
            Action action = () => subject.AssertFieldsAreExpectedTypeInBaseClass<BaseClass>(new DerivedClass());

            //Assert
            action.Should().Throw<Exception>().WithMessage("Expected [name=_value] to be of [type=Action] but found [type=String]");
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldNotThrowWithValidVariable()
        {
            //Arrange
            ClassVariableTypeValidation subject = new ClassVariableTypeValidation()
                .FieldShouldBeType<string>("_value");

            //Act
            Action action = () => subject.AssertFieldsAreExpectedType(new VariabledClass());

            //Assert
            action.Should().NotThrow();
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldThrowWithInvalidVariableName()
        {
            //Arrange
            ClassVariableTypeValidation subject = new ClassVariableTypeValidation()
                .FieldShouldBeType<string>("_value2");

            //Act
            Action action = () => subject.AssertFieldsAreExpectedType(new VariabledClass());

            //Assert
            action.Should().Throw<Exception>().WithMessage("Expected field [name=_value2] to be of [type=string] but a field [name=_value2] was not found.");
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldThrowWithInvalidVariableType()
        {
            //Arrange
            ClassVariableTypeValidation subject = new ClassVariableTypeValidation()
                .FieldShouldBeType<Action>("_value");

            //Act
            Action action = () => subject.AssertFieldsAreExpectedType(new VariabledClass());

            //Assert
            action.Should().Throw<AsserterException>().WithMessage("Expected [name=_value] to be of [type=Action] but found [type=String]");
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldNotThrowGivenMatchingPrimitive()
        {
            //Arrange
            ClassVariableTypeValidation subject = new ClassVariableTypeValidation()
                .FieldShouldBeType<string>("_value", "stringValue");
            VariabledClass target = new VariabledClass();

            //Act
            subject.AssertFieldsAreExpectedType(target);

            //Assert
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldNotThrowGivenSameObject()
        {
            //Arrange
            VariabledClass expectedValue = new VariabledClass();
            ExampleObject exampleObject = new ExampleObject(expectedValue);
            ClassVariableTypeValidation subject = new ClassVariableTypeValidation()
                .FieldShouldBeType<VariabledClass>("_variable", expectedValue);

            //Act
            Action action = () => subject.AssertFieldsAreExpectedType(exampleObject);

            //Assert
            action.Should().NotThrow();
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldThrowGivenDifferentObjects()
        {
            //Arrange
            ExampleObject exampleObject = new ExampleObject(new VariabledClass());
            ClassVariableTypeValidation subject = new ClassVariableTypeValidation()
                .FieldShouldBeType<VariabledClass>("_variable", new VariabledClass());

            //Act
            Action action = () => subject.AssertFieldsAreExpectedType(exampleObject);

            //Assert
            action.Should().Throw<AsserterException>().WithMessage(
                "Field [name=_variable] is not the same reference as expected and does not '#Equals()' actual. [expected=InterfaceMocksTests.Validators.ClassVariableTypeValidationTests+VariabledClass] [actual  =InterfaceMocksTests.Validators.ClassVariableTypeValidationTests+VariabledClass]");
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldNotThrowGivenEquatableObject()
        {
            //Arrange
            ClassVariableTypeValidation subject = new ClassVariableTypeValidation()
                .FieldShouldBeType<EquatableObject>("_eo", new EquatableObject("SomeValue"));

            //Act
            Action action = () => subject.AssertFieldsAreExpectedType(new EquatableObjectContainer(new EquatableObject("SomeValue")));

            //Assert
            action.Should().NotThrow();
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldThrowGivenEquatableNonEqualObject()
        {
            //Arrange
            ClassVariableTypeValidation subject = new ClassVariableTypeValidation()
                .FieldShouldBeType<EquatableObject>("_eo", new EquatableObject("SomeValue"));

            //Act
            Action action = () => subject.AssertFieldsAreExpectedType(new EquatableObjectContainer(new EquatableObject("OtherValue")));

            //Assert
            action.Should().Throw<AsserterException>().WithMessage(
                "Field [name=_eo] is not the same reference as expected and does not '#Equals()' actual. [expected=InterfaceMocksTests.Validators.ClassVariableTypeValidationTests+EquatableObject] [actual  =InterfaceMocksTests.Validators.ClassVariableTypeValidationTests+EquatableObject]");
        }

        private sealed class ExampleObject
        {
            private readonly VariabledClass _variable;

            public ExampleObject(VariabledClass variable) => _variable = variable;
        }

        private sealed class EquatableObject
        {
            private readonly string _lazy;

            public EquatableObject(string lazy) => _lazy = lazy;

            public override bool Equals(object obj)
            {
                return ((EquatableObject) obj)._lazy == _lazy;
            }
        }

        private sealed class EquatableObjectContainer
        {
            private readonly EquatableObject _eo;

            public EquatableObjectContainer(EquatableObject eo) => _eo = eo;
        }

        private sealed class VariabledClass
        {
            private readonly string _value;

            public VariabledClass() : this("stringValue") { }

            private VariabledClass(string value) => _value = value;
        }

        private abstract class BaseClass
        {
            private readonly string _value;

            protected BaseClass(string value) => _value = value;
        }

        private sealed class DerivedClass : BaseClass
        {
            public DerivedClass():base("string"){}
        }
    }
}