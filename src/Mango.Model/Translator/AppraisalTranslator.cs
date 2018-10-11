using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;

namespace Mango.Model.Translator
{
    public class AppraisalTranslator : TranslatorBase<Appraisal, APPRAISAL_HEADER>
    {
        private StatusTranslator statusTranslator;
        private StaffTranslator staffTranslator;
        private PeriodTranslator periodTranslator;

        public AppraisalTranslator()
        {
            staffTranslator = new StaffTranslator();
            periodTranslator = new PeriodTranslator();
            statusTranslator = new StatusTranslator();
        }

        public override Appraisal TranslateToModel(APPRAISAL_HEADER entity)
        {
            try
            {
                Appraisal model = null;
                if (entity != null)
                {
                    model = new Appraisal();
                    model.Id = entity.Appraisal_Header_ID;
                    model.Period = periodTranslator.Translate(entity.PERIOD);
                    model.Staff = staffTranslator.Translate(entity.STAFF);
                    model.Supervisor = staffTranslator.Translate(entity.STAFF1);
                    model.AppraisalDate = entity.Appraisal_Date;
                    model.ResponseDate = entity.Staff_Response_Date;
                    model.Hod = staffTranslator.Translate(entity.STAFF2);
                    model.HodAppraisalDate = entity.Hod_Appraisal_Date;
                    model.Status = statusTranslator.Translate(entity.STATUS);
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override APPRAISAL_HEADER TranslateToEntity(Appraisal model)
        {
            try
            {
                APPRAISAL_HEADER entity = null;
                if (model != null)
                {
                    entity = new APPRAISAL_HEADER();
                    entity.Appraisal_Header_ID = model.Id;
                    entity.Period_ID = model.Period.Id;
                    entity.Staff_ID = model.Staff.Id;
                    entity.Supervisor_ID = model.Supervisor.Id;
                    entity.Appraisal_Date = model.AppraisalDate;
                    entity.Staff_Response_Date = model.ResponseDate;
                    entity.Hod_ID = model.Hod.Id;
                    entity.Hod_Appraisal_Date = model.HodAppraisalDate;
                    entity.Status_ID = model.Status.Id;
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
