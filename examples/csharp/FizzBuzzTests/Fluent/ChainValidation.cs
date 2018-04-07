using System.Collections.Generic;
using System.Reflection;

namespace FizzBuzzExampleTests.Fluent {
    public class ChainValidation
    {
        private readonly List<ValidationInfo> _validationInfo;

        public ChainValidation() : this(new List<ValidationInfo>()) { }

        private ChainValidation(List<ValidationInfo> validationInfo) => _validationInfo = validationInfo;

        public ChainValidation Add<T>(string name = "_nextAction")
        {
            _validationInfo.Add(new ValidationInfo(name, typeof(T)));
            return this;
        }

        public void AssertExpectedChain(object obj)
        {
            object currentLink = obj;
            foreach (ValidationInfo info in _validationInfo)
            {
                object nextLink = NextLink(currentLink, info);
                info.AssertType(nextLink);
                currentLink = nextLink;
            }
        }

        private object NextLink(object obj, ValidationInfo info)
        {
            FieldInfo fieldInfo = info.FieldInfo(obj);
            return fieldInfo.GetValue(obj);
        }
    }
}