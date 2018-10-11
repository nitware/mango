using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mango.Data;
using Mango.Model.Model;

namespace Mango.Model.Translator
{
    public class InpsTranslator : TranslatorBase<Inps, INPS>
    {
        private StaffTranslator staffTranslator;
        private DepartmentTranslator departmentTranslator;
        private PeriodTranslator PeriodTranslator;
        private InpsTypeTranslator inpsTypeTranslator;

        //private MetricsPerspectiveTranslator metricsPerspectiveTranslator;

        public InpsTranslator()
        {
            staffTranslator = new StaffTranslator();
            departmentTranslator = new DepartmentTranslator();
            PeriodTranslator = new PeriodTranslator();
            inpsTypeTranslator = new InpsTypeTranslator();

            //metricsPerspectiveTranslator = new MetricsPerspectiveTranslator();
        }

        public override Inps TranslateToModel(INPS entity)
        {
            try
            {
                Inps nps = null;
                if (entity != null)
                {
                    nps = new Inps();
                    nps.Id = entity.Inps_ID;
                    nps.Type = inpsTypeTranslator.Translate(entity.INPS_TYPE);
                    nps.Staff = staffTranslator.Translate(entity.STAFF);
                    nps.Kpi = entity.Kpi;
                    nps.Measure = entity.Measure;
                    nps.DataSource = entity.Data_Source;
                    nps.ResponsibleDepartment = departmentTranslator.Translate(entity.DEPARTMENT);
                    nps.Target = entity.Target;
                    nps.Score = entity.Score;
                    nps.Period = PeriodTranslator.Translate(entity.PERIOD);
                }

                return nps;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override INPS TranslateToEntity(Inps nps)
        {
            try
            {
                INPS entity = null;
                if (nps != null)
                {
                    entity = new INPS();
                    entity.Inps_ID = nps.Id;
                    entity.Inps_Type_Id = nps.Type.Id;
                    entity.Staff_ID = nps.Staff.Id;
                    entity.Kpi = nps.Kpi;
                    entity.Measure = nps.Measure;
                    entity.Data_Source = nps.DataSource;
                    entity.Responsible_Department_ID = nps.ResponsibleDepartment.Id;
                    entity.Target = nps.Target;
                    entity.Score = nps.Score;
                    entity.Period_ID = nps.Period.Id;
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
