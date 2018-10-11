using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Collections.ObjectModel;
using System.Data;
using Mango.Data;
using Mango.Model;
using Mango.Data.Interfaces;

namespace Mango.Business.DbFacade
{
    public class StaffDbFacade
    {
        private IStaffDb staffDb;
        private MetricDbFacade metricDbFacade;
        private ICurrentPeriodDb currentPeriodDb;
               
        public StaffDbFacade()
        {
            staffDb = new StaffDb();
            metricDbFacade = new MetricDbFacade();
        }

        public Staff GetStaff(string staffId, int periodId)
        {
            Staff staff = new Staff();

            DataSet dsStaff = staffDb.SelectStaffByStaffId(staffId, periodId);
            if (dsStaff != null)
            {
                if (dsStaff.Tables[0].Rows.Count > 0)
                {
                    staff.Id = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_STAFF_ID]);
                    staff.Name = Convert.ToString(dsStaff.Tables[0].Rows[0]["Name"]);
                    staff.LoginName = Convert.ToString(dsStaff.Tables[0].Rows[0]["Login_Name"]);
                    staff.Email = Convert.ToString(dsStaff.Tables[0].Rows[0]["Email"]);
                    staff.Company.Name = Convert.ToString(dsStaff.Tables[0].Rows[0]["Company_Name"]);
                    staff.Department.Name = Convert.ToString(dsStaff.Tables[0].Rows[0]["Department_Name"]);
                    staff.JobRole.Id = Convert.ToInt32(dsStaff.Tables[0].Rows[0]["Job_Role_ID"]);
                    staff.JobRole.Name = Convert.ToString(dsStaff.Tables[0].Rows[0]["Job_Role_Name"]);
                    staff.Level.Name = Convert.ToString(dsStaff.Tables[0].Rows[0]["Job_Role_Level_Name"]);
                    staff.Level.Id = Convert.ToString(dsStaff.Tables[0].Rows[0]["Job_Role_Level_ID"]);
                    staff.CompanyDepartmentJobRoleId = Convert.ToInt32(dsStaff.Tables[0].Rows[0][staffDb.FIELD_COMPANY_DEPARTMENT_JOB_ROLE_ID]);
                    staff.Location.Name = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_LOCATION_NAME]);

                    ////tem code text jumbling
                    ////staff.Id = "0000";
                    //staff.Name = UtilityLogic.JumbbleText(staff.Name);
                    //staff.LoginName = UtilityLogic.JumbbleText(staff.LoginName);
                    //staff.Email = UtilityLogic.JumbbleText(staff.Email);
                    //staff.Company.Name = UtilityLogic.JumbbleText(staff.Company.Name);
                    //staff.Department.Name = UtilityLogic.JumbbleText(staff.Department.Name);
                    //staff.JobRole.Name = UtilityLogic.JumbbleText(staff.JobRole.Name);
                    //staff.Level.Name = UtilityLogic.JumbbleText(staff.Level.Name);
                    ////staff.Level.Id = UtilityLogic.JumbbleText(staff.Level.Id);
                    //staff.Location.Name = UtilityLogic.JumbbleText(staff.Location.Name);


                    staff.FormatNameToPascalCasing();

