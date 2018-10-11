using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;

namespace Mango.Model.Translator
{
    public class MetricsTranslator : TranslatorBase<Metrics, METRIC>
    {
        private MetricsPerspectiveTranslator metricsPerspectiveTranslator;
        private CompanyDepartmentJobRoleTranslator companyDepartmentJobRoleTranslator;
        private DepartmentTranslator departmentTranslator;
        private PeriodTranslator PeriodTranslator;

        public MetricsTranslator()
        {
            metricsPerspectiveTranslator = new MetricsPerspectiveTranslator();
            companyDepartmentJobRoleTranslator = new CompanyDepartmentJobRoleTranslator();
            departmentTranslator = new DepartmentTranslator();
            PeriodTranslator = new PeriodTranslator();
        }

        public override Metrics TranslateToModel(METRIC entity)
        {
            try
            {
               
                Metrics metrics = null;
                if (entity != null)
                {
                    metrics = new Metrics();
                    metrics.Id = entity.Metric_ID;
                    metrics.Perspective = metricsPerspectiveTranslator.Translate(entity.METRIC_PERSPECTIVE);
                    metrics.CompanyDepartmentJobRole = companyDepartmentJobRoleTranslator.Translate(entity.COMPANY_DEPARTMENT_JOB_ROLE);
                    metrics.Kpi = entity.Kpi;
                    metrics.Measure = entity.Measure;
                    metrics.DataSource = entity.Data_Source;
                    metrics.ResponsibleDepartment = departmentTranslator.Translate(entity.DEPARTMENT);
                    metrics.Target = entity.Target;
                    metrics.Score = entity.Score;
                    metrics.Period = PeriodTranslator.Translate(entity.PERIOD);
                    metrics.JobRoleName = metrics.CompanyDepartmentJobRole.JobRole.Name;
                }

                return metrics;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override METRIC TranslateToEntity(Metrics metrics)
        {
            try
            {
                METRIC entity = null;
                if (metrics != null)
                {
                    entity = new METRIC();
                    entity.Metric_ID = metrics.Id;
                    entity.Metric_Perspective_ID = metrics.Perspective.Id;
                    entity.Company_Department_Job_Role_ID = metrics.CompanyDepartmentJobRole.Id;
                    entity.Kpi = metrics.Kpi;
                    entity.Measure = metrics.Measure;
                    entity.Data_Source = metrics.DataSource;
                    entity.Rsponsible_Department_ID = metrics.ResponsibleDepartment.Id;
                    entity.Target = metrics.Target;
                    entity.Score = metrics.Score;
                    entity.Period_ID = metrics.Period.Id;
                }

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }



    }





}
