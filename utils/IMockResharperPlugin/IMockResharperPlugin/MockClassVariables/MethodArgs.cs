using System;
using System.Text;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace Fyzxs.IMockResharperPlugin.MockClassVariables {
    public class MethodArgs : IMethodArgs
    {
        private readonly IMethodDeclaration _methodDeclaration;
        private readonly PsiLanguageType _languageType = Languages.Instance.GetLanguageByName("CSHARP");

        public MethodArgs(IMethodDeclaration methodDeclaration) => _methodDeclaration = methodDeclaration;

        public string Definition() => AggregateString((builder, param) => builder.Append($"{ParamName(param)} {param.DeclaredName}"));

        public string Types() => MultipleParams() ? TupleDefinition() : TypeDefinition();

        private string TypeDefinition() => AggregateString((builder, param) => builder.Append(ParamName(param)));

        public string InvokedArgs() => MultipleParams()
            ? TupleArgValues()
            : SingleArgValues();

        private string TupleArgValues() => $"new {TupleDefinition()}({SingleArgValues()})";

        private string TupleDefinition() => $"Tuple<{TypeDefinition()}>";

        private bool MultipleParams() => 1 < _methodDeclaration.Params.ParameterDeclarations.Count;

        private string SingleArgValues() => AggregateString((builder, param) => builder.Append(param.DeclaredName));

        private string AggregateString(Func<StringBuilder, ICSharpParameterDeclaration, StringBuilder> func) =>
            _methodDeclaration.Params.ParameterDeclarations.AggregateString(", ", func);

        private string ParamName(ITypeOwnerDeclaration param) => param.Type.GetPresentableName(_languageType);
    }
}