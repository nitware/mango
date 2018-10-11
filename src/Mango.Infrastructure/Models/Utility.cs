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
using System.Windows.Browser;

using Mango.Infrastructure.MangoService;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mango.Infrastructure.Models
{
    public class Utility
    {
        public static string RootWebAddress;

        public static Staff LoggedInUser
        {
            get;
            set;
        }

        public static Period Period { get; set; } 

        public static void DisplayReport(string url)
        {
            System.Text.StringBuilder codeToRun = new System.Text.StringBuilder();
            codeToRun.Append("window.open(");
            codeToRun.Append("\"");
            codeToRun.Append(string.Format("{0}", url));
            codeToRun.Append("\",");
            codeToRun.Append("\"");
            codeToRun.Append("\",");
            codeToRun.Append("\"");
            codeToRun.Append("width=" + 1000 + ",height=" + 800);
            codeToRun.Append(",scrollbars=yes,menubar=no,toolbar=no,resizable=yes");
            codeToRun.Append("\");");

            try
            {
                HtmlPage.Window.Eval(codeToRun.ToString());
            }
            catch
            {
                MessageBox.Show("You must enable popups to view reports. Safari browser is not supported.", "Error", MessageBoxButton.OK);
            }
        }

        public static void DisplayMessage(string message)
        {
            MessageBox.Show(message);
        }

        public static bool FaultExist(Fault fault)
        {
            if (fault != null)
            {
                DisplayMessage(fault.Message);
                return true;
            }

            return false;
        }

        public static ObservableCollection<Value> CreateYearListFrom(int startYear)
        {
            try
            {
                //get current year from the system
                DateTime currentDate = DateTime.Now;
                int currentYear = currentDate.Year;

                ObservableCollection<Value> years = new ObservableCollection<Value>();
                for (int i = startYear; i <= currentYear; i++)
                {
                    Value value = new Value();
                    value.Id = i;
                    value.Name = i.ToString();
                    years.Add(value);
                }

                years.Insert(0, new Value() { Id = 0, Name = "<< Select Year >>" });
                return years;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }


}
