using JetBrains.ReSharper.Psi;

namespace Fyzxs.IMockResharperPlugin.MockClassVariables
{
    public class MockMethod : ISignature
    {
        private readonly PsiLanguageType _languageType = Languages.Instance.GetLanguageByName("CSHARP");
        private readonly IMethod _methodDeclaration;
        private readonly IMethodName _methodName;
        private readonly IMethodArgs _methodArgs;

        public MockMethod(IMethod methodDeclaration, IInterface theInterface) :
            this(methodDeclaration, new MethodName(methodDeclaration, theInterface), new MethodArgs(methodDeclaration))
        { }

        public MockMethod(IMethod methodDeclaration, IMethodName methodName, IMethodArgs methodArgs)
        {
            _methodDeclaration = methodDeclaration;
            _methodName = methodName;
            _methodArgs = methodArgs;
        }
        public string Signature() =>
            $"public {ResponseType()} {_methodName.Actual()}({_methodArgs.Definition()}) => " +
            $"_{_methodName.CamelCaseUnique()}.{InvokeMethod()}({_methodArgs.InvokedArgs()});";

        private string InvokeMethod() => "Invoke" + (IsAnyTask(_methodDeclaration.ReturnType) ? "Task" : "");
        private bool IsAnyTask(IType type) => type.IsTask() || type.IsGenericTask();
        private string ResponseType() => _methodDeclaration.ReturnType.GetPresentableName(_languageType);
    }

    public interface ISignature
    {
        string Signature();
    }
}

