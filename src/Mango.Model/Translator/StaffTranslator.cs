using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model;

namespace Mango.Model.Translator
{
    public class StaffTranslator : TranslatorBase<Staff, STAFF>
    {
        private RoleTranslator roleTranslator;
        private CompanyTranslator companyTranslator;

        public StaffTranslator()
        {
            roleTranslator = new RoleTranslator();
            companyTranslator = new CompanyTranslator();
        }

        public override Staff TranslateToModel(STAFF entity)
        {
            try
            {
                Staff staff = null;
                if (entity != null)
                {
                    staff = new Staff();
                    staff.Id = entity.Staff_ID;
                    staff.FirstName = entity.First_Name;
                    staff.LastName = entity.Last_Name;
                    staff.OtherName = entity.Other_Name;
                    staff.Name = staff.FirstName + " " + staff.LastName;
                    staff.FullName = staff.FirstName + " " + staff.LastName + " " + staff.OtherName;
                    staff.LoginName = entity.Login_Name;
                    staff.Email = entity.Email;
                    staff.IsActive = entity.Is_Active.HasValue ? entity.Is_Active.Value : false;
                    staff.Role = roleTranslator.Translate(entity.ROLE);
                    staff.Company = companyTranslator.Translate(entity.COMPANY);
                }

                return staff;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override STAFF TranslateToEntity(Staff staff)
        {
            try
            {
                STAFF entity = null;
                if (staff != null)
                {
                    entity = new STAFF();
                    entity.Staff_ID = staff.Id;
                    entity.First_Name = staff.FirstName;
                    entity.Last_Name = staff.LastName;
                    entity.Other_Name = staff.OtherName;
                    entity.Login_Name = staff.LoginName;
                    entity.Email = staff.Email;
                    entity.Role_Id = staff.Role.Id;
                    entity.Is_Active = staff.IsActive;
                    entity.Company_Id = staff.Company.Id;
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
