using JetBrains.Annotations;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.ContextActions;
using JetBrains.ReSharper.Feature.Services.CSharp.Analyses.Bulbs;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.TextControl;
using JetBrains.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fyzxs.IFakeResharperPlugin
{
    [ContextAction(Name = "CreateFake", Description = "Create Fake Builder for an Interface", Group = "C#")]
    public class FakeCreateContextAction : ContextActionBase
    {
        [NotNull]
        private readonly ICSharpContextActionDataProvider _dataProvider;

        public FakeCreateContextAction([NotNull] ICSharpContextActionDataProvider dataProvider) => _dataProvider = dataProvider;

        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            IInterfaceDeclaration theInterface = _dataProvider.GetSelectedElement<IInterfaceDeclaration>().NotNull();
            IInterface typeElement = theInterface.DeclaredElement;
            string typeParameters = typeElement.TypeParameters.AggregateString(",", (builder, parameter) => builder.Append(parameter.ShortName));
            if (typeElement.TypeParameters.Count != 0) typeParameters = "<" + typeParameters + ">";
            string interfaceName = typeElement.ShortName;
            string className = $"Fake{interfaceName.Substring(1)}";
            IClassLikeDeclaration classDeclaration = (IClassLikeDeclaration)_dataProvider.ElementFactory.CreateTypeMemberDeclaration($"public sealed partial class {className}{typeParameters} : {interfaceName}{typeParameters} {{}}");

            ICSharpTypeAndNamespaceHolderDeclaration holderDeclaration = _dataProvider.PsiFile;
            classDeclaration = (IClassLikeDeclaration)holderDeclaration.AddTypeDeclarationAfter(classDeclaration, theInterface);

            //
            //IEnumerable<IInterface> interfaces = _dataProvider.GetSelectedElement<IClassLikeDeclaration>().NotNull().SuperTypes.Select(x => x.GetTypeElement()).OfType<IInterface>();
            IEnumerable<IInterface> interfaces = classDeclaration.SuperTypes.Select(x => x.GetTypeElement()).OfType<IInterface>();
            //
            return new BuildFakeClassContents().ExecutePsiTransaction(_dataProvider, solution, classDeclaration, interfaces);
        }

        public override string Text => "Create Fake";

        public override bool IsAvailable(IUserDataHolder cache) => _dataProvider.GetSelectedElement<IInterfaceDeclaration>() != null;
    }

}


