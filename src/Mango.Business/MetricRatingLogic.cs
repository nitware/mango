using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model;
using Mango.Model.Translator;
using Mango.Model.Model;
using System.Data;

namespace Mango.Business
{
    public class MetricRatingLogic : BusinessLogicBase<MetricRating, METRIC_RATING>
    {
        public MetricRatingLogic()
        {
            base.translator = new MetricRatingTranslator();
        }

        public List<MetricRating> GetBy(Metrics metrics)
        {
            try
            {
                //Func<METRIC_RATING, bool> selector = s => s.Metric_ID == metrics.Id;

                Func<METRIC_RATING, bool> selector = s => s.Metric_ID == metrics.Id && s.METRIC.COMPANY_DEPARTMENT_JOB_ROLE.Company_ID == 2;
                return base.GetModelsBy(selector);



                //Repository repository = new Repository();
                //List<AppraisalReport> ratings = (from mr in repository.Fetch<METRIC_RATING>()
                //                                          where mr.Metric_ID == metrics.Id
                //                                          select new MetricRating
                //                                          {
                //                                              Metrics = ar.Company_Name,
                //                                              DepartmentName = ar.Department_Name,
                //                                              StaffId = ar.Staff_ID,
                //                                              StaffName = ar.NAME,
                //                                              SupervisorId = ar.Supervisor_Staff_ID,
                //                                              SupervisorName = ar.Supervisor,
                //                                              JobRoleLevelName = ar.Job_Role_Level_Name,
                //                                              JobRoleName = ar.Job_Role_Name,
                //                                              PaceScore = ar.Pace_Score.HasValue ? ar.Pace_Score.Value : (decimal)0,
                //                                              PaceGrade = ar.Pace_Grade,
                //                                              MetricScore = ar.Metric_Score.HasValue ? ar.Metric_Score.Value : (double)0,
                //                                              MetricRating = ar.Metric_Rating.HasValue ? ar.Metric_Rating.Value : (byte)0,
                //                                              Recommendation = ar.Recommendation_Name,
                //                                              PeriodName = ar.Type
                //                                          }).ToList();

                //return appraisalReports; 
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<MetricRating> GetBy(Period period)
        {
            try
            {
                //Func<METRIC_RATING, bool> selector = s => s.Period_ID == period.Id;

                Func<METRIC_RATING, bool> selector = s => s.Period_ID == period.Id && s.METRIC.COMPANY_DEPARTMENT_JOB_ROLE.Company_ID == 2;
                return base.GetModelsBy(selector);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Modify(List<MetricRating> metricRatings)
        {
            try
            {
                int added = 0;
                if (metricRatings != null && metricRatings.Count > 0)
                {
                    bool removed = Remove(metricRatings);
                    if (removed)
                    {
                        added = base.Add(metricRatings);
                    }
                }
                //else
                //{
                //    added = Remove(metricRatings) ? 1 : 0;
                //}

                return added > 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Modify(MetricRating metricRating)
        {
            try
            {
                Func<METRIC_RATING, bool> predicate = m => m.Metric_ID == metricRating.Metrics.Id && m.Rating_ID == metricRating.Rating.Id;
                METRIC_RATING entity = GetEntityBy(predicate);

                entity.Metric_ID = metricRating.Metrics.Id;
                entity.Rating_ID = metricRating.Rating.Id;
                entity.From = metricRating.From;
                entity.To = metricRating.To;
                entity.Rating_Type_ID = metricRating.RatingType.Id;
                entity.Period_ID = metricRating.Period.Id;
             
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

        public bool Remove(List<MetricRating> metricRatings)
        {
            try
            {
                Func<METRIC_RATING, bool> selector = d => d.Metric_ID == metricRatings[0].Metrics.Id;
                bool suceeded = base.Remove(selector);
                repository.SaveChanges();
                return suceeded;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool RemoveBy(List<MetricRating> metricRatings)
        {
            try
            {
                if (metricRatings != null && metricRatings.Count > 0)
                {
                    foreach (MetricRating metricRating in metricRatings)
                    {
                    Func<METRIC_RATING, bool> selector = d => d.Metric_ID == metricRating.Metrics.Id;
                    base.Remove(selector);
                    }
                }

                return repository.SaveChanges() > 0 ? true : false;
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
                Func<METRIC_RATING, bool> selector = d => d.Metric_ID == metrics.Id;
                bool suceeded = base.Remove(selector);
                repository.SaveChanges();
                return suceeded;
            }
            catch (Exception)
            {
                throw;
            }
        }






    }



}
