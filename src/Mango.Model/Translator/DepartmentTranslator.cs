using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Model;
using Mango.Data;

namespace Mango.Model.Translator
{
    public class DepartmentTranslator : TranslatorBase<Department, DEPARTMENT>
    {
        public override Department TranslateToModel(DEPARTMENT departmentEntity)
        {
            try
            {
                Department department = null;
                if (departmentEntity != null)
                {
                    department = new Department();
                    department.Id = departmentEntity.Department_ID;
                    department.Name = departmentEntity.Department_Name;
                }

                return department;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override DEPARTMENT TranslateToEntity(Department department)
        {
            try
            {
                DEPARTMENT departmentEntity = null;
                if (department != null)
                {
                    departmentEntity = new DEPARTMENT();
                    departmentEntity.Department_ID = department.Id;
                    departmentEntity.Department_Name = department.Name;
                }

                return departmentEntity;
            }
            catch (Exception)
            {
                throw;
            }
        }




    }
}
