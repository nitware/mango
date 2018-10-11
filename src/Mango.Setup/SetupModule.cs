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
using Mango.Setup.Views;
using Mango.Setup.Services;
using Mango.Setup.Interfaces;
using Mango.Infrastructure.MangoService;
using Mango.Infrastructure.Interfaces;
using Mango.Setup.Views.Upload;

namespace Mango.Setup
{
    public class SetupModule : IModule
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        public SetupModule(IRegionManager _regionManager, IUnityContainer _container)
        {
            container = _container;
            regionManager = _regionManager;
        }

        public void Initialize()
        {
            container.RegisterType<ISetupService<Period>, PeriodService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISetupService<JobRole>, JobRoleService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISetupService<Level>, LevelService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISetupService<Department>, DepartmentService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISetupService<Location>, LocationService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISetupService<StaffLevel>, StaffJobLevelService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISetupService<StaffCdjr>, StaffCdjrService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISetupService<CompanyDepartmentJobRole>, CompanyDepartmentJobRoleService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMetricBaseService<JobRoleHod>, JobRoleHodService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISetupService<CompanyDepartmentJobRole>, CompanyDepartmentJobRoleService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMetricBaseService<MetricRating>, MetricRatingService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMetricBaseService<Metrics>, MetricsService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMetricsSetupService, MetricsSetupService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISetupService<Infrastructure.MangoService.Rating>, RatingService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISetupService<RatingType>, RatingTypeService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISetupService<MetricsPerspective>, MetricsPerspectiveService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IStatusService, StatusService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMetricBaseService<JobRoleSupervisor>, JobRoleSupervisorService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISetupService<PeriodType>, PeriodTypeService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISetupService<Company>, CompanyService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISetupService<CompanyDepartmentJobRole>, CompanyDepartmentJobRoleService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISetupService<StaffLearning>, StaffLearningService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISetupService<StaffLocation>, StaffLocationService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUploadService, UploadService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISetupService<Inps>, InpsService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMetricBaseService<InpsRating>, InpsRatingService>(new ContainerControlledLifetimeManager());

            regionManager.RegisterViewWithRegion(RegionContainer.SubMenu, typeof(SetupMenuView));
            regionManager.RegisterViewWithRegion(RegionContainer.SetupTab, typeof(JobRoleView));
            regionManager.RegisterViewWithRegion(RegionContainer.SetupTab, typeof(JobRoleLevelView));
            regionManager.RegisterViewWithRegion(RegionContainer.SetupTab, typeof(DepartmentView));
            regionManager.RegisterViewWithRegion(RegionContainer.SetupTab, typeof(LocationView));
            regionManager.RegisterViewWithRegion(RegionContainer.SetupTab, typeof(PeriodTypeView));  

            regionManager.RegisterViewWithRegion(RegionContainer.StaffSetupTab, typeof(CompanyDepartmentJobRoleView));
            regionManager.RegisterViewWithRegion(RegionContainer.StaffSetupTab, typeof(StaffJobLevelView));
            regionManager.RegisterViewWithRegion(RegionContainer.StaffSetupTab, typeof(StaffCdjrView));
            regionManager.RegisterViewWithRegion(RegionContainer.StaffSetupTab, typeof(StaffLearningView));
            regionManager.RegisterViewWithRegion(RegionContainer.StaffSetupTab, typeof(StaffLocationView));

            regionManager.RegisterViewWithRegion(RegionContainer.RoleSetupTab, typeof(JobRoleSupervisorView));
            regionManager.RegisterViewWithRegion(RegionContainer.RoleSetupTab, typeof(JobRoleHodView));

            regionManager.RegisterViewWithRegion(RegionContainer.MetricsSetupTab, typeof(MetricsView));
            regionManager.RegisterViewWithRegion(RegionContainer.MetricsSetupTab, typeof(ModifyMetricsView));
            regionManager.RegisterViewWithRegion(RegionContainer.MetricsSetupTab, typeof(ModifyAllMetricsView));
            regionManager.RegisterViewWithRegion(RegionContainer.MetricsSetupTab, typeof(MetricRatingView));
            
            regionManager.RegisterViewWithRegion(RegionContainer.PeriodSetupTab, typeof(CurrentPeriodView));
            regionManager.RegisterViewWithRegion(RegionContainer.PeriodSetupTab, typeof(ModifyPeriodView));
            regionManager.RegisterViewWithRegion(RegionContainer.PeriodSetupTab, typeof(NewPeriodView));

            regionManager.RegisterViewWithRegion(RegionContainer.UploadTab, typeof(UploadInpsView));
            regionManager.RegisterViewWithRegion(RegionContainer.UploadTab, typeof(SetupInpsRatingView));
            regionManager.RegisterViewWithRegion(RegionContainer.UploadTab, typeof(EditInpsView));
            

           
        }
    }



}
