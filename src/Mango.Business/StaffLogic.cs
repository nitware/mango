using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Mango.Data;
using Mango.Model;
using Mango.Model.Translator;
using System.Transactions;

namespace Mango.Business
{
    public class StaffLogic : BusinessLogicBase<Staff, STAFF> 
    {
        private LoginDetailLogic loginDetailLogic;
        private JobRoleSupervisorLogic jobRoleSupervisorLogic;
        private AppraisalLogic appraisalLogic;
        
        public StaffLogic()
        {
            base.translator = new StaffTranslator();
            loginDetailLogic = new LoginDetailLogic();
        }

        public Staff Get(string id)
        {
            try
            {
                Func<STAFF, bool> selector = rb => rb.Staff_ID == id;
                return GetModelBy(selector);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override List<Staff> GetAll()
        {
            try
            {
                Func<STAFF, bool> selector = u => u.Is_Active == true && u.Company_Id == 2;
                return base.GetModelsBy(selector).OrderBy(s => s.FullName).OrderBy(s => s.Name).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Staff> GetAll(Staff staff)
        {
            try
            {
                List<Staff> staffs = new List<Staff>();
                if (staff != null)
                {
                    if (staff.Role.Id != 2)
                    {
                        Func<STAFF, bool> selector = u => u.Role_Id != 2 && u.Role_Id != staff.Role.Id && u.Company_Id == 2 && u.Is_Active == true;
                        staffs = base.GetModelsBy(selector);
                    }
                    else
                    {
                        Func<STAFF, bool> selector = s => s.Is_Active == true && s.Company_Id == 2;
                        staffs = base.GetModelsBy(selector);
                    }
                }

                return staffs.OrderBy(s => s.FullName).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override Staff Add(Staff staff)
        {
            //const string Domain = "firstcitygroup.com";
            const string Domain = "fcmb.com";

            try
            {
                staff.LoginName = CreateUserName(staff);
                staff.Email = staff.LoginName + "@" + Domain;
                staff.IsActive = true;
                staff.Role = new Role() { Id = 1 };

                using (TransactionScope transaction = new TransactionScope())
                {
                    Staff newStaff = base.Add(staff);
                    if (newStaff != null)
                    {
                        LoginDetail newLogin = loginDetailLogic.Add(newStaff);
                        if (newLogin != null)
                        {
                            transaction.Complete();
                            return newStaff;
                        }
                    }
                }

                //return base.Add(staff);

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string CreateUserName(Staff staff)
        {
            try
            {
                string smallFirstName = staff.FirstName.Substring(1, staff.FirstName.Length - 1).ToLower();
                string smallLastName = staff.LastName.Substring(1, staff.LastName.Length - 1).ToLower();
                string firstNameFirstChar = staff.FirstName.Substring(0, 1).ToUpper();
                string lastNameFirstChar = staff.LastName.Substring(0, 1).ToUpper();

                return firstNameFirstChar + smallFirstName + "." + lastNameFirstChar + smallLastName;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Staff GetByUserName(string userName)
        {
            try
            {
                Func<STAFF, bool> selector = p => p.Login_Name.Equals(userName, StringComparison.OrdinalIgnoreCase) && p.Company_Id == 2;
                return GetModelBy(selector);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Staff> GetByName(string name)
        {
            try
            {
                Func<STAFF, bool> selector = p => p.Last_Name.StartsWith(name, true, null) && p.Company_Id == 2;
                return GetModelsBy(selector);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Modify(Staff staff)
        {
            const string TRY_AGAIN = "Please try again, but contact your system administrator after three unsuccessful trials.";

            try
            {
                Func<STAFF, bool> selector = u => u.Staff_ID == staff.Id;
                STAFF entity = GetEntityBy(selector);

                entity.Staff_ID = staff.Id;
                entity.First_Name = staff.FirstName;
                entity.Last_Name = staff.LastName;
                entity.Other_Name = staff.OtherName;
                entity.Login_Name = CreateUserName(staff);
                entity.Email = staff.Email;
                entity.Role_Id = staff.Role.Id;
                entity.Is_Active = staff.IsActive;
                entity.Company_Id = staff.Company.Id;

                using (TransactionScope transaction = new TransactionScope())
                {
                    int rowsAffected = base.repository.SaveChanges();
                    if (rowsAffected <= 0)
                    {
                        throw new Exception("Staff detail modification failed! " + TRY_AGAIN);
                    }

                    LoginDetail staffLogin = loginDetailLogic.GetModelBy(sl => sl.Staff_ID == staff.Id);
                    staffLogin.Username = entity.Login_Name;

                    bool updated = loginDetailLogic.Modify(staffLogin);
                    if (updated == false)
                    {
                        throw new Exception("Staff Username update failed! " + TRY_AGAIN);
                    }

                    transaction.Complete();
                    return updated;
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

        public bool Remove(Staff staff)
        {
            try
            {
                bool removed = false;
                using (TransactionScope transaction = new TransactionScope())
                {
                    loginDetailLogic.Remove(staff);

                    Func<STAFF, bool> selector = s => s.Staff_ID == staff.Id;
                    removed = base.Remove(selector);

                    repository.SaveChanges();
                    transaction.Complete();
                }

                return removed;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Staff> FilterAppraisees(List<Staff> appraisees, int periodId)
        {
            try
            {
                if (appraisees == null || appraisees.Count == 0)
                {
                    return appraisees;
                }

                appraisalLogic = new AppraisalLogic();
                jobRoleSupervisorLogic = new JobRoleSupervisorLogic();

                List<Staff> staffs = new List<Staff>();
                foreach (Staff staff in appraisees)
                {
                    //Func<JOB_ROLE_SUPERVISOR, bool> noOfAppraiseesSelector = jrs => jrs.Supervisor_Company_Department_Job_Role_ID == staff.CompanyDepartmentJobRoleId && jrs.Period_Id == periodId;
                    //int noOfAppraisees = jobRoleSupervisorLogic.GetModelsBy(noOfAppraiseesSelector).Count;


                    int noOfAppraisees = GetTotalAppraiseeCount(periodId, staff);
                                        
                    Func<APPRAISAL_HEADER, bool> noOfStaffsAppraiseedSelector = ah => ah.Supervisor_ID == staff.Id && ah.Period_ID == periodId;
                    int noOfStaffsAppraiseed = appraisalLogic.GetModelsBy(noOfStaffsAppraiseedSelector).Count;

                    if (noOfAppraisees == noOfStaffsAppraiseed)
                    {
                        staffs.Add(staff);
                    }
                }

                return staffs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private int GetTotalAppraiseeCount(int periodId, Staff staff)
        {
            try
            {
                var appraiseesUnderStaff = (from jrs in base.repository.Fetch<VW_JOB_ROLE_SUPERVISOR>()
                                            where jrs.Supervisor_Company_Department_Job_Role_ID == staff.CompanyDepartmentJobRoleId && jrs.Period_ID == periodId && jrs.Company_ID == 2
                                            select jrs
                               ).ToList();
                                
                return appraiseesUnderStaff.Count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        


    }


}
