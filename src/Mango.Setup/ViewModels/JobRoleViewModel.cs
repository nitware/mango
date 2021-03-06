﻿using System;
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
using Mango.Infrastructure.MangoService;
using Mango.Infrastructure.Interfaces;
using Mango.Infrastructure.Models;

namespace Mango.Setup.ViewModels
{
    public class JobRoleViewModel : SetupViewModelBase<JobRole>
    {
        public JobRoleViewModel(ISetupService<JobRole> _service) : base(_service)
        {
            modelName = "Job Role";
            Initialize();

            IsLoggedInUserHasRight = Utility.LoggedInUser.Role.UserRight.CanSetupJobRole;

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
