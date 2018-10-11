using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;

namespace Mango.Model.Translator
{
    public class LevelTranslator : TranslatorBase<Level, JOB_ROLE_LEVEL>
    {
        public override Level TranslateToModel(JOB_ROLE_LEVEL entity)
        {
            try
            {
                Level level = null;
                if (entity != null)
                {
                    level = new Level();
                    level.Id = entity.Job_Role_Level_ID;
                    level.Name = entity.Job_Role_Level_Name;
                }

                return level;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override JOB_ROLE_LEVEL TranslateToEntity(Level level)
        {
            try
            {
                JOB_ROLE_LEVEL entity = null;
                if (level != null)
                {
                    entity = new JOB_ROLE_LEVEL();
                    entity.Job_Role_Level_ID = level.Id;
                    entity.Job_Role_Level_Name = level.Name;
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
