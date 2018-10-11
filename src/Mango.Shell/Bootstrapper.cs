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
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Prism.Modularity;
using Mango.Home;
using Mango.Users;
using Mango.Appraisal;
using Mango.Setup;
//using Mango.Vote;

namespace Mango.Shell
{
    public class Bootstrapper : UnityBootstrapper
    {
         //create an instance of shell window and return it
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        //display shell window to user
        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.RootVisual = (UIElement)this.Shell;
        }

        //populate the module catalogue with modules
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            //defined modules
            Type homeModule = typeof(HomeModule);
            Type staffModule = typeof(StaffModule);
            Type appraisalModule = typeof(AppraisalModule);
            Type setupModule = typeof(SetupModule);
                                            
            //add modules to catalog
            this.ModuleCatalog.AddModule(new ModuleInfo() { ModuleName = staffModule.Name, ModuleType = staffModule.AssemblyQualifiedName, InitializationMode = InitializationMode.WhenAvailable });
            this.ModuleCatalog.AddModule(new ModuleInfo() { ModuleName = homeModule.Name, ModuleType = homeModule.AssemblyQualifiedName, InitializationMode = InitializationMode.WhenAvailable });
            this.ModuleCatalog.AddModule(new ModuleInfo() { ModuleName = appraisalModule.Name, ModuleType = appraisalModule.AssemblyQualifiedName, InitializationMode = InitializationMode.WhenAvailable });
            this.ModuleCatalog.AddModule(new ModuleInfo() { ModuleName = setupModule.Name, ModuleType = setupModule.AssemblyQualifiedName, InitializationMode = InitializationMode.WhenAvailable });
        }



    }
}
