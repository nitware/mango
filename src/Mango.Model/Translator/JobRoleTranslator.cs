using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;

namespace Mango.Model.Translator
{
    public class JobRoleTranslator : TranslatorBase<JobRole, JOB_ROLE>
    {
        public override JobRole TranslateToModel(JOB_ROLE entity)
        {
            try
            {
                JobRole jobRole = null;
                if (entity != null)
                {
                    jobRole = new JobRole();
                    jobRole.Id = entity.Job_Role_ID;
                    jobRole.Name = entity.Job_Role_Name;
                }

                return jobRole;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override JOB_ROLE TranslateToEntity(JobRole jobRole)
        {
            try
            {
                JOB_ROLE entity = null;
                if (jobRole != null)
                {
                    entity = new JOB_ROLE();
                    entity.Job_Role_ID = jobRole.Id;
                    entity.Job_Role_Name = jobRole.Name;
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
