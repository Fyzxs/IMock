using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.VB.Util;

namespace Fyzxs.IMockResharperPlugin.MockClassVariables {
    public class ResponseType : IResponseType
    {
        private readonly PsiLanguageType _languageType = Languages.Instance.GetLanguageByName("CSHARP");
        private readonly IMethodDeclaration _methodDeclaration;

        public ResponseType(IMethodDeclaration methodDeclaration) => _methodDeclaration = methodDeclaration;

        public string Type() => _methodDeclaration.Type.IsGenericTask()
            ? _methodDeclaration.Type.GetTaskUnderlyingType().GetPresentableName(_languageType)
            : _methodDeclaration.Type.GetPresentableName(_languageType);
    }
}