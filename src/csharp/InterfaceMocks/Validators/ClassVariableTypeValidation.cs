using System.Collections.Generic;

namespace InterfaceMocks.Validators
{
    public class ClassVariableTypeValidation
    {
        private readonly List<ValidationInfo> _validationInfo;

        public ClassVariableTypeValidation() : this(new List<ValidationInfo>()) { }

        public ClassVariableTypeValidation(List<ValidationInfo> validationInfo) => _validationInfo = validationInfo;

        public ClassVariableTypeValidation Add<T>(string name)
        {
            _validationInfo.Add(new ValidationInfo(name, typeof(T)));
            return this;
        }

        public void AssertExpectedVariables(object classToValidate)
        {
            foreach (ValidationInfo info in _validationInfo)
            {
                info.AssertType(GetObjectToValidate(classToValidate, info));
            }
        }

        public void AssertExpectedVariables<T>(object classToValidate)
        {
            foreach (ValidationInfo info in _validationInfo)
            {
                info.AssertType(GetObjectToValidate<T>(classToValidate, info));
            }
        }

        private object GetObjectToValidate(object obj, ValidationInfo validationInfo) =>
            validationInfo.FieldInfo(obj).GetValue(obj);

        private object GetObjectToValidate<T>(object obj, ValidationInfo validationInfo) =>
            validationInfo.FieldInfo<T>().GetValue(obj);
    }
}