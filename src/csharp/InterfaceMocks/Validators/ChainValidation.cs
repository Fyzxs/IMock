using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace InterfaceMocks.Validators
{
    public class ChainValidation
    {
        private readonly List<ValidationInfo> _validationInfo;

        public ChainValidation() : this(new List<ValidationInfo>()) { }

        public ChainValidation(List<ValidationInfo> validationInfo) => _validationInfo = validationInfo;

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

        private object NextLink(object obj, ValidationInfo info) => GetAllFields(obj.GetType()).First(t => info.NameMatches(t.Name)).GetValue(obj);

        private static IEnumerable<FieldInfo> GetAllFields(Type t)
        {
            if (t == null) return Enumerable.Empty<FieldInfo>();

            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic |
                                       BindingFlags.Static | BindingFlags.Instance |
                                       BindingFlags.DeclaredOnly;
            return t.GetFields(flags).Concat(GetAllFields(t.GetTypeInfo().BaseType));
        }
    }
}