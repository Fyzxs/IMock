using JetBrains.ReSharper.Psi;

namespace Fyzxs.IFakeResharperPlugin.FakeClassVariables
{
    public class FakeMethodWithParamAndResponseVariable : FakeVariable
    {
        private readonly IMethodArgs _methodArgs;
        private readonly IResponseType _responseType;

        public FakeMethodWithParamAndResponseVariable(IMethod methodDeclaration, IInterface theInterface) :
            this(methodDeclaration, theInterface, new MethodArgs(methodDeclaration), new ResponseType(methodDeclaration))
        { }

        public FakeMethodWithParamAndResponseVariable(IMethod methodDeclaration, IInterface theInterface, IMethodArgs methodArgs, IResponseType responseType) : base(methodDeclaration, theInterface)
        {
            _methodArgs = methodArgs;
            _responseType = responseType;
        }

        protected override string TypeDeclaration() => $"FakeMethodWithParamAndResponse<{_methodArgs.Types()}, {_responseType.Type()}>";
    }
}