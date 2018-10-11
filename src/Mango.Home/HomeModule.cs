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

using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Modularity;
using Mango.Home.Views;
using Mango.Infrastructure.Models;

namespace Mango.Home
{
    public class HomeModule : IModule
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        public HomeModule(IRegionManager _regionManager, IUnityContainer _container)
        {
            container = _container;
            regionManager = _regionManager;
        }

        public void Initialize()
        {
            regionManager.RegisterViewWithRegion(RegionContainer.Content, typeof(HomeView));
            regionManager.RegisterViewWithRegion(RegionContainer.SubMenu, typeof(HomeMenuView));

            //container.RegisterType<ISmsService, SmsService>(new ContainerControlledLifetimeManager());
        }

    }
}
