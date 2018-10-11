using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Collections.ObjectModel;
using System.Data;
using Mango.DataAccess;
using Mango.Data;
using Mango.Data.Interfaces;
using Mango.Model;
using Mango.Model.Model;

namespace Mango.Business.DbFacade
{
    public class MetricDbFacade
    {
        private LearningDbFacade learningDbFacade;
        private IMetricDb metricDb;
        private IInpsDb npsDb;
        //private Transaction transaction;
        private MetricRatingDbFacade metricRatingDbFacade;
        private InpsRatingLogic npsRatingLogic;

        public MetricDbFacade()
        {
            npsDb = new InpsDb();
            metricDb = new MetricDb();
            learningDbFacade = new LearningDbFacade();
        }

        public Metrices PopulateMetrices(int companyDepartmentJobRoleId, bool isStaffAppraised, bool isSupervisor, long appraisalHeaderId, int periodId, string staffId)
        {
            try
            {
                Metrices metrices = new Metrices();
                if (isStaffAppraised)
                {
                    metrices = LoadCustomer(metrices, 1, companyDepartmentJobRoleId, appraisalHeaderId, periodId, staffId);
                    metrices = LoadFinancial(metrices, 2, companyDepartmentJobRoleId, appraisalHeaderId, periodId);
                    metrices = LoadPeople(metrices, 3, companyDepartmentJobRoleId, isSupervisor, appraisalHeaderId, periodId, staffId);
                    metrices = LoadProcess(metrices, 4, companyDepartmentJobRoleId, isSupervisor, appraisalHeaderId, periodId);
                    metrices = LoadRisk(metrices, 5, companyDepartmentJobRoleId, appraisalHeaderId, periodId);
                }
                else
                {
                    metrices = LoadDefaultCustomer(metrices, 1, companyDepartmentJobRoleId, periodId, staffId);
                    metrices = LoadDefaultFinancial(metrices, 2, companyDepartmentJobRoleId, periodId);
                    metrices = LoadDefaultPeople(metrices, 3, companyDepartmentJobRoleId, isSupervisor, periodId, staffId);
                    metrices = LoadDefaultProcess(metrices, 4, companyDepartmentJobRoleId, isSupervisor, periodId);
                    metrices = LoadDefaultRisk(metrices, 5, companyDepartmentJobRoleId, periodId);
                }

                return metrices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CountMetrics(int companyDepartmentJobRoleId, int periodId)
        {
            try
            {
                //DataSet ds = metricDb.SelectMetricTotalCountByCompanyDepartmentJobRoleId(companyDepartmentJobRoleId);

                DataSet ds = metricDb.SelectMetricTotalCountByCompanyDepartmentJobRoleIdAndPeriodID(companyDepartmentJobRoleId, periodId);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return ds.Tables[0].Rows[0][0] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Metrices LoadCustomer(Metrices metrices, int metricPerspectiveId, int companyDepartmentJobRoleId, long appraisalHeaderId, int periodId, string staffId)
        {
            try
            {
                List<Customer> metrics = new List<Customer>();
                metrices.CustomerTarget = "CUSTOMER (" + 0 + "%)";

                if (periodId < 6)
                {
                    DataSet ds = metricDb.SelectMetricByMetricPerspectiveIDAndCompanyDepartmentJobRoleIDAndAppraisalHeaderId(metricPerspectiveId, companyDepartmentJobRoleId, appraisalHeaderId);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            metrices.CustomerTargetValue = 0;
                            metrices.CustomerActualScoreTotal = 0;
                            metricRatingDbFacade = new MetricRatingDbFacade();
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                //decimal target = 0;
                                Customer metric = new Customer();
                                metric.StaffMetricId = ds.Tables[0].Rows[i][metricDb.FIELD_STAFF_METRIC_ID] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i][metricDb.FIELD_STAFF_METRIC_ID]);
                                metric.Id = ds.Tables[0].Rows[i][metricDb.FIELD_METRIC_ID] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i][metricDb.FIELD_METRIC_ID]);
                                metric.Kpi = ds.Tables[0].Rows[i][metricDb.FIELD_KPI] == DBNull.Value ? null : Convert.ToString(ds.Tables[0].Rows[i][metricDb.FIELD_KPI]);
                                metric.Measure = ds.Tables[0].Rows[i][metricDb.FIELD_MEASURE] == DBNull.Value ? null : Convert.ToString(ds.Tables[0].Rows[i][metricDb.FIELD_MEASURE]);
                                metric.Target = ds.Tables[0].Rows[i][metricDb.FIELD_TARGET] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i][metricDb.FIELD_TARGET]);
                                metric.Score = ds.Tables[0].Rows[i][metricDb.FIELD_SCORE] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i][metricDb.FIELD_SCORE]);
                                metric.Rating = ds.Tables[0].Rows[i][metricDb.FIELD_RATING] == DBNull.Value ? 0 : Convert.ToByte(ds.Tables[0].Rows[i][metricDb.FIELD_RATING]);

                                if (metric.Rating > 0)
                                {
                                    metric.Period = new Period() { Id = periodId };
                                    metrices.CustomerActualScoreTotal += Math.Round((Convert.ToDecimal(metric.Rating) / Convert.ToDecimal(metricRatingDbFacade.GetBy(metric))) * metric.Target, 2);
                                }

