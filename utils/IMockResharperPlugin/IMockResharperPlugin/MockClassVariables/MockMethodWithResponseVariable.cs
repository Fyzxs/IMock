using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace Fyzxs.IMockResharperPlugin.MockClassVariables
{
    public class MockMethodWithResponseVariable : MockVariable
    {
        private readonly IResponseType _responseType;

        public MockMethodWithResponseVariable(IMethodDeclaration methodDeclaration, IClassLikeDeclaration theInterface) :
            this(methodDeclaration, theInterface, new ResponseType(methodDeclaration))
        { }

        public MockMethodWithResponseVariable(IMethodDeclaration methodDeclaration, IClassLikeDeclaration theInterface, IResponseType responseType) :
            base(methodDeclaration, theInterface) => _responseType = responseType;

        protected override string TypeDeclaration() => $"MockMethodWithResponse<{_responseType.Type()}>";
    }
}