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

using Mango.Infrastructure.ViewModelBase;
using Mango.Infrastructure.Interfaces;
using Mango.Infrastructure.Models;
using Mango.Infrastructure.MangoService;

namespace mobak.Users.ViewModels
{
    public class ManageRoleViewModel : SetupViewModelBase<Role>
    {
        public ManageRoleViewModel(ISetupService<Role> _service)
            : base(_service)
        {
            modelName = "Role";
            Initialize();

            base.addSelector = l => l.Name.Equals(Model.Name, StringComparison.OrdinalIgnoreCase);
            base.modifySelector = l => l.Name.Equals(Model.Name, StringComparison.OrdinalIgnoreCase) && l.Id != Model.Id;
        }

        public string TabCaption
        {
            get { return modelName; }
        }

        protected override void OnSaveCommand()
        {
            try
            {
                if (base.InvalidEntry(Model.Name))
                {
                    return;
                }

                base.OnSaveCommand();
            }
            catch (Exception ex)
            {
                Utility.DisplayMessage(ex.Message);
            }
        }




    }
}
