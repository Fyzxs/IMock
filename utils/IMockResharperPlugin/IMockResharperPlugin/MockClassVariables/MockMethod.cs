using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace Fyzxs.IMockResharperPlugin.MockClassVariables
{
    public class MockMethod : ISignature
    {
        private readonly PsiLanguageType _languageType = Languages.Instance.GetLanguageByName("CSHARP");
        private readonly IMethodDeclaration _methodDeclaration;
        private readonly IMethodName _methodName;
        private readonly IMethodArgs _methodArgs;

        public MockMethod(IMethodDeclaration methodDeclaration, IClassLikeDeclaration theInterface) :
            this(methodDeclaration, new MethodName(methodDeclaration, theInterface), new MethodArgs(methodDeclaration))
        { }

        public MockMethod(IMethodDeclaration methodDeclaration, IMethodName methodName, IMethodArgs methodArgs)
        {
            _methodDeclaration = methodDeclaration;
            _methodName = methodName;
            _methodArgs = methodArgs;
        }
        public string Signature() =>
            $"public {ResponseType()} {_methodName.Actual()}() => " +
            $"_{_methodName.CamelCaseUnique()}.{InvokeMethod()}();";

        private string InvokeMethod() => "Invoke" + (IsAnyTask(_methodDeclaration.Type) ? "Task" : "");
        private bool IsAnyTask(IType type) => type.IsTask() || type.IsGenericTask();
        private string ResponseType() => _methodDeclaration.Type.GetPresentableName(_languageType);
    }
}

