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
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Mango.Infrastructure.Services;
using Mango.Users.Services;
using Mango.Users.View;
using Mango.Users.Views;
using Mango.Infrastructure.Models;
using Mango.Infrastructure.Interfaces;
using Mango.Infrastructure.MangoService;
using Mango.Staff.Views;
using Mango.Staff.Interfaces;
using Mango.Users.Interfaces;
using Mango.Staff.Services;

namespace Mango.Users
{
    public class StaffModule : IModule
    {
        private IUnityContainer container;
        private IRegionManager regionManager;
        
        public StaffModule(IRegionManager _regionManager, IUnityContainer _container)
        {
            regionManager = _regionManager;
            container = _container;
        }

        public void Initialize()
        {
            container.RegisterType<ISetupService<Infrastructure.MangoService.Staff>, StaffService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISetupService<Role>, RoleService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISetupService<Right>, RightService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAssignRightToRoleService, AssignRightToRoleService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILoginService, LoginService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICurrentPeriodService, CurrentPeriodService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILoginDetailService, LoginDetailService>(new ContainerControlledLifetimeManager());
            
            regionManager.RegisterViewWithRegion(RegionContainer.Content, typeof(LoginView));

            //regionManager.RegisterViewWithRegion(RegionContainer.SubMenu, typeof(StaffMenuView));
            regionManager.RegisterViewWithRegion(RegionContainer.StaffTab, typeof(StaffView));
            regionManager.RegisterViewWithRegion(RegionContainer.StaffTab, typeof(LoginDetailsView));
            regionManager.RegisterViewWithRegion(RegionContainer.StaffTab, typeof(ManageRoleView));
            regionManager.RegisterViewWithRegion(RegionContainer.StaffTab, typeof(ManageRightView));
            regionManager.RegisterViewWithRegion(RegionContainer.StaffTab, typeof(AssignRightToRoleView));
            regionManager.RegisterViewWithRegion(RegionContainer.StaffTab, typeof(AssignRoleToPersonView));
        }



    }
}
