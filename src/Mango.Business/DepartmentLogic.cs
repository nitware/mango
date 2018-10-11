using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model;
using Mango.Model.Translator;
using System.Data;

namespace Mango.Business
{
    public class DepartmentLogic : BusinessLogicBase<Department, DEPARTMENT>
    {
        public DepartmentLogic()
        {
            base.translator = new DepartmentTranslator();
        }

        //public override List<Department> GetAll()
        //{
        //    try
        //    {
        //        List<Department> departments = base.GetAll().Where(c => c..Id == 2).ToList();
        //        return companyDepartmentJobRoles.OrderBy(c => c.JobRole.Name).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public Department GetBy(string id)
        {
            try
            {
                Func<DEPARTMENT, bool> selector = d => d.Department_ID.Trim() == id.Trim();
                return base.GetModelBy(selector);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Modify(Department department)
        {
            try
            {
                Func<DEPARTMENT, bool> predicate = d => d.Department_ID == department.Id;
                DEPARTMENT entity = GetEntityBy(predicate);

                entity.Department_Name = department.Name;

                int rowsAffected = repository.SaveChanges();
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    throw new Exception(NoItemModified);
                }
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException(ArgumentNullException);
            }
            catch (UpdateException)
            {
                throw new UpdateException(UpdateException);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Remove(Department department)
        {
            try
            {
                Func<DEPARTMENT, bool> selector = d => d.Department_ID == department.Id;
                bool suceeded = base.Remove(selector);
                repository.SaveChanges();
                return suceeded;
            }
            catch (Exception)
            {
                throw;
            }
        }




    }
}
