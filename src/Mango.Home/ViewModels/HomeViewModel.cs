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

using Microsoft.Practices.Prism.Commands;
using Mango.Infrastructure.Models;

namespace Mango.Home.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            //ReturnMoneyCommand = new DelegateCommand(OnReturnMoneyCommand, CanManageReturnMoney);
            //UsersCommand = new DelegateCommand(OnUsersCommand, CanManageUser);
        }

        public DelegateCommand ReturnMoneyCommand { get; private set; }
        public DelegateCommand MailCommand { get; private set; }
        public DelegateCommand UsersCommand { get; private set; }

        //private bool CanManageReturnMoney()
        //{
        //    if (Utility.LoggedInUser != null)
        //    {
        //        return Utility.LoggedInUser.Role.UserRight.CanManageReturnMoney;
        //    }

        //    return false;
        //}
        //private bool CanManageMail()
        //{
        //    if (Utility.LoggedInUser != null)
        //    {
        //        return Utility.LoggedInUser.Role.UserRight.CanPostMail;
        //    }

        //    return false;
        //}
        //private bool CanManageUser()
        //{
        //    if (Utility.LoggedInUser != null)
        //    {
        //        return Utility.LoggedInUser.Role.UserRight.CanManageUser;
        //    }

        //    return false;
        //}

        private void OnReturnMoneyCommand()
        {
            //RequestHomeView view = container.Resolve<RequestHomeView>();
            //RequestMenuView menuView = container.Resolve<RequestMenuView>();

            //ChangeMenu(menuView);
            //ChangeContent(view);
        }
        //private void OnMailCommand()
        //{
        //    MailHomeView view = container.Resolve<MailHomeView>();
        //    MailMenuView menuView = container.Resolve<MailMenuView>();

        //    ChangeMenu(menuView);
        //    ChangeContent(view);
        //}
        //private void OnUsersCommand()
        //{
        //    UserHomeView view = container.Resolve<UserHomeView>();
        //    UserMenuView menuView = container.Resolve<UserMenuView>();

        //    ChangeMenu(menuView);
        //    ChangeContent(view);


        //}


    }


}
