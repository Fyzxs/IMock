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

//using JetBrains.ReSharper.Feature.Services.Intentions.Impl.LanguageSpecific.Finders;

namespace Fyzxs.IFakeResharperPlugin
{

    [ContextAction(Name = "UpdateFake", Description = "Update Fake Builder for an Interface", Group = "C#")]
    public class FakeUpdateContextAction : ContextActionBase
    {
        [NotNull] private readonly ICSharpContextActionDataProvider _dataProvider;

        public FakeUpdateContextAction([NotNull] ICSharpContextActionDataProvider dataProvider) => _dataProvider = dataProvider;

        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            IClassLikeDeclaration classDeclaration = _dataProvider.GetSelectedElement<IClassLikeDeclaration>().NotNull();
            IEnumerable<IInterface> interfaces = classDeclaration.SuperTypes.Select(x => x.GetTypeElement()).OfType<IInterface>();
            classDeclaration.RemoveDeclarationsRange(classDeclaration.GetAllDeclarationsRange());

            return new BuildFakeClassContents().ExecutePsiTransaction(_dataProvider, solution, classDeclaration, interfaces);
        }

        public override string Text => "Update Fake";

        public override bool IsAvailable(IUserDataHolder cache)
        {
            IClassLikeDeclaration element = _dataProvider.GetSelectedElement<IClassLikeDeclaration>();
            if (element == null) return false;
            if (element.SuperTypes.IsEmpty()) return false;

            return IsFakeClass(element);
        }

        private bool IsFakeClass(IClassLikeDeclaration element)
        {
            string elementName = element.DeclaredName;
            string interfaceFakeName = "Fake" + element.SuperTypes.Select(x => x.GetTypeElement()).OfType<IInterface>().First().ShortName.Substring(1);

            return elementName == interfaceFakeName;
        }
    }
}
