using JetBrains.ReSharper.Psi;

namespace Fyzxs.IMockResharperPlugin.MockClassVariables
{
    public class MockMethodWithParamAndResponseVariable : MockVariable
    {
        private readonly IMethodArgs _methodArgs;
        private readonly IResponseType _responseType;

        public MockMethodWithParamAndResponseVariable(IMethod methodDeclaration, IInterface theInterface) :
            this(methodDeclaration, theInterface, new MethodArgs(methodDeclaration), new ResponseType(methodDeclaration))
        { }

        public MockMethodWithParamAndResponseVariable(IMethod methodDeclaration, IInterface theInterface, IMethodArgs methodArgs, IResponseType responseType) : base(methodDeclaration, theInterface)
        {
            _methodArgs = methodArgs;
            _responseType = responseType;
        }

        protected override string TypeDeclaration() => $"MockMethodWithParamAndResponse<{_methodArgs.Types()}, {_responseType.Type()}>";
    }
}