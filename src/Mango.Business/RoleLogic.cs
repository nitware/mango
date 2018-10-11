using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Mango.Data;
using Mango.Model;
using Mango.Model.Translator;

namespace Mango.Business
{
    public class RoleLogic : BusinessLogicBase<Role, ROLE>
    {
        private RightLogic rightLogic;
        private RoleRightLogic roleRightLogic;

        public RoleLogic()
        {
            base.translator = new RoleTranslator();
            rightLogic = new RightLogic();
            roleRightLogic = new RoleRightLogic();
        }

        public override List<Role> GetAll()
        {
            try
            {
                List<Role> roles = base.GetAll();
                return SetPersonRightView(roles);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Role> GetAll(Staff staff)
        {
            try
            {
                List<Role> roles = new List<Role>();
                if (staff != null)
                {
                    if (staff.Role.Id != 2)
                    {
                        Func<ROLE, bool> selector = r => r.Role_Id != 2 && r.Role_Id != staff.Role.Id;
                        roles = base.GetModelsBy(selector);
                    }
                    else
                    {
                        roles = base.GetAll();
                    }
                }

                return SetPersonRightView(roles);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Role Get(Staff staff)
        {
            try
            {
                Role role = null;
                if (staff != null)
                {
                    Func<ROLE, bool> selector = r => r.Role_Id == staff.Role.Id;
                    role = base.GetModelBy(selector);
                }

                return role;

                //return SetPersonRightView(Role role)
                //return SetPersonRightView(role);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Modify(Role role)
        {
            try
            {
                Func<ROLE, bool> selector = r => r.Role_Id == role.Id;
                ROLE roleEntity = GetEntityBy(selector);
                roleEntity.Role_Name = role.Name;
                roleEntity.Role_Description = role.Description;

                //int rowsAffected = base.ContextManager.SummitChanges();

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

        public bool Remove(Role role)
        {
            try
            {
                Func<ROLE, bool> selector = r => r.Role_Id == role.Id;
                bool suceeded = base.Remove(selector);
                //base.ContextManager.SummitChanges();
                base.repository.SaveChanges();
                return suceeded;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Role SetPersonRightView(Role role)
        {
            try
            {
                List<Right> rightsInRole = role.Rights;
                List<Right> rights = rightLogic.GetAll(); //get all rights

                foreach (Right right in rights)
                {
                    foreach (Right rightInRole in rightsInRole)
                    {
                        if (rightInRole.Id == right.Id)
                        {
                            right.IsInRole = true;
                        }
                    }
                }

                role.UserRight = new UserRight();
                role.UserRight.View = rights;

                return role;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<Role> SetPersonRightView(List<Role> roles)
        {
            try
            {
                List<Role> newRoles = new List<Role>();
                if (roles != null)
                {
                    foreach (Role role in roles)
                    {
                        Role newRole = SetPersonRightView(role);
                        newRoles.Add(newRole);
                    }
                }

                return newRoles;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AssignRightToRole(Role role)
        {
            try
            {
                bool isSuccessful = false;
                if (role != null)
                {
                    Func<ROLE_RIGHT, bool> selector = rr => rr.Role_Id == role.Id;

                    if (roleRightLogic.Remove(selector))
                    {
                        List<RoleRight> roleRights = new List<RoleRight>();
                        roleRights = roleRightLogic.Create(role, role.UserRight.View);
                        if (roleRights != null)
                        {
                            isSuccessful = roleRightLogic.Add(roleRights) > 0 ? true : false;
                        }
                    }
                }

                return isSuccessful;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }


}
