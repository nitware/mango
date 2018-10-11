using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;

namespace Mango.Model.Translator
{
    public class PeriodTranslator : TranslatorBase<Period, PERIOD>
    {
        private StatusTranslator statusTranslator;
        private PeriodTypeTranslator periodTypeTranslator;

        public PeriodTranslator()
        {
            statusTranslator = new StatusTranslator();
            periodTypeTranslator = new PeriodTypeTranslator();
        }

        public override Period TranslateToModel(PERIOD entity)
        {
            try
            {
                Period period = null;
                if (entity != null)
                {
                    period = new Period();
                    period.Id = entity.Period_ID;
                    period.Status = statusTranslator.Translate(entity.STATUS);

                    //entity.CURRENT_PERIOD
                    //period.Name = entity.Period_Name;
                    //period.Type = entity.Type;
                    
                                       
                    period.Span = entity.Span;
                    period.StartDate = entity.Start_Date;
                    period.EndDate = entity.End_Date;
                    period.Type = periodTypeTranslator.Translate(entity.PERIOD_TYPE);
                    period.Year = entity.Year;

                    period.Name = entity.Year + " " + period.Type.Name;
                }

                return period;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override PERIOD TranslateToEntity(Period period)
        {
            try
            {
                PERIOD entity = null;
                if (period != null)
                {
                    entity = new PERIOD();
                    entity.Period_ID = period.Id;
                    entity.Status_ID = period.Status.Id;
                    entity.Period_Name = period.Year + " " + period.Type.Name;

                    //entity.Period_Name = period.Name;
                    //entity.Type = period.Type;

                   

                    entity.Span = period.Span;
                    entity.Start_Date = period.StartDate;
                    entity.End_Date  = period.EndDate;
                    entity.Period_Type_Id = period.Type.Id;
                    entity.Year = period.Year;
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
