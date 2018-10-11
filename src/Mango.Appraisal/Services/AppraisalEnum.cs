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

namespace Mango.Appraisal.Services
{
    public class AppraisalEnum
    {
        public enum State
        {
            InitialLoading = 0,

            UnAppraisedStaff = 1,
            IncompletedStaffAppraisal = 2,
            CompletedStaffAppraisal = 3,
            UnAppraisedSupervisor = 4,
            IncompletedSupervisorAppraisal = 5,
            CompletedSupervisorAppraisal = 6,
            UnAppraisedHod = 7,
            IncompletedHodAppraisal = 8,
            CompletedHodAppraisal = 9,

            SupervisorLodingAppraisee = 10,
            HodLoadingSecondLevelAppraisee = 11,
            AppraisalCompleted = 12,
            IncompleteAppraisal = 13,

            StaffLoaded = 14,
            SupervisorLoaded = 15,
            HodLoaded = 16,

            HodSecondLevelAppraiseeAppraisal


        }


    }
}
