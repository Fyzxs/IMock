using JetBrains.Annotations;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.ContextActions;
using JetBrains.ReSharper.Feature.Services.CSharp.Analyses.Bulbs;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Util;
using JetBrains.TextControl;
using JetBrains.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using FindExecution = JetBrains.ReSharper.Psi.Search.FindExecution;

//using JetBrains.ReSharper.Feature.Services.Intentions.Impl.LanguageSpecific.Finders;

namespace Fyzxs.IMockResharperPlugin
{

    [ContextAction(Name = "UpdateMock", Description = "Update Mock Builder for an Interface", Group = "C#")]
    public class MockUpdateContextAction : ContextActionBase
    {
        [NotNull]
        private readonly ICSharpContextActionDataProvider _dataProvider;

        public MockUpdateContextAction([NotNull] ICSharpContextActionDataProvider dataProvider) => _dataProvider = dataProvider;

        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            IClassLikeDeclaration classDeclaration = _dataProvider.GetSelectedElement<IClassLikeDeclaration>().NotNull();

            //Trying to get IClassLikeDeclaration of the interface to build Mock/Builder from
            IDeclaredType theInterface = classDeclaration.SuperTypes.First(s => s.IsInterfaceType());

            IDeclaredElement baseElement = solution.GetPsiServices().Finder.FindImmediateBaseElements(classDeclaration.DeclaredElement, NullProgressIndicator.Create())[1];
            FindResultConsumer<IClassLikeDeclaration> findResultConsumer = new FindResultConsumer<IClassLikeDeclaration>(m => null, f => FindExecution.Stop);
            solution.GetPsiServices()
                .Finder.Find<IClassLikeDeclaration>(new List<IDeclaredElement> { baseElement }, SearchDomainFactory.Instance.CreateSearchDomainOfModuleAndItsReferences(_dataProvider.PsiModule), findResultConsumer, SearchPattern.FIND_CANDIDATES, progress);


            return new BuildMockClassContents().ExecutePsiTransaction(_dataProvider, solution, classDeclaration, null);
        }

        public override string Text => "Update Mock";

        public override bool IsAvailable(IUserDataHolder cache) => _dataProvider.GetSelectedElement<IClassLikeDeclaration>()?.SuperTypes.Any(s => s.IsInterfaceType()) != null;
    }


}
