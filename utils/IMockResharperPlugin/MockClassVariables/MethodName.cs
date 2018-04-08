using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.Util;
using MicroObjectFakesResharperPlugin.FluentTypes.Texts;
using System.Linq;

namespace MicroObjectFakesResharperPlugin.MockClassVariables
{
    public class MethodName : IMethodName
    {
        private readonly IMethodDeclaration _methodDeclaration;
        private readonly IClassLikeDeclaration _theInterface;
        private readonly PsiLanguageType _languageType = Languages.Instance.GetLanguageByName("CSHARP");

        public MethodName(IMethodDeclaration methodDeclaration, IClassLikeDeclaration theInterface)
        {
            _methodDeclaration = methodDeclaration;
            _theInterface = theInterface;
        }

        public string Actual() => _methodDeclaration.DeclaredName;
        public string Unique() => Actual() + Suffix();
        public string CamelCaseUnique() => new CamelCaseText(new TextOf(Unique()));

        private string Suffix()
        {
            string sameNameSuffix = _methodDeclaration.Params.ParameterDeclarations
                .AggregateString("", (builder, param) => builder.Append(param.Type.GetPresentableName(_languageType)));
            bool hasSameName = _theInterface.MethodDeclarations.Count(m => m.DeclaredName == _methodDeclaration.DeclaredName) > 1;
            string suffix = hasSameName
                ? sameNameSuffix
                : "";
            return suffix;
        }
    }

    public class MockActionBuilderMethods : IMockBuilderMethods
    {
        private readonly IMethodName _methodName;

        public MockActionBuilderMethods(IMethodDeclaration methodDeclaration, IClassLikeDeclaration theInterface) :
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

        public MockResponseBuilderMethods(IMethodDeclaration methodDeclaration, IClassLikeDeclaration theInterface) :
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