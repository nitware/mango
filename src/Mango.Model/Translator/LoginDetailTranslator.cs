using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model;

namespace Mango.Model.Translator
{
    public class LoginDetailTranslator : TranslatorBase<LoginDetail, STAFF_LOGIN>
    {
        private StaffTranslator staffTranslator;

        public LoginDetailTranslator()
        {
            staffTranslator = new StaffTranslator();
        }

        public override LoginDetail TranslateToModel(STAFF_LOGIN staffLoginEntity)
        {
            try
            {
                LoginDetail staffLogin = null;
                if (staffLoginEntity != null)
                {
                    staffLogin = new LoginDetail();
                    staffLogin.Staff = staffTranslator.Translate(staffLoginEntity.STAFF);
                    staffLogin.Username = staffLoginEntity.Username;
                    staffLogin.Password = staffLoginEntity.Password;
                    staffLogin.IsLocked = staffLoginEntity.Is_Locked;
                    staffLogin.IsActivated = staffLoginEntity.Is_Activated;
                    staffLogin.IsFirstLogon = staffLoginEntity.Is_First_Logon;
                }

                return staffLogin;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override STAFF_LOGIN TranslateToEntity(LoginDetail staffLogin)
        {
            try
            {
                STAFF_LOGIN staffLoginEntity = null;
                if (staffLogin != null)
                {
                    staffLoginEntity = new STAFF_LOGIN();
                    staffLoginEntity.Staff_ID = staffLogin.Staff.Id;
                    staffLoginEntity.Username = staffLogin.Username;
                    staffLoginEntity.Password = staffLogin.Password;
                    staffLoginEntity.Is_Locked = staffLogin.IsLocked;
                    staffLoginEntity.Is_Activated = staffLogin.IsActivated;
                    staffLoginEntity.Is_First_Logon = staffLogin.IsFirstLogon;
                }

                return staffLoginEntity;
            }
            catch (Exception)
            {
                throw;
            }
        }





    }

}