                    return staff;
                }
            }

            return null;
        }

        public Staff GetStaffByLoginName(string loginName)
        {
            Staff staff = new Staff();
            currentPeriodDb = new CurrentPeriodDb();

            int periodId = 0;
            DataSet dsCurrentPeriod = currentPeriodDb.SelectAllCurrentPeriod();
            if (dsCurrentPeriod != null)
            {
                periodId = Convert.ToInt32(dsCurrentPeriod.Tables[0].Rows[0][currentPeriodDb.FIELD_PERIOD_ID]);
            }

            if (periodId <= 0)
            {
                throw new Exception("No current period exist in the system! Please contact your system administrator.");
            }

            DataSet dsStaff = staffDb.SelectStaffByLoginName(loginName, periodId);
            if (dsStaff != null)
            {
                if (dsStaff.Tables[0].Rows.Count > 0)
                {
                    staff.Id = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_STAFF_ID]);
                    staff.Name = Convert.ToString(dsStaff.Tables[0].Rows[0]["Name"]);
                    staff.LoginName = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_LOGIN_NAME]);
                    staff.Email = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_EMAIL]);
                    staff.Company.Name = Convert.ToString(dsStaff.Tables[0].Rows[0]["Company_Name"]);
                    staff.Department.Name = Convert.ToString(dsStaff.Tables[0].Rows[0]["Department_Name"]);
                    staff.JobRole.Id = Convert.ToInt32(dsStaff.Tables[0].Rows[0]["Job_Role_ID"]);
                    staff.JobRole.Name = Convert.ToString(dsStaff.Tables[0].Rows[0]["Job_Role_Name"]);
                    staff.Level.Name = Convert.ToString(dsStaff.Tables[0].Rows[0]["Job_Role_Level_Name"]);
                    staff.Level.Id = Convert.ToString(dsStaff.Tables[0].Rows[0]["Job_Role_Level_ID"]);
                    staff.CompanyDepartmentJobRoleId = Convert.ToInt32(dsStaff.Tables[0].Rows[0][staffDb.FIELD_COMPANY_DEPARTMENT_JOB_ROLE_ID]);
                    staff.Location.Name = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_LOCATION_NAME]);
                    staff.MetricsCount = metricDbFacade.CountMetrics(staff.CompanyDepartmentJobRoleId, periodId);
                    staff.IsActive = Convert.ToBoolean(dsStaff.Tables[0].Rows[0]["Is_Active"]);

                    staff.Role = new Role();
                    staff.Role.Id = Convert.ToInt32(dsStaff.Tables[0].Rows[0]["Role_Id"]);

                    RoleLogic roleLogic = new RoleLogic();
                    staff.Role = roleLogic.Get(staff);

                    staff.FormatNameToPascalCasing();

                    return staff;
                }
            }

            return null;
        }


        public List<Staff> LoadSupervisorAppraisees(int companyDepartmentJobRoleId, int periodId)
        {
            try
            {
                List<Staff> staffs = new List<Staff>();
                DataSet dsStaff = staffDb.SelectStaffByCompanyDepartmentJobRoleIdAndPeriodId(companyDepartmentJobRoleId, periodId);

                string s = companyDepartmentJobRoleId.ToString();
                if (dsStaff != null)
                {
                    if (dsStaff.Tables[0].Rows.Count > 0)
                    {
                        //Staff stf = new Staff();
                        //stf.Name = "< Select Appraisee >";
                        //staffs.Add(stf);

                        for (int i = 0; i < dsStaff.Tables[0].Rows.Count; i++)
                        {
                            Staff staff = new Staff();
                            staff.Id = Convert.ToString(dsStaff.Tables[0].Rows[i][staffDb.FIELD_STAFF_ID]);
                            staff.Name = Convert.ToString(dsStaff.Tables[0].Rows[i][staffDb.FIELD_LAST_NAME]);
                            staff.JobRoleLevel.Id = Convert.ToString(dsStaff.Tables[0].Rows[i][staffDb.FIELD_JOB_ROLE_LEVEL_ID]);
                            staff.CompanyDepartmentJobRoleId = Convert.ToInt32(dsStaff.Tables[0].Rows[i]["Staff_Company_Department_Job_Role_ID"]);
                            staff.Email = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_EMAIL]);
                            staff.Location.Name = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_LOCATION_NAME]);
                            //staff.FormatNameToPascalcasing();

                            staffs.Add(staff);
                        }

                        return staffs;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public List<Staff> LoadSupervisorAppraisees(int companyDepartmentJobRoleId)
        //{
        //    try
        //    {
        //        List<Staff> staffs = new List<Staff>();
        //        DataSet dsStaff = staffDb.SelectStaffByCompanyDepartmentJobRoleId(companyDepartmentJobRoleId);

        //        if (dsStaff != null)
        //        {
        //            if (dsStaff.Tables[0].Rows.Count > 0)
        //            {
        //                Staff stf = new Staff();
        //                stf.Name = "< Select Appraisee >";
        //                staffs.Add(stf);

        //                for (int i = 0; i < dsStaff.Tables[0].Rows.Count; i++)
        //                {
        //                    Staff staff = new Staff();
        //                    staff.Id = Convert.ToString(dsStaff.Tables[0].Rows[i][staffDb.FIELD_STAFF_ID]);
        //                    staff.Name = Convert.ToString(dsStaff.Tables[0].Rows[i][staffDb.FIELD_LAST_NAME]);
        //                    staff.JobRoleLevel.Id = Convert.ToString(dsStaff.Tables[0].Rows[i][staffDb.FIELD_JOB_ROLE_LEVEL_ID]);
        //                    staff.CompanyDepartmentJobRoleId = Convert.ToInt32(dsStaff.Tables[0].Rows[i]["Staff_Company_Department_Job_Role_ID"]);
        //                    staff.Email = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_EMAIL]);
        //                    staff.Location.Name = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_LOCATION_NAME]);
        //                    //staff.FormatNameToPascalcasing();

        //                    staffs.Add(staff);
        //                }

        //                return staffs;
        //            }
        //        }

        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<Staff> LoadHodAppraisees(int companyDepartmentJobRoleId, int periodId, byte optionId)
        {
            try
            {
                List<Staff> staffs = new List<Staff>();
                DataSet dsStaff = staffDb.SelectHodAppraiseesByCompanyDepartmentJobRoleIdAndPeriodIdAndOptionId(companyDepartmentJobRoleId, periodId, optionId);

                if (dsStaff != null)
                {
                    if (dsStaff.Tables[0].Rows.Count > 0)
                    {
                        Staff stf = new Staff();
                        stf.Name = "< Select Appraisee >";
                        staffs.Add(stf);

                        for (int i = 0; i < dsStaff.Tables[0].Rows.Count; i++)
                        {
                            Staff staff = new Staff();
                            staff.Id = Convert.ToString(dsStaff.Tables[0].Rows[i][staffDb.FIELD_STAFF_ID]);
                            staff.Name = Convert.ToString(dsStaff.Tables[0].Rows[i][staffDb.FIELD_LAST_NAME]);
                            staff.JobRoleLevel.Id = Convert.ToString(dsStaff.Tables[0].Rows[i][staffDb.FIELD_JOB_ROLE_LEVEL_ID]);
                            staff.CompanyDepartmentJobRoleId = Convert.ToInt32(dsStaff.Tables[0].Rows[i]["Staff_Company_Department_Job_Role_ID"]);
                            staff.Email = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_EMAIL]);
                            staff.Location.Name = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_LOCATION_NAME]);
                            //staff.FormatNameToPascalcasing();

                            staffs.Add(staff);
                        }

                        return staffs;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public Staff LoadAppraiseeSupervisor(int companyDepartmentJobRoleId)
        //{
        //    try
        //    {
        //        Staff staff = new Staff();
        //        DataSet dsStaff = staffDb.SelectStaffSupervisorByCompanyDepartmentJobRoleId(companyDepartmentJobRoleId);

        //        if (dsStaff != null)
        //        {
        //            if (dsStaff.Tables[0].Rows.Count > 0)
        //            {
        //                staff.Id = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_STAFF_ID]);
        //                staff.Name = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_LAST_NAME]);
        //                staff.JobRoleLevel.Id = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_JOB_ROLE_LEVEL_ID]);
        //                staff.CompanyDepartmentJobRoleId = Convert.ToInt32(dsStaff.Tables[0].Rows[0]["Staff_Company_Department_Job_Role_ID"]);
        //                staff.Email = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_EMAIL]);
        //                staff.Location.Name = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_LOCATION_NAME]);

        //                staff.FormatNameToPascalCasing();

        //                return staff;
        //            }
        //        }

        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public Staff LoadAppraiseeSupervisor(int companyDepartmentJobRoleId, int periodId)
        {
            try
            {
                Staff staff = new Staff();
                DataSet dsStaff = staffDb.SelectStaffSupervisorByCompanyDepartmentJobRoleIdAndPeriodId(companyDepartmentJobRoleId, periodId);

                if (dsStaff != null)
                {
                    if (dsStaff.Tables[0].Rows.Count > 0)
                    {
                        staff.Id = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_STAFF_ID]);
                        staff.Name = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_LAST_NAME]);
                        staff.JobRoleLevel.Id = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_JOB_ROLE_LEVEL_ID]);
                        staff.CompanyDepartmentJobRoleId = Convert.ToInt32(dsStaff.Tables[0].Rows[0]["Staff_Company_Department_Job_Role_ID"]);
                        staff.Email = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_EMAIL]);
                        staff.Location.Name = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_LOCATION_NAME]);

                        ////temp code
                        //staff.Name = UtilityLogic.JumbbleText(staff.Name);

                        staff.FormatNameToPascalCasing();

                        return staff;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Staff GetAppraiseeHod(int companyDepartmentJobRoleId, int periodId)
        {
            try
            {
                Staff staff = new Staff();
                DataSet dsStaff = staffDb.SelectStaffHodByCompanyDepartmentJobRoleIdAndPeriodId(companyDepartmentJobRoleId, periodId);

                if (dsStaff != null)
                {
                    if (dsStaff.Tables[0].Rows.Count > 0)
                    {
                        staff.Id = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_STAFF_ID]);
                        staff.Name = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_LAST_NAME]);
                        staff.JobRoleLevel.Id = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_JOB_ROLE_LEVEL_ID]);
                        staff.CompanyDepartmentJobRoleId = Convert.ToInt32(dsStaff.Tables[0].Rows[0]["Staff_Company_Department_Job_Role_ID"]);
                        staff.Email = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_EMAIL]);
                        staff.Location.Name = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_LOCATION_NAME]);

                        staff.FormatNameToPascalCasing();

                        return staff;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public Staff GetAppraiseeHod(int companyDepartmentJobRoleId)
        //{
        //    try
        //    {
        //        Staff staff = new Staff();
        //        DataSet dsStaff = staffDb.SelectStaffHodByCompanyDepartmentJobRoleId(companyDepartmentJobRoleId);

        //        if (dsStaff != null)
        //        {
        //            if (dsStaff.Tables[0].Rows.Count > 0)
        //            {
        //                staff.Id = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_STAFF_ID]);
        //                staff.Name = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_LAST_NAME]);
        //                staff.JobRoleLevel.Id = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_JOB_ROLE_LEVEL_ID]);
        //                staff.CompanyDepartmentJobRoleId = Convert.ToInt32(dsStaff.Tables[0].Rows[0]["Staff_Company_Department_Job_Role_ID"]);
        //                staff.Email = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_EMAIL]);
        //                staff.Location.Name = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_LOCATION_NAME]);

        //                staff.FormatNameToPascalCasing();

        //                return staff;
        //            }
        //        }

        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public Staff GetStaff(string staffId)
        //{
        //    Staff staff = new Staff();

        //    DataSet dsStaff = staffDb.SelectStaffByStaffId(staffId);
        //    if (dsStaff != null)
        //    {
        //        if (dsStaff.Tables[0].Rows.Count > 0)
        //        {
        //            staff.Id = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_STAFF_ID]);
        //            staff.Name = Convert.ToString(dsStaff.Tables[0].Rows[0]["Name"]);
        //            staff.LoginName = Convert.ToString(dsStaff.Tables[0].Rows[0]["Login_Name"]);
        //            staff.Email = Convert.ToString(dsStaff.Tables[0].Rows[0]["Email"]);
        //            staff.CompanyName = Convert.ToString(dsStaff.Tables[0].Rows[0]["Company_Name"]);
        //            staff.DepartmentName = Convert.ToString(dsStaff.Tables[0].Rows[0]["Department_Name"]);
        //            staff.JobRoleId = Convert.ToInt32(dsStaff.Tables[0].Rows[0]["Job_Role_ID"]);
        //            staff.JobRole = Convert.ToString(dsStaff.Tables[0].Rows[0]["Job_Role_Name"]);
        //            staff.LevelName = Convert.ToString(dsStaff.Tables[0].Rows[0]["Job_Role_Level_Name"]);
        //            staff.LevelId = Convert.ToString(dsStaff.Tables[0].Rows[0]["Job_Role_Level_ID"]);
        //            staff.CompanyDepartmentJobRoleId = Convert.ToInt32(dsStaff.Tables[0].Rows[0][staffDb.FIELD_COMPANY_DEPARTMENT_JOB_ROLE_ID]);
        //            staff.Location = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_LOCATION_NAME]);

        //            staff.FormatNameToPascalCasing();

        //            return staff;
        //        }
        //    }

        //    return null;
        //}

        //public Staff GetStaffByLoginName(string loginName)
        //{
        //    Staff staff = new Staff();

        //    DataSet dsStaff = staffDb.SelectStaffByLoginName(loginName);
        //    if (dsStaff != null)
        //    {
        //        if (dsStaff.Tables[0].Rows.Count > 0)
        //        {
        //            staff.Id = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_STAFF_ID]);
        //            staff.Name = Convert.ToString(dsStaff.Tables[0].Rows[0]["Name"]);
        //            staff.LoginName = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_LOGIN_NAME]);
        //            staff.Email = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_EMAIL]);
        //            staff.CompanyName = Convert.ToString(dsStaff.Tables[0].Rows[0]["Company_Name"]);
        //            staff.DepartmentName = Convert.ToString(dsStaff.Tables[0].Rows[0]["Department_Name"]);
        //            staff.JobRoleId = Convert.ToInt32(dsStaff.Tables[0].Rows[0]["Job_Role_ID"]);
        //            staff.JobRole = Convert.ToString(dsStaff.Tables[0].Rows[0]["Job_Role_Name"]);
        //            staff.LevelName = Convert.ToString(dsStaff.Tables[0].Rows[0]["Job_Role_Level_Name"]);
        //            staff.LevelId = Convert.ToString(dsStaff.Tables[0].Rows[0]["Job_Role_Level_ID"]);
        //            staff.CompanyDepartmentJobRoleId = Convert.ToInt32(dsStaff.Tables[0].Rows[0][staffDb.FIELD_COMPANY_DEPARTMENT_JOB_ROLE_ID]);
        //            staff.Location = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_LOCATION_NAME]);

        //            staff.FormatNameToPascalCasing();

        //            return staff;
        //        }
        //    }

        //    return null;
        //}


        //public List<Staff> LoadSupervisorAppraisees(int companyDepartmentJobRoleId)
        //{
        //    try
        //    {
        //        List<Staff> staffs = new List<Staff>();
        //        DataSet dsStaff = staffDb.SelectStaffByCompanyDepartmentJobRoleId(companyDepartmentJobRoleId);

        //        if (dsStaff != null)
        //        {
        //            if (dsStaff.Tables[0].Rows.Count > 0)
        //            {
        //                Staff stf = new Staff();
        //                stf.Name = "< Select Appraisee >";
        //                staffs.Add(stf);

        //                for (int i = 0; i < dsStaff.Tables[0].Rows.Count; i++)
        //                {
        //                    Staff staff = new Staff();
        //                    staff.Id = Convert.ToString(dsStaff.Tables[0].Rows[i][staffDb.FIELD_STAFF_ID]);
        //                    staff.Name = Convert.ToString(dsStaff.Tables[0].Rows[i][staffDb.FIELD_LAST_NAME]);
        //                    staff.JobRoleLevelId = Convert.ToString(dsStaff.Tables[0].Rows[i][staffDb.FIELD_JOB_ROLE_LEVEL_ID]);
        //                    staff.CompanyDepartmentJobRoleId = Convert.ToInt32(dsStaff.Tables[0].Rows[i]["Staff_Company_Department_Job_Role_ID"]);
        //                    staff.Email = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_EMAIL]);
        //                    staff.Location = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_LOCATION_NAME]);
        //                    //staff.FormatNameToPascalcasing();

        //                    staffs.Add(staff);
        //                }

        //                return staffs;
        //            }
        //        }

        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<Staff> LoadHodAppraisees(int companyDepartmentJobRoleId, int periodId, byte optionId)
        //{
        //    try
        //    {
        //        List<Staff> staffs = new List<Staff>();
        //        DataSet dsStaff = staffDb.SelectHodAppraiseesByCompanyDepartmentJobRoleIdAndPeriodIdAndOptionId(companyDepartmentJobRoleId, periodId, optionId);

        //        if (dsStaff != null)
        //        {
        //            if (dsStaff.Tables[0].Rows.Count > 0)
        //            {
        //                Staff stf = new Staff();
        //                stf.Name = "< Select Appraisee >";
        //                staffs.Add(stf);

        //                for (int i = 0; i < dsStaff.Tables[0].Rows.Count; i++)
        //                {
        //                    Staff staff = new Staff();
        //                    staff.Id = Convert.ToString(dsStaff.Tables[0].Rows[i][staffDb.FIELD_STAFF_ID]);
        //                    staff.Name = Convert.ToString(dsStaff.Tables[0].Rows[i][staffDb.FIELD_LAST_NAME]);
        //                    staff.JobRoleLevelId = Convert.ToString(dsStaff.Tables[0].Rows[i][staffDb.FIELD_JOB_ROLE_LEVEL_ID]);
        //                    staff.CompanyDepartmentJobRoleId = Convert.ToInt32(dsStaff.Tables[0].Rows[i]["Staff_Company_Department_Job_Role_ID"]);
        //                    staff.Email = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_EMAIL]);
        //                    staff.Location = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_LOCATION_NAME]);
        //                    //staff.FormatNameToPascalcasing();

        //                    staffs.Add(staff);
        //                }

        //                return staffs;
        //            }
        //        }

        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public Staff LoadAppraiseeSupervisor(int companyDepartmentJobRoleId)
        //{
        //    try
        //    {
        //        Staff staff = new Staff();
        //        DataSet dsStaff = staffDb.SelectStaffSupervisorByCompanyDepartmentJobRoleId(companyDepartmentJobRoleId);

        //        if (dsStaff != null)
        //        {
        //            if (dsStaff.Tables[0].Rows.Count > 0)
        //            {
        //                staff.Id = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_STAFF_ID]);
        //                staff.Name = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_LAST_NAME]);
        //                staff.JobRoleLevelId = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_JOB_ROLE_LEVEL_ID]);
        //                staff.CompanyDepartmentJobRoleId = Convert.ToInt32(dsStaff.Tables[0].Rows[0]["Staff_Company_Department_Job_Role_ID"]);
        //                staff.Email = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_EMAIL]);
        //                staff.Location = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_LOCATION_NAME]);

        //                staff.FormatNameToPascalCasing();

        //                return staff;
        //            }
        //        }

        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public Staff GetAppraiseeHod(int companyDepartmentJobRoleId)
        //{
        //    try
        //    {
        //        Staff staff = new Staff();
        //        DataSet dsStaff = staffDb.SelectStaffHodByCompanyDepartmentJobRoleId(companyDepartmentJobRoleId);

        //        if (dsStaff != null)
        //        {
        //            if (dsStaff.Tables[0].Rows.Count > 0)
        //            {
        //                staff.Id = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_STAFF_ID]);
        //                staff.Name = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_LAST_NAME]);
        //                staff.JobRoleLevelId = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_JOB_ROLE_LEVEL_ID]);
        //                staff.CompanyDepartmentJobRoleId = Convert.ToInt32(dsStaff.Tables[0].Rows[0]["Staff_Company_Department_Job_Role_ID"]);
        //                staff.Email = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_EMAIL]);
        //                staff.Location = Convert.ToString(dsStaff.Tables[0].Rows[0][staffDb.FIELD_LOCATION_NAME]);

        //                staff.FormatNameToPascalCasing();

        //                return staff;
        //            }
        //        }

        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public byte GetStaffType(int companyDepartmentJobRoleId, int periodId)
        {
            try
            {
                Staff staff = new Staff();
                DataSet dsStaff = staffDb.SelectStaffTypeByCompanyDepartmentJobRoleId(companyDepartmentJobRoleId, periodId);

                byte staffType = 0;

                if (dsStaff != null)
                {
                    int hodCount = dsStaff.Tables[0].Rows.Count;
                    int supervisorCount = dsStaff.Tables[1].Rows.Count;

                    //if (companyDepartmentJobRoleId == 42)
                    //{
                    //    staffType = (byte)Staff.Category.Admin;
                    //}
                    if (hodCount > 0)
                    {
                        staffType = (byte)Staff.Category.Hod;
                    }
                    else if (hodCount <= 0 && supervisorCount > 0)
                    {
                        staffType = (byte)Staff.Category.Supervisor;
                    }
                    else
                    {
                        staffType = (byte)Staff.Category.Staff;
                    }                   
                }

                return staffType;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





    }


}