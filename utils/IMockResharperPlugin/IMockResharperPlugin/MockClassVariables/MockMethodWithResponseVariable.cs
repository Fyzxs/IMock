using JetBrains.ReSharper.Psi;

namespace Fyzxs.IMockResharperPlugin.MockClassVariables
{
    public class MockMethodWithResponseVariable : MockVariable
    {
        private readonly IResponseType _responseType;

        public MockMethodWithResponseVariable(IMethod methodDeclaration, IInterface theInterface) :
            this(methodDeclaration, theInterface, new ResponseType(methodDeclaration))
        { }

        public MockMethodWithResponseVariable(IMethod methodDeclaration, IInterface theInterface, IResponseType responseType) :
            base(methodDeclaration, theInterface) => _responseType = responseType;

        protected override string TypeDeclaration() => $"MockMethodWithResponse<{_responseType.Type()}>";
    }
}