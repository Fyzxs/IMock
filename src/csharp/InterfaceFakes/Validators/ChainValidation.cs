﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace InterfaceFakes.Validators
{
    /// <summary>
    /// Perform type validation against the expected link order in a Chain of Responsibility implementation.
    /// <example>
    /// Usage:
    /// <code>
    /// new ChainValidation().Add&lt;ExpectedClass>().Add&lt;ExpectedOther>("_variableName").AssertExpectedChain(new HeadOfChainBeingTested());
    /// </code>
    /// </example>
    /// </summary>
    public sealed class ChainValidation
    {
        private readonly List<ValidationInfo> _validationInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChainValidation"/> class.
        /// </summary>
        public ChainValidation() : this(new List<ValidationInfo>()) { }

        private ChainValidation(List<ValidationInfo> validationInfo) => _validationInfo = validationInfo;

        /// <summary>
        /// Adds the next class of the chain to be validated.
        /// </summary>
        /// <typeparam name="T">The class expected to be next in the chain.</typeparam>
        /// <param name="name">The name of the private variable the instance is held in. Default value of "_nextAction".</param>
        /// <returns>This instance of ChainValidation</returns>
        public ChainValidation NextClassInChain<T>(string name = "_nextAction")
        {
            _validationInfo.Add(new ValidationInfo(name, typeof(T)));
            return this;
        }

        /// <summary>
        /// Obsolete - Use  <see cref="NextClassInChain{T}"/>
        /// </summary>
        [Obsolete("Use NextClassInChain")]
        public ChainValidation Add<T>(string name = "_nextAction") => NextClassInChain<T>(name);

        /// <summary>
        /// Validates the chain specified through the <see cref="NextClassInChain{T}"/> method.
        /// </summary>
        /// <param name="headToValidate">The instance to start validation against.</param>
        public void AssertExpectedChainOrder(object headToValidate)
        {
            object currentLink = headToValidate;
            foreach (ValidationInfo info in _validationInfo)
            {
                object nextLink = NextLink(currentLink, info);
                info.AssertType(nextLink);
                currentLink = nextLink;
            }
        }

        /// <summary>
        /// Obsolete - Use  <see cref="AssertExpectedChainOrder"/>
        /// </summary>
        [Obsolete("Use AssertExpectedChainOrder")]
        public void AssertExpectedChain(object headToValidate) => AssertExpectedChainOrder(headToValidate);

        private object NextLink(object obj, ValidationInfo info) => GetAllFields(obj.GetType()).First(t => info.NameMatches(t.Name)).GetValue(obj);

        private IEnumerable<FieldInfo> GetAllFields(Type t)
        {
            if (t == null) return Enumerable.Empty<FieldInfo>();

            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic |
                                       BindingFlags.Static | BindingFlags.Instance |
                                       BindingFlags.DeclaredOnly;
            return t.GetFields(flags).Concat(GetAllFields(t.GetTypeInfo().BaseType));
        }
    }
}