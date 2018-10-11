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
using Mango.Infrastructure.MangoService;

namespace Mango.Appraisal.Services
{
    public class LearningService : ILearningService
    {
        private  ServiceClient service;

        //public event EventHandler ActionCompleted;
        public event EventHandler GetLearnigByStaffAndPeriodCompleted;

        public Learning Learning { get; set; }
        public bool Done { get; set; }

        public void GetByStaffAndPeriod(string staffId, int periodId)
        {
            try
            {
                service = new ServiceClient();
                service.GetLearningByStaffAndPeriodCompleted += new EventHandler<GetLearningByStaffAndPeriodCompletedEventArgs>(service_GetLearningByStaffAndPeriodCompleted);
                service.GetLearningByStaffAndPeriodAsync(staffId, periodId);
                service.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void service_GetLearningByStaffAndPeriodCompleted(object sender, GetLearningByStaffAndPeriodCompletedEventArgs e)
        {
            try
            {
                Learning = e.Result;

                if (GetLearnigByStaffAndPeriodCompleted != null)
                {
                    GetLearnigByStaffAndPeriodCompleted(this, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
