using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.VB.Util;

namespace Fyzxs.IFakeResharperPlugin.FakeClassVariables
{
    public class ResponseType : IResponseType
    {
        private readonly PsiLanguageType _languageType = Languages.Instance.GetLanguageByName("CSHARP");
        private readonly IMethod _method;

        public ResponseType(IMethod methodDeclaration) => _method = methodDeclaration;

        public string Type() => _method.ReturnType.IsGenericTask()
            ? _method.ReturnType.GetTaskUnderlyingType().GetPresentableName(_languageType)
            : _method.ReturnType.GetPresentableName(_languageType);
    }
}