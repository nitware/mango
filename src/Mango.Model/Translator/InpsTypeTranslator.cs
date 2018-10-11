using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;

namespace Mango.Model.Translator
{
    public class InpsTypeTranslator : TranslatorBase<InpsType, INPS_TYPE>
    {
        public override InpsType TranslateToModel(INPS_TYPE entity)
        {
            try
            {
                InpsType inpsType = null;
                if (entity != null)
                {
                    inpsType = new InpsType();
                    inpsType.Id = entity.Inps_Type_Id;
                    inpsType.Name = entity.Inps_Type_Name;
                    inpsType.Description = entity.Inps_Type_Description;
                }

                return inpsType;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override INPS_TYPE TranslateToEntity(InpsType inpsType)
        {
            try
            {
                INPS_TYPE entity = null;
                if (inpsType != null)
                {
                    entity = new INPS_TYPE();
                    entity.Inps_Type_Id = inpsType.Id;
                    entity.Inps_Type_Name = inpsType.Name;
                    entity.Inps_Type_Description = inpsType.Description;
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
