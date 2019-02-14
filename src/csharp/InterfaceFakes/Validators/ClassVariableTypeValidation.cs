using System;
using System.Collections.Generic;

namespace InterfaceFakes.Validators
{
    /// <summary>
    /// Perform type validation against the private variables of a class.
    /// <example>
    /// Usage for variables in class:
    /// <code>
    /// new ClassVariableTypeValidation().Add&lt;ExpectedType>("_someVariableName").Add&lt;ExpectedOtherTyper>("_variableName").AssertExpectedVariables(new HeadOfChainBeingTested());
    /// </code>
    /// Usage for variables in super type:
    /// <code>
    /// new ClassVariableTypeValidation().Add&lt;ExpectedType>("_someVariableName").Add&lt;ExpectedOtherTyper>("_variableName").AssertExpectedVariables&lt;SuperType>(new ClassUnderTest());
    /// </code>
    /// </example>
    /// </summary>
    public sealed class ClassVariableTypeValidation
    {
        private readonly List<ValidationInfo> _validationInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassVariableTypeValidation"/> class.
        /// </summary>
        public ClassVariableTypeValidation() : this(new List<ValidationInfo>()) { }

        private ClassVariableTypeValidation(List<ValidationInfo> validationInfo) => _validationInfo = validationInfo;

        /// <summary>
        /// Adds the next class of the chain to be validated.
        /// </summary>
        /// <typeparam name="T">The class expected to be next in the chain.</typeparam>
        /// <param name="name">The name of the private variable the instance is held in.</param>
        /// <returns>This instance of ChainValidation</returns>
        public ClassVariableTypeValidation FieldShouldBeType<T>(string name)
        {
            _validationInfo.Add(new ValidationInfo(name, typeof(T)));
            return this;
        }

        /// <summary>
        /// Validates the class fields specified through the <see cref="FieldShouldBeType{T}"/> method.
        /// </summary>
        /// <param name="classToValidate">The instance to start validation against.</param>
        public void AssertFieldsAreExpectedType(object classToValidate)
        {
            foreach (ValidationInfo info in _validationInfo)
            {
                info.AssertType(GetObjectToValidate(classToValidate, info));
            }
        }

        /// <summary>
        /// Validates the class fields in the super type <typeparamref name="T"/> specified through the <see cref="FieldShouldBeType{T}"/> method.
        /// </summary>
        /// <param name="classToValidate">The instance to start validation against.</param>
        public void AssertFieldsAreExpectedTypeInBaseClass<T>(object classToValidate)
        {
            foreach (ValidationInfo info in _validationInfo)
            {
                info.AssertType(GetObjectToValidate<T>(classToValidate, info));
            }
        }

        private object GetObjectToValidate(object obj, ValidationInfo validationInfo) => validationInfo.FieldInfo(obj).GetValue(obj);

        private object GetObjectToValidate<T>(object obj, ValidationInfo validationInfo) => validationInfo.FieldInfo<T>().GetValue(obj);
    }
}