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

namespace Mango.Setup.ViewModels
{
    public class AppraisalReportViewModel
    {
        public AppraisalReportViewModel()
        {
            AppraisalDetailAnalysisReportCommand = new DelegateCommand(OnAppraisalDetailAnalysisReportCommand);
            AppraisalDetailAnalysisWithAllCommentReportCommand = new DelegateCommand(OnAppraisalDetailAnalysisWithAllCommentReportCommand);
        }

        //public string RootWebAddress { get; set; }
        public DelegateCommand AppraisalDetailAnalysisReportCommand { get; private set; }
        public DelegateCommand AppraisalDetailAnalysisWithAllCommentReportCommand { get; private set; }

        private void OnAppraisalDetailAnalysisReportCommand()
        {
            Utility.DisplayReport(Utility.RootWebAddress + "/ReportPresenters/appraisalReport.aspx");
        }
        public void OnAppraisalDetailAnalysisWithAllCommentReportCommand()
        {
            Utility.DisplayReport(Utility.RootWebAddress + "/ReportPresenters/appraisalReport2.aspx");
        }

    }
}