                                metrices.CustomerTargetValue += metric.Target;
                                metrices.CustomerTarget = "CUSTOMER (" + metrices.CustomerTargetValue + "%)";
                                metrices.CustomerSumTotal += metric.Score;
                                metrices.Customers.Add(metric);
                            }
                        }
                    }
                }
                else
                {
                    metrices = GetStaffInps(metrices, periodId, staffId);
                }

                return metrices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Metrices LoadFinancial(Metrices metrices, int metricPerspectiveId, int companyDepartmentJobRoleId, long appraisalHeaderId, int periodId)
        {
            try
            {
                List<Financial> metrics = new List<Financial>();
                metrices.FinancialTarget = "FINANCIALS (" + 0 + "%)";
                DataSet ds = metricDb.SelectMetricByMetricPerspectiveIDAndCompanyDepartmentJobRoleIDAndAppraisalHeaderId(metricPerspectiveId, companyDepartmentJobRoleId, appraisalHeaderId);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        metrices.FinancialTargetValue = 0;
                        metricRatingDbFacade = new MetricRatingDbFacade();
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            //decimal target = 0;
                            Financial metric = new Financial();
                            metric.StaffMetricId = ds.Tables[0].Rows[i][metricDb.FIELD_STAFF_METRIC_ID] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i][metricDb.FIELD_STAFF_METRIC_ID]);
                            metric.Id = ds.Tables[0].Rows[i][metricDb.FIELD_METRIC_ID] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i][metricDb.FIELD_METRIC_ID]);
                            metric.Kpi = ds.Tables[0].Rows[i][metricDb.FIELD_KPI] == DBNull.Value ? null : Convert.ToString(ds.Tables[0].Rows[i][metricDb.FIELD_KPI]);
                            metric.Measure = ds.Tables[0].Rows[i][metricDb.FIELD_MEASURE] == DBNull.Value ? null : Convert.ToString(ds.Tables[0].Rows[i][metricDb.FIELD_MEASURE]);
                            metric.Target = ds.Tables[0].Rows[i][metricDb.FIELD_TARGET] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i][metricDb.FIELD_TARGET]);
                            metric.Score = ds.Tables[0].Rows[i][metricDb.FIELD_SCORE] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i][metricDb.FIELD_SCORE]);
                            metric.Rating = ds.Tables[0].Rows[i][metricDb.FIELD_RATING] == DBNull.Value ? 0 : Convert.ToByte(ds.Tables[0].Rows[i][metricDb.FIELD_RATING]);

                            if (metric.Rating > 0)
                            {
                                //metrices.FinancialActualScoreTotal += Math.Round((Convert.ToDecimal(metric.Rating) / Convert.ToDecimal(5)) * metric.Target, 2);

                                metric.Period = new Period() { Id = periodId };
                                metrices.FinancialActualScoreTotal += Math.Round((Convert.ToDecimal(metric.Rating) / Convert.ToDecimal(metricRatingDbFacade.GetBy(metric))) * metric.Target, 2);

                            }

                            metrices.FinancialTargetValue += metric.Target;
                            metrices.FinancialTarget = "FINANCIALS (" + metrices.FinancialTargetValue + "%)";
                            metrices.FinancialSumTotal += metric.Score;
                            metrices.Financials.Add(metric);
                        }
                    }
                }

                return metrices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Metrices LoadPeople(Metrices metrices, int metricPerspectiveId, int companyDepartmentJobRoleId, bool isSupervisor, long appraisalHeaderId, int periodId, string staffId)
        {
            try
            {
                List<People> metrics = new List<People>();
                metrices.PeopleTarget = "PEOPLE (" + 0 + "%)";
                DataSet ds = metricDb.SelectMetricByMetricPerspectiveIDAndCompanyDepartmentJobRoleIDAndAppraisalHeaderId(metricPerspectiveId, companyDepartmentJobRoleId, appraisalHeaderId);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //get learning and growth for this staff
                        decimal learningScore = 0;
                        Learning learning = learningDbFacade.GetByStaffAndPeriod(staffId, periodId);
                        if (learning != null)
                        {
                            learningScore = learning.PercentageScore;
                        }

                        metrices.PeopleTargetValue = 0;
                        metricRatingDbFacade = new MetricRatingDbFacade();
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            //decimal target = 0;
                            People metric = new People();
                            metric.StaffMetricId = ds.Tables[0].Rows[i][metricDb.FIELD_STAFF_METRIC_ID] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i][metricDb.FIELD_STAFF_METRIC_ID]);
                            metric.Id = ds.Tables[0].Rows[i][metricDb.FIELD_METRIC_ID] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i][metricDb.FIELD_METRIC_ID]);
                            metric.Kpi = ds.Tables[0].Rows[i][metricDb.FIELD_KPI] == DBNull.Value ? null : Convert.ToString(ds.Tables[0].Rows[i][metricDb.FIELD_KPI]);
                            metric.Measure = ds.Tables[0].Rows[i][metricDb.FIELD_MEASURE] == DBNull.Value ? null : Convert.ToString(ds.Tables[0].Rows[i][metricDb.FIELD_MEASURE]);
                            metric.Target = ds.Tables[0].Rows[i][metricDb.FIELD_TARGET] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i][metricDb.FIELD_TARGET]);
                            metric.Score = learningScore;

                            //metric.Score = ds.Tables[0].Rows[i]["Metric_Score"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["Metric_Score"]);
                            metric.Rating = ds.Tables[0].Rows[i][metricDb.FIELD_RATING] == DBNull.Value ? 0 : Convert.ToByte(ds.Tables[0].Rows[i][metricDb.FIELD_RATING]);
                            metric.IsSupervisor = isSupervisor;

                            metric.MetricRatings = GetMetricRating(metric.Id, metricPerspectiveId, companyDepartmentJobRoleId, periodId);

                            if (metric.Rating > 0)
                            {
                                metric.Period = new Period() { Id = periodId };

                                //metrices.PeopleActualScoreTotal += Math.Round((Convert.ToDecimal(metric.Rating) / Convert.ToDecimal(5)) * metric.Target, 2);
                                metrices.PeopleActualScoreTotal += Math.Round((Convert.ToDecimal(metric.Rating) / Convert.ToDecimal(metricRatingDbFacade.GetBy(metric))) * metric.Target, 2);
                            }

                            metrices.PeopleTargetValue += metric.Target;
                            metrices.PeopleTarget = "PEOPLE (" + metrices.PeopleTargetValue + "%)";
                            metrices.PeopleSumTotal += metric.Score;
                            metrices.Peoples.Add(metric);
                        }
                    }
                }

                return metrices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Metrices LoadProcess(Metrices metrices, int metricPerspectiveId, int companyDepartmentJobRoleId, bool isSupervisor, long appraisalHeaderId, int periodId)
        {
            try
            {
                List<Process> metrics = new List<Process>();
                metrices.ProcessTarget = "PROCESS (" + 0 + "%)";
                DataSet ds = metricDb.SelectMetricByMetricPerspectiveIDAndCompanyDepartmentJobRoleIDAndAppraisalHeaderId(metricPerspectiveId, companyDepartmentJobRoleId, appraisalHeaderId);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        metrices.ProcessTargetValue = 0;
                        metricRatingDbFacade = new MetricRatingDbFacade();
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Process metric = new Process();
                            metric.StaffMetricId = ds.Tables[0].Rows[i][metricDb.FIELD_STAFF_METRIC_ID] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i][metricDb.FIELD_STAFF_METRIC_ID]);
                            //metric.StaffMetricId = ds.Tables[0].Rows[i]["Staff_Metric_ID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["Staff_Metric_ID"]);
                            metric.Id = ds.Tables[0].Rows[i][metricDb.FIELD_METRIC_ID] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i][metricDb.FIELD_METRIC_ID]);
                            metric.Kpi = ds.Tables[0].Rows[i][metricDb.FIELD_KPI] == DBNull.Value ? null : Convert.ToString(ds.Tables[0].Rows[i][metricDb.FIELD_KPI]);
                            metric.Measure = ds.Tables[0].Rows[i][metricDb.FIELD_MEASURE] == DBNull.Value ? null : Convert.ToString(ds.Tables[0].Rows[i][metricDb.FIELD_MEASURE]);
                            metric.Target = ds.Tables[0].Rows[i][metricDb.FIELD_TARGET] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i][metricDb.FIELD_TARGET]);
                            metric.Score = ds.Tables[0].Rows[i]["Metric_Score"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["Metric_Score"]);
                            metric.Rating = ds.Tables[0].Rows[i][metricDb.FIELD_RATING] == DBNull.Value ? 0 : Convert.ToByte(ds.Tables[0].Rows[i][metricDb.FIELD_RATING]);
                            metric.IsSupervisor = isSupervisor;

                            metric.MetricRatings = GetMetricRating(metric.Id, metricPerspectiveId, companyDepartmentJobRoleId, periodId);

                            if (metric.Rating > 0)
                            {
                                //metrices.ProcessActualScoreTotal += Math.Round((Convert.ToDecimal(metric.Rating) / Convert.ToDecimal(5)) * metric.Target, 2);

                                metric.Period = new Period() { Id = periodId };
                                metrices.ProcessActualScoreTotal += Math.Round((Convert.ToDecimal(metric.Rating) / Convert.ToDecimal(metricRatingDbFacade.GetBy(metric))) * metric.Target, 2);

                            }

                            metrices.ProcessTargetValue += metric.Target;
                            metrices.ProcessTarget = "PROCESS (" + metrices.ProcessTargetValue + "%)";
                            metrices.ProcessSumTotal += metric.Score;
                            metrices.Processes.Add(metric);
                        }
                    }
                }

                return metrices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<MetricRating> GetMetricRating(long metricId, int metricPerspectiveId, int companyDepartmentJobRoleId, int periodId)
        {
            try
            {
                //get metric ratings
                List<MetricRating> metricRatings = new List<MetricRating>();

                //DataSet dsRating = metricDb.SelectMetricRatingByMetricPerspectiveIDAndCompanyDepartmentJobRoleID(metricId, metricPerspectiveId, companyDepartmentJobRoleId);
                DataSet dsRating = metricDb.SelectMetricRatingByMetricPerspectiveIDAndCompanyDepartmentJobRoleIDAndPeriodID(metricId, metricPerspectiveId, companyDepartmentJobRoleId, periodId);
                if (dsRating != null)
                {
                    if (dsRating.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < dsRating.Tables[0].Rows.Count; j++)
                        {
                            MetricRating metricRating = new MetricRating();
                            metricRating.Metrics = new Model.Model.Metrics();
                            metricRating.Rating = new Rating();

                            metricRating.Metrics.Id = Convert.ToInt32(dsRating.Tables[0].Rows[j][metricDb.FIELD_METRIC_ID]);
                            metricRating.Rating.Id = Convert.ToByte(dsRating.Tables[0].Rows[j]["Rating_ID"]);
                            metricRating.From = Convert.ToDecimal(dsRating.Tables[0].Rows[j]["From"]);
                            metricRating.To = Convert.ToDecimal(dsRating.Tables[0].Rows[j]["To"]);

                            metricRatings.Add(metricRating);
                        }
                    }
                }

                return metricRatings;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Metrices LoadRisk(Metrices metrices, int metricPerspectiveId, int companyDepartmentJobRoleId, long appraisalHeaderId, int periodId)
        {
            try
            {
                List<Risk> metrics = new List<Risk>();
                metrices.RiskTarget = "RISK (" + 0 + "%)";
                DataSet ds = metricDb.SelectMetricByMetricPerspectiveIDAndCompanyDepartmentJobRoleIDAndAppraisalHeaderId(metricPerspectiveId, companyDepartmentJobRoleId, appraisalHeaderId);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        metrices.RiskTargetValue = 0;
                        metricRatingDbFacade = new MetricRatingDbFacade();
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            //decimal target = 0;
                            Risk metric = new Risk();
                            metric.StaffMetricId = ds.Tables[0].Rows[i][metricDb.FIELD_STAFF_METRIC_ID] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i][metricDb.FIELD_STAFF_METRIC_ID]);
                            metric.Id = ds.Tables[0].Rows[i][metricDb.FIELD_METRIC_ID] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i][metricDb.FIELD_METRIC_ID]);
                            metric.Kpi = ds.Tables[0].Rows[i][metricDb.FIELD_KPI] == DBNull.Value ? null : Convert.ToString(ds.Tables[0].Rows[i][metricDb.FIELD_KPI]);
                            metric.Measure = ds.Tables[0].Rows[i][metricDb.FIELD_MEASURE] == DBNull.Value ? null : Convert.ToString(ds.Tables[0].Rows[i][metricDb.FIELD_MEASURE]);
                            metric.Target = ds.Tables[0].Rows[i][metricDb.FIELD_TARGET] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i][metricDb.FIELD_TARGET]);
                            metric.Score = ds.Tables[0].Rows[i][metricDb.FIELD_SCORE] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i][metricDb.FIELD_SCORE]);
                            metric.Rating = ds.Tables[0].Rows[i][metricDb.FIELD_RATING] == DBNull.Value ? 0 : Convert.ToByte(ds.Tables[0].Rows[i][metricDb.FIELD_RATING]);

                            if (metric.Rating > 0)
                            {
                                //metrices.RiskActualScoreTotal += Math.Round((Convert.ToDecimal(metric.Rating) / Convert.ToDecimal(5)) * metric.Target, 2);

                                metric.Period = new Period() { Id = periodId };
                                metrices.RiskActualScoreTotal += Math.Round((Convert.ToDecimal(metric.Rating) / Convert.ToDecimal(metricRatingDbFacade.GetBy(metric))) * metric.Target, 2);
                            }

                            metrices.RiskTargetValue += metric.Target;
                            metrices.RiskTarget = "RISK (" + metrices.RiskTargetValue + "%)";
                            metrices.RiskSumTotal += metric.Score;
                            metrices.Risks.Add(metric);
                        }


                    }
                }

                return metrices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private Metrices LoadDefaultCustomer(Metrices metrices, int metricPerspectiveId, int companyDepartmentJobRoleId, int periodId, string staffId)
        {
            try
            {
                List<Customer> metrics = new List<Customer>();
                metrices.CustomerTarget = "CUSTOMER (" + 0 + "%)";

                if (periodId < 6)
                {
                    DataSet ds = metricDb.SelectDefaultMetricByMetricPerspectiveIDAndCompanyDepartmentJobRoleIDAndPeriodID(metricPerspectiveId, companyDepartmentJobRoleId, periodId);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            metrices.CustomerTargetValue = 0;
                            metrices.CustomerActualScoreTotal = 0;

                            metricRatingDbFacade = new MetricRatingDbFacade();
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                Customer metric = new Customer();
                                metric.Id = ds.Tables[0].Rows[i][metricDb.FIELD_METRIC_ID] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i][metricDb.FIELD_METRIC_ID]);
                                metric.Kpi = ds.Tables[0].Rows[i][metricDb.FIELD_KPI] == DBNull.Value ? null : Convert.ToString(ds.Tables[0].Rows[i][metricDb.FIELD_KPI]);
                                metric.Measure = ds.Tables[0].Rows[i][metricDb.FIELD_MEASURE] == DBNull.Value ? null : Convert.ToString(ds.Tables[0].Rows[i][metricDb.FIELD_MEASURE]);
                                metric.Target = ds.Tables[0].Rows[i][metricDb.FIELD_TARGET] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i][metricDb.FIELD_TARGET]);
                                metric.Score = ds.Tables[0].Rows[i][metricDb.FIELD_SCORE] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i][metricDb.FIELD_SCORE]);
                                metric.Rating = ds.Tables[0].Rows[i][metricDb.FIELD_RATING] == DBNull.Value ? 0 : Convert.ToByte(ds.Tables[0].Rows[i][metricDb.FIELD_RATING]);

                                if (metric.Rating > 0)
                                {
                                    metric.Period = new Period() { Id = periodId };
                                    metrices.CustomerActualScoreTotal += Math.Round((Convert.ToDecimal(metric.Rating) / Convert.ToDecimal(metricRatingDbFacade.GetBy(metric))) * metric.Target, 2);
                                }

                                metrices.CustomerTargetValue += metric.Target;
                                metrices.CustomerTarget = "CUSTOMER (" + metrices.CustomerTargetValue + "%)";
                                metrices.CustomerSumTotal += metric.Score;
                                metrices.Customers.Add(metric);
                            }

                        }
                    }
                }
                else
                {
                    metrices = GetStaffInps(metrices, periodId, staffId);
                }

                return metrices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Metrices GetStaffInps(Metrices metrices, int periodId, string staffId)
        {
            try
            {
                DataSet npsDs = npsDb.SelectDefaultInpsByStaffIDAndPeriodID(staffId, periodId);
                if (npsDs != null)
                {
                    if (npsDs.Tables[0].Rows.Count > 0)
                    {
                        npsRatingLogic = new InpsRatingLogic();
                        for (int i = 0; i < npsDs.Tables[0].Rows.Count; i++)
                        {
                            Customer nps = new Customer();
                            nps.Id = npsDs.Tables[0].Rows[i][npsDb.FIELD_INPS_ID] == DBNull.Value ? 0 : Convert.ToInt32(npsDs.Tables[0].Rows[i][npsDb.FIELD_INPS_ID]);
                            nps.Kpi = npsDs.Tables[0].Rows[i][npsDb.FIELD_KPI] == DBNull.Value ? null : Convert.ToString(npsDs.Tables[0].Rows[i][npsDb.FIELD_KPI]);
                            nps.Measure = npsDs.Tables[0].Rows[i][npsDb.FIELD_MEASURE] == DBNull.Value ? null : Convert.ToString(npsDs.Tables[0].Rows[i][npsDb.FIELD_MEASURE]);
                            nps.Target = npsDs.Tables[0].Rows[i][npsDb.FIELD_TARGET] == DBNull.Value ? 0 : Convert.ToDecimal(npsDs.Tables[0].Rows[i][npsDb.FIELD_TARGET]);
                            nps.Score = npsDs.Tables[0].Rows[i][npsDb.FIELD_SCORE] == DBNull.Value ? 0 : Convert.ToDecimal(npsDs.Tables[0].Rows[i][npsDb.FIELD_SCORE]);
                            nps.Rating = npsDs.Tables[0].Rows[i][npsDb.FIELD_RATING] == DBNull.Value ? 0 : Convert.ToByte(npsDs.Tables[0].Rows[i][npsDb.FIELD_RATING]);

                            //nps.Kpi = UtilityLogic.JumbbleText(nps.Kpi);
                            //nps.Measure = UtilityLogic.JumbbleText(nps.Kpi);
                           

                            if (nps.Rating > 0)
                            {
                                nps.Period = new Period() { Id = periodId };
                                metrices.CustomerActualScoreTotal += Math.Round((Convert.ToDecimal(nps.Rating) / Convert.ToDecimal(npsRatingLogic.GetBy(nps.Period))) * nps.Target, 2);
                            }

                            metrices.CustomerTargetValue += nps.Target;
                            metrices.CustomerTarget = "CUSTOMER (" + metrices.CustomerTargetValue + "%)";
                            metrices.CustomerSumTotal += nps.Score;
                            metrices.Customers.Add(nps);
                        }
                    }
                }

                return metrices;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Metrices LoadDefaultFinancial(Metrices metrices, int metricPerspectiveId, int companyDepartmentJobRoleId, int periodId)
        {
            try
            {
                List<Financial> metrics = new List<Financial>();
                metrices.FinancialTarget = "FINANCIAL (" + 0 + "%)";
                //DataSet ds = metricDb.SelectDefaultMetricByMetricPerspectiveIDAndCompanyDepartmentJobRoleID(metricPerspectiveId, companyDepartmentJobRoleId);

                DataSet ds = metricDb.SelectDefaultMetricByMetricPerspectiveIDAndCompanyDepartmentJobRoleIDAndPeriodID(metricPerspectiveId, companyDepartmentJobRoleId, periodId);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        metrices.FinancialTargetValue = 0;
                        metricRatingDbFacade = new MetricRatingDbFacade();
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            //decimal target = 0;
                            Financial metric = new Financial();
                            metric.Id = ds.Tables[0].Rows[i][metricDb.FIELD_METRIC_ID] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i][metricDb.FIELD_METRIC_ID]);
                            metric.Kpi = ds.Tables[0].Rows[i][metricDb.FIELD_KPI] == DBNull.Value ? null : Convert.ToString(ds.Tables[0].Rows[i][metricDb.FIELD_KPI]);
                            metric.Measure = ds.Tables[0].Rows[i][metricDb.FIELD_MEASURE] == DBNull.Value ? null : Convert.ToString(ds.Tables[0].Rows[i][metricDb.FIELD_MEASURE]);
                            metric.Target = ds.Tables[0].Rows[i][metricDb.FIELD_TARGET] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i][metricDb.FIELD_TARGET]);
                            metric.Score = ds.Tables[0].Rows[i][metricDb.FIELD_SCORE] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i][metricDb.FIELD_SCORE]);
                            metric.Rating = ds.Tables[0].Rows[i][metricDb.FIELD_RATING] == DBNull.Value ? 0 : Convert.ToByte(ds.Tables[0].Rows[i][metricDb.FIELD_RATING]);

                            //metric.Kpi = UtilityLogic.JumbbleText(metric.Kpi);
                            //metric.Measure = UtilityLogic.JumbbleText(metric.Kpi);

                            if (metric.Rating > 0)
                            {
                                //metrices.FinancialActualScoreTotal += Math.Round((Convert.ToDecimal(metric.Rating) / Convert.ToDecimal(5)) * metric.Target, 2);

                                metric.Period = new Period() { Id = periodId };
                                metrices.FinancialActualScoreTotal += Math.Round((Convert.ToDecimal(metric.Rating) / Convert.ToDecimal(metricRatingDbFacade.GetBy(metric))) * metric.Target, 2);

                            }

                            metrices.FinancialTargetValue += metric.Target;
                            metrices.FinancialTarget = "FINANCIAL (" + metrices.FinancialTargetValue + "%)";
                            metrices.FinancialSumTotal += metric.Score;
                            metrices.Financials.Add(metric);
                        }
                    }
                }

                return metrices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Metrices LoadDefaultPeople(Metrices metrices, int metricPerspectiveId, int companyDepartmentJobRoleId, bool isSupervisor, int periodId, string staffId)
        {
            try
            {
                List<People> metrics = new List<People>();
                metrices.PeopleTarget = "PEOPLE (" + 0 + "%)";
                //DataSet ds = metricDb.SelectDefaultMetricByMetricPerspectiveIDAndCompanyDepartmentJobRoleID(metricPerspectiveId, companyDepartmentJobRoleId);

                DataSet ds = metricDb.SelectDefaultMetricByMetricPerspectiveIDAndCompanyDepartmentJobRoleIDAndPeriodID(metricPerspectiveId, companyDepartmentJobRoleId, periodId);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //get learning and growth for this staff
                        decimal learningScore = 0;
                        Learning learning = learningDbFacade.GetByStaffAndPeriod(staffId, periodId);
                        if (learning != null)
                        {
                            learningScore = learning.PercentageScore;
                        }

                        metrices.PeopleTargetValue = 0;
                        metricRatingDbFacade = new MetricRatingDbFacade();
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            //decimal target = 0;
                            People metric = new People();
                            metric.Period = new Period();

                            metric.Period.Id = periodId;
                            metric.Id = ds.Tables[0].Rows[i][metricDb.FIELD_METRIC_ID] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i][metricDb.FIELD_METRIC_ID]);
                            metric.Kpi = ds.Tables[0].Rows[i][metricDb.FIELD_KPI] == DBNull.Value ? null : Convert.ToString(ds.Tables[0].Rows[i][metricDb.FIELD_KPI]);
                            metric.Measure = ds.Tables[0].Rows[i][metricDb.FIELD_MEASURE] == DBNull.Value ? null : Convert.ToString(ds.Tables[0].Rows[i][metricDb.FIELD_MEASURE]);
                            metric.Target = ds.Tables[0].Rows[i][metricDb.FIELD_TARGET] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i][metricDb.FIELD_TARGET]);
                            metric.Score = learningScore;
                            //metric.Score = ds.Tables[0].Rows[i][metricDb.FIELD_SCORE] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i][metricDb.FIELD_SCORE]);
                            metric.Rating = ds.Tables[0].Rows[i][metricDb.FIELD_RATING] == DBNull.Value ? 0 : Convert.ToByte(ds.Tables[0].Rows[i][metricDb.FIELD_RATING]);
                            metric.IsSupervisor = isSupervisor;

                            //metric.Kpi = UtilityLogic.JumbbleText(metric.Kpi);
                            //metric.Measure = UtilityLogic.JumbbleText(metric.Kpi);

                            metric.MetricRatings = GetMetricRating(metric.Id, metricPerspectiveId, companyDepartmentJobRoleId, periodId);

                            if (metric.Rating > 0)
                            {


                                //metrices.PeopleActualScoreTotal += Math.Round((Convert.ToDecimal(metric.Rating) / Convert.ToDecimal(5)) * metric.Target, 2);
                                metrices.PeopleActualScoreTotal += Math.Round((Convert.ToDecimal(metric.Rating) / Convert.ToDecimal(metricRatingDbFacade.GetBy(metric))) * metric.Target, 2);

                            }

                            metrices.PeopleTargetValue += metric.Target;
                            metrices.PeopleTarget = "PEOPLE (" + metrices.PeopleTargetValue + "%)";
                            metrices.PeopleSumTotal += metric.Score;
                            metrices.Peoples.Add(metric);
                        }


                    }
                }

                return metrices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Metrices LoadDefaultProcess(Metrices metrices, int metricPerspectiveId, int companyDepartmentJobRoleId, bool isSupervisor, int periodId)
        {
            try
            {
                List<Process> metrics = new List<Process>();
                metrices.ProcessTarget = "PROCESS (" + 0 + "%)";
                //DataSet ds = metricDb.SelectDefaultMetricByMetricPerspectiveIDAndCompanyDepartmentJobRoleID(metricPerspectiveId, companyDepartmentJobRoleId);

                DataSet ds = metricDb.SelectDefaultMetricByMetricPerspectiveIDAndCompanyDepartmentJobRoleIDAndPeriodID(metricPerspectiveId, companyDepartmentJobRoleId, periodId);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        metrices.ProcessTargetValue = 0;
                        metricRatingDbFacade = new MetricRatingDbFacade();
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            //decimal target = 0;
                            Process metric = new Process();
                            metric.Id = ds.Tables[0].Rows[i][metricDb.FIELD_METRIC_ID] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i][metricDb.FIELD_METRIC_ID]);
                            metric.Kpi = ds.Tables[0].Rows[i][metricDb.FIELD_KPI] == DBNull.Value ? null : Convert.ToString(ds.Tables[0].Rows[i][metricDb.FIELD_KPI]);
                            metric.Measure = ds.Tables[0].Rows[i][metricDb.FIELD_MEASURE] == DBNull.Value ? null : Convert.ToString(ds.Tables[0].Rows[i][metricDb.FIELD_MEASURE]);
                            metric.Target = ds.Tables[0].Rows[i][metricDb.FIELD_TARGET] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i][metricDb.FIELD_TARGET]);
                            metric.Score = ds.Tables[0].Rows[i][metricDb.FIELD_SCORE] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i][metricDb.FIELD_SCORE]);
                            metric.Rating = ds.Tables[0].Rows[i][metricDb.FIELD_RATING] == DBNull.Value ? 0 : Convert.ToByte(ds.Tables[0].Rows[i][metricDb.FIELD_RATING]);
                            metric.IsSupervisor = isSupervisor;

                            if (metric.Rating > 0)
                            {
                                //metrices.ProcessActualScoreTotal += Math.Round((Convert.ToDecimal(metric.Rating) / Convert.ToDecimal(5)) * metric.Target, 2);

                                metric.Period = new Period() { Id = periodId };
                                metrices.ProcessActualScoreTotal += Math.Round((Convert.ToDecimal(metric.Rating) / Convert.ToDecimal(metricRatingDbFacade.GetBy(metric))) * metric.Target, 2);

                            }

                            //metric.Kpi = UtilityLogic.JumbbleText(metric.Kpi);
                            //metric.Measure = UtilityLogic.JumbbleText(metric.Kpi);

                            metrices.ProcessTargetValue += metric.Target;
                            metrices.ProcessTarget = "PROCESS (" + metrices.ProcessTargetValue + "%)";
                            metrices.ProcessSumTotal += metric.Score;
                            metric.MetricRatings = GetMetricRating(metric.Id, metricPerspectiveId, companyDepartmentJobRoleId, periodId);
                            metrices.Processes.Add(metric);
                        }
                    }
                }

                return metrices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Metrices LoadDefaultRisk(Metrices metrices, int metricPerspectiveId, int companyDepartmentJobRoleId, int periodId)
        {
            try
            {
                List<Risk> metrics = new List<Risk>();
                metrices.RiskTarget = "RISK (" + 0 + "%)";
                //DataSet ds = metricDb.SelectDefaultMetricByMetricPerspectiveIDAndCompanyDepartmentJobRoleID(metricPerspectiveId, companyDepartmentJobRoleId);

                DataSet ds = metricDb.SelectDefaultMetricByMetricPerspectiveIDAndCompanyDepartmentJobRoleIDAndPeriodID(metricPerspectiveId, companyDepartmentJobRoleId, periodId);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        metrices.RiskTargetValue = 0;
                        metricRatingDbFacade = new MetricRatingDbFacade();
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            //decimal target = 0;
                            Risk metric = new Risk();
                            metric.Id = ds.Tables[0].Rows[i][metricDb.FIELD_METRIC_ID] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i][metricDb.FIELD_METRIC_ID]);
                            metric.Kpi = ds.Tables[0].Rows[i][metricDb.FIELD_KPI] == DBNull.Value ? null : Convert.ToString(ds.Tables[0].Rows[i][metricDb.FIELD_KPI]);
                            metric.Measure = ds.Tables[0].Rows[i][metricDb.FIELD_MEASURE] == DBNull.Value ? null : Convert.ToString(ds.Tables[0].Rows[i][metricDb.FIELD_MEASURE]);
                            metric.Target = ds.Tables[0].Rows[i][metricDb.FIELD_TARGET] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i][metricDb.FIELD_TARGET]);
                            metric.Score = ds.Tables[0].Rows[i][metricDb.FIELD_SCORE] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i][metricDb.FIELD_SCORE]);
                            metric.Rating = ds.Tables[0].Rows[i][metricDb.FIELD_RATING] == DBNull.Value ? 0 : Convert.ToByte(ds.Tables[0].Rows[i][metricDb.FIELD_RATING]);

                            if (metric.Rating > 0)
                            {
                                //metrices.RiskActualScoreTotal += Math.Round((Convert.ToDecimal(metric.Rating) / Convert.ToDecimal(5)) * metric.Target,2);

                                metric.Period = new Period() { Id = periodId };
                                metrices.RiskActualScoreTotal += Math.Round((Convert.ToDecimal(metric.Rating) / Convert.ToDecimal(metricRatingDbFacade.GetBy(metric))) * metric.Target, 2);
                            }

                            //metric.Kpi = UtilityLogic.JumbbleText(metric.Kpi);
                            //metric.Measure = UtilityLogic.JumbbleText(metric.Kpi);

                            metrices.RiskTargetValue += metric.Target;
                            metrices.RiskTarget = "RISK (" + metrices.RiskTargetValue + "%)";
                            metrices.RiskSumTotal += metric.Score;
                            metrices.Risks.Add(metric);
                        }


                    }
                }

                return metrices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Risk> LoadMetricByPeriodId(int periodId)
        {
            try
            {
                List<Risk> metrices = new List<Risk>();
                DataSet ds = metricDb.SelectMetricByPeriodID(periodId);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Risk metric = new Risk();
                            metric.Period = new Period();

                            metric.Id = ds.Tables[0].Rows[i][metricDb.FIELD_METRIC_ID] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i][metricDb.FIELD_METRIC_ID]);
                            metric.PerspectiveId = ds.Tables[0].Rows[i][metricDb.FIELD_METRIC_PERSPECTIVE_ID] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i][metricDb.FIELD_METRIC_PERSPECTIVE_ID]);
                            metric.CompanyDepartmentJobRoleId = ds.Tables[0].Rows[i][metricDb.FIELD_COMPANY_DEPARTMENT_JOB_ROLE_ID] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i][metricDb.FIELD_COMPANY_DEPARTMENT_JOB_ROLE_ID]);
                            metric.Kpi = ds.Tables[0].Rows[i][metricDb.FIELD_KPI] == DBNull.Value ? null : Convert.ToString(ds.Tables[0].Rows[i][metricDb.FIELD_KPI]);
                            metric.Measure = ds.Tables[0].Rows[i][metricDb.FIELD_MEASURE] == DBNull.Value ? null : Convert.ToString(ds.Tables[0].Rows[i][metricDb.FIELD_MEASURE]);
                            metric.DataSource = ds.Tables[0].Rows[i][metricDb.FIELD_DATA_SOURCE] == DBNull.Value ? null : Convert.ToString(ds.Tables[0].Rows[i][metricDb.FIELD_DATA_SOURCE]);
                            metric.ResponsibilityDepartmentId = ds.Tables[0].Rows[i][metricDb.FIELD_RESPONSIBLE_DEPARTMENT_ID] == DBNull.Value ? null : Convert.ToString(ds.Tables[0].Rows[i][metricDb.FIELD_RESPONSIBLE_DEPARTMENT_ID]);
                            metric.Target = ds.Tables[0].Rows[i][metricDb.FIELD_TARGET] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i][metricDb.FIELD_TARGET]);
                            metric.Score = ds.Tables[0].Rows[i][metricDb.FIELD_SCORE] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i][metricDb.FIELD_SCORE]);
                            metric.Period.Id = ds.Tables[0].Rows[i][metricDb.FIELD_PERIOD_ID] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i][metricDb.FIELD_PERIOD_ID]);
                            //metric.Rating = ds.Tables[0].Rows[i][metricDb.FIELD_RATING] == DBNull.Value ? 0 : Convert.ToByte(ds.Tables[0].Rows[i][metricDb.FIELD_RATING]);

                            metrices.Add(metric);
                        }
                    }
                }

                return metrices;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Add(List<Risk> metrices, int newPeriodId, int oldPeriodId, Transaction transaction)
        {
            metricRatingDbFacade = new MetricRatingDbFacade();

            Risk risk = metrices[0];
            List<MetricRating> metricRatings = metricRatingDbFacade.GetMetricRatingByPeriodId(risk.Period.Id);

            //transaction = new Transaction(DataAccess.DataAccess.ConnString);

            try
            {
                if (metrices != null)
                {
                    foreach (Metric metric in metrices)
                    {
                        long metricId = metricDb.InsertMetric(metric.PerspectiveId, metric.CompanyDepartmentJobRoleId, metric.Kpi, metric.Measure, metric.DataSource, metric.ResponsibilityDepartmentId, metric.Target, metric.Score, newPeriodId, transaction);

                        //List<MetricRating> selectedMetricRatings = metricRatings.Where(m => m.Rating.Id == metric.Id).ToList();
                        List<MetricRating> selectedMetricRatings = metricRatings.Where(m => m.Metrics.Id == metric.Id).ToList();
                        List<MetricRating> newMetricRatings = UpdatMetricRatings(selectedMetricRatings, metricId, newPeriodId);

                        if (!metricRatingDbFacade.Add(newMetricRatings, transaction))
                        {
                            //transaction.Abort();
                            return false;
                        }

                        newMetricRatings = null;
                    }
                }

                if (!metricDb.InsertOtherEntitiesForNewAppraisal(oldPeriodId, newPeriodId, transaction))
                {
                    //transaction.Abort();
                    return false;
                }

                //transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<MetricRating> UpdatMetricRatings(List<MetricRating> ratings, long metricId, int periodId)
        {
            try
            {
                List<MetricRating> metricRatings = new List<MetricRating>();

                foreach (MetricRating metricRating in ratings)
                {
                    metricRating.Metrics = new Metrics();

                    metricRating.Metrics.Id = metricId;
                    metricRating.Period.Id = periodId;

                    metricRatings.Add(metricRating);
                }

                return metricRatings;
            }
            catch (Exception)
            {
                throw;
            }
        }






    }
}