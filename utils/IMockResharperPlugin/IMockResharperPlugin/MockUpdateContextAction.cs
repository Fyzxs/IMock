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
using JetBrains.Diagnostics;

//using JetBrains.ReSharper.Feature.Services.Intentions.Impl.LanguageSpecific.Finders;

namespace Fyzxs.IMockResharperPlugin
{

    [ContextAction(Name = "UpdateMock", Description = "Update Mock Builder for an Interface", Group = "C#")]
    public class MockUpdateContextAction : ContextActionBase
    {
        [NotNull] private readonly ICSharpContextActionDataProvider _dataProvider;

        public MockUpdateContextAction([NotNull] ICSharpContextActionDataProvider dataProvider) => _dataProvider = dataProvider;

        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            IClassLikeDeclaration classDeclaration = _dataProvider.GetSelectedElement<IClassLikeDeclaration>();
            if (classDeclaration == null) throw new Exception("they took away the NotNull... now I have to throw. classDeclaration");
            IEnumerable<IInterface> interfaces = classDeclaration.SuperTypes.Select(x => x.GetTypeElement()).OfType<IInterface>();
            classDeclaration.RemoveDeclarationsRange(classDeclaration.GetAllDeclarationsRange());

            return new BuildMockClassContents().ExecutePsiTransaction(_dataProvider, solution, classDeclaration, interfaces);
        }

        public override string Text => "Update Mock";

        public override bool IsAvailable(IUserDataHolder cache)
        {
            IClassLikeDeclaration element = _dataProvider.GetSelectedElement<IClassLikeDeclaration>();
            if (element == null) return false;
            if (element.SuperTypes.IsEmpty()) return false;

            return IsMockClass(element);
        }

        private bool IsMockClass(IClassLikeDeclaration element)
        {
            string elementName = element.DeclaredName;
            string interfaceMockName = "Mock" + element.SuperTypes.Select(x => x.GetTypeElement()).OfType<IInterface>().First().ShortName.Substring(1);

            return elementName == interfaceMockName;
        }
    }
}
