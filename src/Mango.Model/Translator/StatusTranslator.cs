using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;

namespace Mango.Model.Translator
{
    public class StatusTranslator : TranslatorBase<Status, STATUS>
    {
        public override Status TranslateToModel(STATUS entity)
        {
            try
            {
                Status status = null;
                if (entity != null)
                {
                    status = new Status();
                    status.Id = entity.Status_ID;
                    status.Name = entity.Status_Name;
                }

                return status;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override STATUS TranslateToEntity(Status status)
        {
            try
            {
                STATUS entity = null;
                if (status != null)
                {
                    entity = new STATUS();
                    entity.Status_ID = status.Id;
                    entity.Status_Name = status.Name;
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
