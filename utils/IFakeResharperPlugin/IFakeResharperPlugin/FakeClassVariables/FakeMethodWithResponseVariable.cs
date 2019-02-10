using JetBrains.ReSharper.Psi;

namespace Fyzxs.IFakeResharperPlugin.FakeClassVariables
{
    public class FakeMethodWithResponseVariable : FakeVariable
    {
        private readonly IResponseType _responseType;

        public FakeMethodWithResponseVariable(IMethod methodDeclaration, IInterface theInterface) :
            this(methodDeclaration, theInterface, new ResponseType(methodDeclaration))
        { }

        public FakeMethodWithResponseVariable(IMethod methodDeclaration, IInterface theInterface, IResponseType responseType) :
            base(methodDeclaration, theInterface) => _responseType = responseType;

        protected override string TypeDeclaration() => $"FakeMethodWithResponse<{_responseType.Type()}>";
    }
}