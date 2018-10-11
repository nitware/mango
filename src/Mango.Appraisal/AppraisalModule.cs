using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using Mango.Infrastructure.Models;
using Mango.Appraisal.Views;
using Mango.Appraisal.Services;

namespace Mango.Appraisal
{
    public class AppraisalModule : IModule
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        public AppraisalModule(IRegionManager _regionManager, IUnityContainer _container)
        {
            container = _container;
            regionManager = _regionManager;
        }

        public void Initialize()
        {
            //container.RegisterType<IPeriodSetupService, PeriodSetupService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAppraisalService, AppraisalService>(new ContainerControlledLifetimeManager());

            //regionManager.RegisterViewWithRegion(RegionContainer.Content, typeof(AppraisalView));
            //regionManager.RegisterViewWithRegion(RegionContainer.SubMenu, typeof(AppraisalMenuView));

        }

    }


}
