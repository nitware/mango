using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Microsoft.Reporting.WebForms;
using Mango.Web.MangoSvc;

namespace Mango.Web.ReportPresenters
{
    public partial class appraisalReport : System.Web.UI.Page
    {
        private  ServiceClient service;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";

            if (!IsPostBack)
            {
                PopulatePeriodDropDown();
            }
        }

        protected void btnDisplayReport_Click(object sender, EventArgs e)
        {
            DisplayReport();
        }

        private void DisplayReport()
        {
            //AppraisalSvc.Fault fault = null;

            try
            {
                if (NoPeriodSelected())
                {
                    return;
                }

                service = new ServiceClient();

                
                int periodId = Convert.ToInt32(ddlPeriod.SelectedValue);
                string periodName = ddlPeriod.SelectedItem.Text;
               
                Period period = new Period() { Id = periodId };


                List<AppraisalReport> appraisalReports = service.GetAppraisalReportByPeriod(periodId);
                //if (fault != null)
                //{
                //    lblMessage.Text = "Service exception occurred! " + fault.Message;
                //    return;
                //}

                string bind_dsAppraisalReport = "dsAppraisalReport";
                //string reportPath = @"Reports\AppraisalReport.rdlc";

                rv.Reset();
                rv.LocalReport.DisplayName = "Appraisal Report";
                rv.LocalReport.ReportPath = @"Reports\AppraisalReport.rdlc"; ;

                //ReportParameter periodIdParam = new ReportParameter("PeriodId", periodId.ToString());
                ReportParameter periodNameParam = new ReportParameter("PeriodName", periodName);

                ReportParameter[] reportParams = new ReportParameter[] { periodNameParam };
                rv.LocalReport.SetParameters(reportParams);

                if (appraisalReports != null)
                {
                    rv.ProcessingMode = ProcessingMode.Local;
                    rv.LocalReport.DataSources.Add(new ReportDataSource(bind_dsAppraisalReport.Trim(), appraisalReports));
                    rv.LocalReport.Refresh();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred!  " + ex.Message;
            }
        }

        private void PopulatePeriodDropDown()
        {
            //Fault fault = null;

            try
            {
                service = new ServiceClient();

                List<Period> periods = service.GetAllPeriods();
                if (periods != null && periods.Count > 0)
                {
                    periods.Insert(0, new Period() { Name = "<< Select Period >>" });

                    ddlPeriod.DataValueField = "Id";
                    ddlPeriod.DataTextField = "Name";
                    ddlPeriod.DataSource = periods;
                    ddlPeriod.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred!  " + ex.Message;
            }
        }

        //protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Fault fault = null;

        //    try
        //    {
        //        if (NoCompanySelected())
        //        {
        //            return;
        //        }

        //        service = new MobakSvc.ServiceClient();

        //        int companyId = Convert.ToInt32(ddlCompany.SelectedValue);
        //        Company company = new Company() { Id = companyId };

        //        List<WarrantPay> warrantPays = service.GetWarrantPayByCompany(out fault, company);
        //        if (warrantPays != null)
        //        {
        //            if (warrantPays.Count > 0)
        //            {
        //                warrantPays.Insert(0, new WarrantPay() { Description = "<< Select Warrant Pay >>" });
        //            }
        //            else
        //            {
        //                warrantPays.Insert(0, new WarrantPay() { Description = "<< No Warrant Pay Found >>" });
        //            }

        //            ddlWarrantPay.DataValueField = "Id";
        //            ddlWarrantPay.DataTextField = "Description";
        //            ddlWarrantPay.DataSource = warrantPays;
        //            ddlWarrantPay.DataBind();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = "Error occurred!  " + ex.Message;
        //    }
        //}

        private bool NoPeriodSelected()
        {
            try
            {
                if (Convert.ToInt32(ddlPeriod.SelectedValue) <= 0)
                {
                    lblMessage.Text = "No period selected! Please select period";
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      


    }
}