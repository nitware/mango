using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model;

namespace Mango.Model.Translator
{
    public class RoleTranslator : TranslatorBase<Role, ROLE>
    {
        private RightTranslator rightTranslator;

        public RoleTranslator()
        {
            rightTranslator = new RightTranslator();
        }

        public override Role TranslateToModel(ROLE roleEntity)
        {
            try
            {
                Role role = null;
                if (roleEntity != null)
                {
                    role = new Role();
                    role.Id = roleEntity.Role_Id;
                    role.Name = roleEntity.Role_Name;
                    role.Description = roleEntity.Role_Description;

                    role.Rights = rightTranslator.TranslateToModel(roleEntity.ROLE_RIGHT);

                    role.UserRight = new UserRight();
                    role.UserRight.Rights = role.Rights;
                    role.UserRight.Set();
                }

                return role;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override ROLE TranslateToEntity(Role role)
        {
            try
            {
                ROLE roleEntity = null;
                if (role != null)
                {
                    roleEntity = new ROLE();
                    roleEntity.Role_Id = role.Id;
                    roleEntity.Role_Name = role.Name;
                    roleEntity.Role_Description = role.Description;
                }

                return roleEntity;
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
