using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model;
using Mango.Model.Translator;

namespace Mango.Business
{
    public class RoleRightLogic : BusinessLogicBase<RoleRight, ROLE_RIGHT>
    {
        public RoleRightLogic()
        {
            base.translator = new RoleRightTranslator();
        }

        public List<RoleRight> Create(Role role, List<Right> rights)
        {
            try
            {
                List<RoleRight> roleRights = null;
                if (role != null && rights != null)
                {
                    roleRights = new List<RoleRight>();
                    foreach (Right right in rights)
                    {
                        if (right.IsInRole)
                        {
                            RoleRight roleRight = new RoleRight();
                            roleRight.Role = role;
                            roleRight.Right = right;

                            roleRights.Add(roleRight);
                        }
                    }
                }

                return roleRights;
            }
            catch (Exception)
            {
                throw;
            }

        }



    }


}
