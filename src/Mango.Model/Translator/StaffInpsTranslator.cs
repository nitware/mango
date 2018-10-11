using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;


namespace Mango.Model.Translator
{
    public class StaffInpsTranslator : TranslatorBase<StaffInps, STAFF_INPS>
    {
        
        private InpsTranslator inpsTranslator;
        private AppraisalTranslator appraisalTranslator;

        public StaffInpsTranslator()
        {
            inpsTranslator = new InpsTranslator();
            appraisalTranslator = new AppraisalTranslator();
        }

        public override StaffInps TranslateToModel(STAFF_INPS entity)
        {
            try
            {
                StaffInps model = null;
                if (entity != null)
                {
                    model = new StaffInps();
                    model.Id = entity.Staff_Inps_ID;
                    model.Appraisal = appraisalTranslator.Translate(entity.APPRAISAL_HEADER);
                    model.Inps = inpsTranslator.Translate(entity.INPS);
                    model.Score = entity.Score;
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override STAFF_INPS TranslateToEntity(StaffInps model)
        {
            try
            {
                STAFF_INPS entity = null;
                if (model != null)
                {
                    entity = new STAFF_INPS();
                    entity.Staff_Inps_ID = model.Id;
                    entity.Appraisal_Header_ID = model.Appraisal.Id;
                    entity.Inps_ID = model.Inps.Id;
                    entity.Score = model.Score;

                    if (model.StaffMetric != null)
                    {
                        entity.Staff_Metric_ID = model.StaffMetric.Id;
                    }
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
