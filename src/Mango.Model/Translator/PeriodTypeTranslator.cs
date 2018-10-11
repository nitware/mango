using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;

namespace Mango.Model.Translator
{
    public class PeriodTypeTranslator : TranslatorBase<PeriodType, PERIOD_TYPE>
    {
        public override PeriodType TranslateToModel(PERIOD_TYPE entity)
        {
            try
            {
                PeriodType periodType = null;
                if (entity != null)
                {
                    periodType = new PeriodType();
                    periodType.Id = entity.Period_Type_Id;
                    periodType.Name = entity.Period_Type_Name;
                    periodType.Description = entity.Period_Type_Description;
                }

                return periodType;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override PERIOD_TYPE TranslateToEntity(PeriodType periodType)
        {
            try
            {
                PERIOD_TYPE entity = null;
                if (periodType != null)
                {
                    entity = new PERIOD_TYPE();
                    entity.Period_Type_Id = periodType.Id;
                    entity.Period_Type_Name = periodType.Name;
                    entity.Period_Type_Description = periodType.Description;
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
