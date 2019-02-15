using Fyzxs.IMockResharperPlugin.FluentTypes.Texts;
using JetBrains.ReSharper.Psi;
using JetBrains.Util;
using System.Linq;

namespace Fyzxs.IMockResharperPlugin.MockClassVariables
{
    public class MethodName : IMethodName
    {
        private readonly IMethod _method;
        private readonly IInterface _theInterface;
        private readonly PsiLanguageType _languageType = Languages.Instance.GetLanguageByName("CSHARP");

        public MethodName(IMethod method, IInterface theInterface)
        {
            _method = method;
            _theInterface = theInterface;
        }

        public string Actual() => _method.ShortName;
        public string Unique() => Actual() + Suffix();
        public string CamelCaseUnique() => new TextOf(Unique()).CamelCase();

        private string Suffix()
        {
            string sameNameSuffix = _method.Parameters
                .AggregateString("", (builder, param) => builder.Append(new UppercaseFirstText(new TextOf(param.Type.GetPresentableName(_languageType)))));
            bool hasSameName = _theInterface.Methods.Count(m => m.ShortName == _method.ShortName) > 1;
            string suffix = hasSameName
                ? sameNameSuffix
                : "";
            return suffix;
        }
    }

    public class MockActionBuilderMethods : IMockBuilderMethods
    {
        private readonly IMethodName _methodName;

        public MockActionBuilderMethods(IMethod methodDeclaration, IInterface theInterface) :
            this(new MethodName(methodDeclaration, theInterface))
        { }

        protected MockActionBuilderMethods(IMethodName methodName) => _methodName = methodName;

        public string Typed() => TheMethod("", "");
        public string Lambdad() => TheMethod("params Action[] actions", "actions");

        private string TheMethod(string def, string arg) => $@"public Builder {_methodName.Unique()}({def}){{
                _{_methodName.CamelCaseUnique()}.UpdateInvocation({arg});
                return this;
            }}";
    }

    public class MockResponseBuilderMethods : IMockBuilderMethods
    {
        private readonly IMethodName _methodName;
        private readonly IResponseType _responseType;

        public MockResponseBuilderMethods(IMethod methodDeclaration, IInterface theInterface) :
            this(new MethodName(methodDeclaration, theInterface), new ResponseType(methodDeclaration))
        { }

        protected MockResponseBuilderMethods(IMethodName methodName, IResponseType responseType)
        {
            _methodName = methodName;
            _responseType = responseType;
        }
        public string Typed() => TheMethod(_responseType.Type());
        public string Lambdad() => TheMethod($"Func<{_responseType.Type()}>");

        private string TheMethod(string type) => $@"public Builder {_methodName.Unique()}(params {type}[] responseValues){{
                _{_methodName.CamelCaseUnique()}.UpdateInvocation(responseValues);
                return this;
            }}";
    }

    public interface IMockBuilderMethods
    {
        string Typed();
        string Lambdad();
    }
}