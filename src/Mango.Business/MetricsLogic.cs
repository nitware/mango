using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;
using Mango.Model.Translator;
using System.Data;
using Mango.Model;
using System.Transactions;

namespace Mango.Business
{
    public class MetricsLogic : BusinessLogicBase<Metrics, METRIC>
    {
        private MetricRatingLogic metricRatingLogic;
        private StaffMetricLogic staffMetricLogic;

        public MetricsLogic()
        {
            base.translator = new MetricsTranslator();
            metricRatingLogic = new MetricRatingLogic();
        }

        public bool Modify(List<Metrics> newMetrices, bool removeRatings)
        {
            try
            {
                int rowsAdded = -1;
                bool modified = false;

                if (newMetrices != null && newMetrices.Count > 0)
                {
                    int difference = 0;
                    List<Metrics> oldMetrices = GetBy(newMetrices[0].CompanyDepartmentJobRole, newMetrices[0].Period);
                    oldMetrices = oldMetrices.OrderByDescending(m => m.Id).ToList();
                    newMetrices = newMetrices.OrderByDescending(m => m.Id).ToList();

                    int oldRecordCount = oldMetrices.Count;
                    int newRecordCount = newMetrices.Count;
                    if (oldRecordCount == newRecordCount)
                    {
                        modified = Modify(oldMetrices, newMetrices);
                        repository.SaveChanges();
                    }
                    else if (oldRecordCount > newRecordCount)
                    {
                        difference = oldRecordCount - newRecordCount;
                        List<Metrics> metricesThatCanBeDiscarded = ToBeDeleted(oldMetrices, newMetrices);
                        List<Metrics> metricesToBeDeleted = metricesThatCanBeDiscarded.Take(difference).ToList();
                        List<Metrics> oldMetricesToModify = ToBeModified(oldMetrices, metricesToBeDeleted);

                        if (Modify(oldMetricesToModify, newMetrices))
                        {
                            if (removeRatings)
                            {
                                foreach (Metrics metrics in metricesToBeDeleted)
                                {
                                    modified = RemoveBy(metrics);
                                }
                            }
                            else
                            {
                                modified = Remove(metricesToBeDeleted);
                            }

                            if (modified)
                            {
                                repository.SaveChanges();
                            }
                        }
                    }
                    else if (oldRecordCount < newRecordCount)
                    {
                        difference = newRecordCount - oldRecordCount;
                        List<Metrics> metricesToAdd = newMetrices.Skip(oldRecordCount).Take(difference).ToList();

                        if (Modify(oldMetrices, newMetrices))
                        {
                            repository.SaveChanges();

                            rowsAdded = Add(metricesToAdd);
                            modified = rowsAdded > -1 ? true : false;
                        }
                    }
                }

                return modified;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<Metrics> ToBeDeleted(List<Metrics> oldMetrices, List<Metrics> newMetrices)
        {
            try
            {
                if (oldMetrices == null && newMetrices == null)
                {
                    throw new Exception("Old and new mterices cannot be null! Please contact your system administrator.");
                }

                List<Metrics> metrices = new List<Metrics>();
                foreach (Metrics metrics in oldMetrices)
                {
                    Metrics old = newMetrices.Where(m => m.Id == metrics.Id).SingleOrDefault();
                    if (old == null)
                    {
                        metrices.Add(metrics);
                    }
                }

                return metrices;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private List<Metrics> ToBeModified(List<Metrics> oldMetrices, List<Metrics> metricesToBeDeleted)
        {
            try
            {
                if (metricesToBeDeleted == null || metricesToBeDeleted.Count == 0)
                {
                    return oldMetrices;
                }

                if (oldMetrices == null && metricesToBeDeleted == null)
                {
                    throw new Exception("Old and Metrices to be deleted cannot be null! Please contact your system administrator.");
                }

                List<Metrics> metrices = new List<Metrics>();
                foreach (Metrics metrics in metricesToBeDeleted)
                {
                    oldMetrices.Remove(metrics);
                }

                return oldMetrices;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Metrics> GetBy(CompanyDepartmentJobRole companyDepartmentJobRole, Period period)
        {
            try
            {
                Func<METRIC, bool> predicate = m => m.Company_Department_Job_Role_ID == companyDepartmentJobRole.Id && m.Period_ID == period.Id && m.COMPANY_DEPARTMENT_JOB_ROLE.Company_ID == 2;
                return GetModelsBy(predicate).OrderBy(m => m.Perspective.Id).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Metrics> GetBy(Period period)
        {
            try
            {
                Func<METRIC, bool> predicate = m => m.Period_ID == period.Id && m.COMPANY_DEPARTMENT_JOB_ROLE.Company_ID == 2;
                return GetModelsBy(predicate).OrderBy(m => m.CompanyDepartmentJobRole.JobRole.Name).OrderBy(m => m.Perspective.Id).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Metrics> GetBy(Period period, MetricsPerspective perspective)
        {
            try
            {
                Func<METRIC, bool> predicate = m => m.Period_ID == period.Id && m.Metric_Perspective_ID == perspective.Id && m.COMPANY_DEPARTMENT_JOB_ROLE.Company_ID == 2;
                return GetModelsBy(predicate).OrderBy(m => m.Perspective.Id).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Metrics> GetNpsBy(Period period)
        {
            try
            {
                Func<METRIC, bool> predicate = m => m.Period_ID == period.Id && m.COMPANY_DEPARTMENT_JOB_ROLE.Company_ID == 2 && m.Metric_Perspective_ID == 1;
                return GetModelsBy(predicate).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool Modify(List<Metrics> oldMetrices, List<Metrics> newMetrices)
        {
            try
            {
                for (int i = 0; i < oldMetrices.Count; i++)
                {
                    oldMetrices[i].Perspective = newMetrices[i].Perspective;
                    oldMetrices[i].CompanyDepartmentJobRole = newMetrices[i].CompanyDepartmentJobRole;
                    oldMetrices[i].Kpi = newMetrices[i].Kpi;
                    oldMetrices[i].Measure = newMetrices[i].Measure;
                    oldMetrices[i].DataSource = newMetrices[i].DataSource;
                    oldMetrices[i].ResponsibleDepartment = newMetrices[i].ResponsibleDepartment;
                    oldMetrices[i].Target = newMetrices[i].Target;
                    oldMetrices[i].Score = newMetrices[i].Score;
                    oldMetrices[i].Period = newMetrices[i].Period;

                    if (!Modify(oldMetrices[i]))
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ModifyNps(List<Metrics> metrices)
        {
            try
            {
                if (metrices != null && metrices.Count > 0)
                {
                    foreach (Metrics metrics in metrices)
                    {
                        Func<METRIC, bool> predicate = m => m.Metric_ID == metrics.Id;
                        METRIC entity = GetEntityBy(predicate);

                        entity.Kpi = metrics.Kpi;
                        entity.Measure = metrics.Measure;
                        entity.Data_Source = metrics.DataSource;
                        entity.Rsponsible_Department_ID = metrics.ResponsibleDepartment.Id;
                        entity.Target = metrics.Target;
                        entity.Score = metrics.Score;
                    }

                    int rowsAffected = repository.SaveChanges();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception(NoItemModified);
                    }
                }
                else
                {
                    throw new Exception("Required object cannot be empty! Please contact your system administrator.");
                }
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException(ArgumentNullException);
            }
            catch (UpdateException)
            {
                throw new UpdateException(UpdateException);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Modify(Metrics metrics)
        {
            try
            {
                Func<METRIC, bool> predicate = m => m.Metric_ID == metrics.Id;
                METRIC entity = GetEntityBy(predicate);

                entity.Metric_Perspective_ID = metrics.Perspective.Id;
                entity.Company_Department_Job_Role_ID = metrics.CompanyDepartmentJobRole.Id;
                entity.Kpi = metrics.Kpi;
                entity.Measure = metrics.Measure;
                entity.Data_Source = metrics.DataSource;
                entity.Rsponsible_Department_ID = metrics.ResponsibleDepartment.Id;
                entity.Target = metrics.Target;
                entity.Score = metrics.Score;
                entity.Period_ID = metrics.Period.Id;

                int rowsAffected = repository.SaveChanges();
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    throw new Exception(NoItemModified);
                }
                
                //using (TransactionScope transaction = new TransactionScope())
                //{
                //    int rowsAffected = repository.SaveChanges();
                //    if (rowsAffected > 0)
                //    {
                //        if (staffMetricLogic.Modify(metrics))
                //        {
                //            transaction.Complete();
                //            return true;
                //        }
                //        else
                //        {
                //            return false;
                //        }
                //    }
                //    else
                //    {
                //        throw new Exception(NoItemModified);
                //    }

                //}

            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException(ArgumentNullException);
            }
            catch (UpdateException)
            {
                throw new UpdateException(UpdateException);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Remove(List<Metrics> metrices)
        {
            try
            {
                bool suceeded = false;
                if (metrices != null && metrices.Count > 0)
                {
                    foreach (Metrics metrics in metrices)
                    {
                        if (IsDependentOnMetricRating(metrics))
                        {
                            return suceeded;
                        }
                    }

                    foreach (Metrics metrics in metrices)
                    {
                        Func<METRIC, bool> selector = m => m.Metric_ID == metrics.Id;
                        suceeded = base.Remove(selector);
                    }

                    repository.SaveChanges();
                    suceeded = true;
                }

                return suceeded;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool MetricRatingDependencyExist(List<Metrics> metrices)
        {
            try
            {
                foreach (Metrics metrics in metrices)
                {
                    List<MetricRating> metricRatings = metricRatingLogic.GetBy(metrics);
                    if (metricRatings != null && metricRatings.Count > 0)
                    {
                        return true;
                        //throw new Exception("Metrics with KPI '" + metrics.Kpi + "' depends on Metric Rating(s) which cannot be removed at this time. Manually remove the Metric Rating associated with " + metrics.CompanyDepartmentJobRole.JobRole.Name + " by using the Metric Rating tab, or click the Remove Associated Metric Rating button on this page.");
                    }
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool IsDependentOnMetricRating(Metrics metrics)
        {
            try
            {
                List<MetricRating> metricRatings = metricRatingLogic.GetBy(metrics);
                if (metricRatings != null && metricRatings.Count > 0)
                {
                    throw new Exception("Metrics with KPI '" + metrics.Perspective.Name + " - " + metrics.Kpi + "' depends on Metric Rating(s) which cannot be removed at this time. Manually remove the Metric Rating associated with " + metrics.CompanyDepartmentJobRole.JobRole.Name + " by using the Metric Rating tab, or click the Remove Associated Metric Rating button on this page.");
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveBy(Metrics metrics)
        {
            try
            {
                List<MetricRating> metricRatings = metricRatingLogic.GetBy(metrics);
                if (metricRatings != null || metricRatings.Count > 0)
                {
                    metricRatingLogic.Remove(metrics);
                }

                return this.Remove(metrics);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Remove(Metrics metrics)
        {
            try
            {
                Func<METRIC, bool> selector = d => d.Metric_ID == metrics.Id;
                bool suceeded = base.Remove(selector);
                repository.SaveChanges();
                return suceeded;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveBy(CompanyDepartmentJobRole companyDepartmentJobRole, Period period, bool removeMetrics)
        {
            try
            {
                bool suceeded = false;
                if (removeMetrics)
                {
                    List<Metrics> metrices = GetBy(companyDepartmentJobRole, period);
                    foreach (Metrics metrics in metrices)
                    {
                        if (IsDependentOnMetricRating(metrics))
                        {
                            return false;
                        }
                    }

                    Func<METRIC, bool> selector = m => m.Company_Department_Job_Role_ID == companyDepartmentJobRole.Id && m.Period_ID == period.Id;
                    suceeded = base.Remove(selector);
                    repository.SaveChanges();
                }
                else
                {
                    List<Metrics> metrices = GetBy(companyDepartmentJobRole, period);

                    List<MetricRating> ratings = new List<MetricRating>();
                    foreach (Metrics metrics in metrices)
                    {
                        List<MetricRating> metricRatings = metricRatingLogic.GetBy(metrics);
                        if (metricRatings.Count > 0)
                        {
                            ratings.AddRange(metricRatings);
                        }
                    }

                    if (ratings.Count > 0)
                    {
                        suceeded = metricRatingLogic.RemoveBy(ratings);
                    }
                }

                return suceeded;
            }
            catch (Exception)
            {
                throw;
            }
        }



    }

}
