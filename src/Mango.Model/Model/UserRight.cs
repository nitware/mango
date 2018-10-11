using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Mango.Model
{
    [DataContract]
    public class UserRight
    {
        [DataMember]
        public List<Right> Rights { get; set; }
        [DataMember]
        public List<Right> View { get; set; }

        [DataMember]
        public bool CanSetupUser { get; set; }
        [DataMember]
        public bool CanSetupRole { get; set; }
        [DataMember]
        public bool CanSetupRight { get; set; }
        [DataMember]
        public bool CanAssignRightToRole { get; set; }
        [DataMember]
        public bool CanAssignRoleToUser { get; set; }

        [DataMember]
        public bool CanDoAppraisal { get; set; }
        [DataMember]
        public bool CanAppraiseStaff { get; set; }
        [DataMember]
        public bool CanAcceptOrRejectAppraisal { get; set; }
        [DataMember]
        public bool CanViewReports { get; set; }
       
        [DataMember]
        public bool CanManageUser { get; set; }
        [DataMember]
        public bool CanManageReport { get; set; }
        [DataMember]
        public bool CanManageMarkOperation { get; set; }

        [DataMember]
        public bool CanSetupJobRole { get; set; }
        [DataMember]
        public bool CanSetupJobLevel { get; set; }
        [DataMember]
        public bool CanSetupDepartment { get; set; }
        [DataMember]
        public bool CanSetupStaffLevel { get; set; }
        [DataMember]
        public bool CanSetupCdjr { get; set; }
        [DataMember]
        public bool CanAssignRolesUnderSupervisor { get; set; }
        [DataMember]
        public bool CanAssignRolesUnderHod { get; set; }
        [DataMember]
        public bool CanSetupMetricRating { get; set; }
        [DataMember]
        public bool CanSetupMetrics { get; set; }
        [DataMember]
        public bool CanCreateNewPeriod { get; set; }
        [DataMember]
        public bool CanModifyPeriod { get; set; }
        [DataMember]
        public bool CanSetCurrentPeriod { get; set; }

        [DataMember]
        public bool CanManageSetup { get; set; }
        
        //9	Can Setup Job role--
        //10	Can Setup Job Level--
        //11	Can Setup Department--
        //12	Can Setup Staff Level--
        //13	Can Setup CDJR--
        //14	Can Assign Roles Under Supervisor--
        //15	Can Assign Roles Under Hod--
        //16	Can Setup Metric Rating--
        //17	Can Setup Metrics--
        //18	Can Create New Period--
        //19	Can Modify Period--
        //20	Can Set Current Period--


        public void Set()
        {
            if (Rights != null)
            {
                if (Rights.Count > 0)
                {
                    foreach (Right right in Rights)
                    {
                        switch (right.Id)
                        {
                            case 1:
                                {
                                    CanSetupUser = true;
                                    break;
                                }
                            case 2:
                                {
                                    CanSetupRole = true;
                                    break;
                                }
                            case 3:
                                {
                                    CanSetupRight = true;
                                    break;
                                }
                            case 4:
                                {
                                    CanAssignRightToRole = true;
                                    break;
                                }
                            case 5:
                                {
                                    CanAssignRoleToUser = true;
                                    break;
                                }
                            case 6:
                                {
                                    CanAppraiseStaff = true;
                                    break;
                                }
                            case 7:
                                {
                                    CanAcceptOrRejectAppraisal = true;
                                    break;
                                }
                            case 8:
                                {
                                    CanViewReports = true;
                                    break;
                                }
                            case 9:
                                {
                                    CanSetupJobRole = true;
                                    break;
                                }
                            case 10:
                                {
                                    CanSetupJobLevel = true;
                                    break;
                                }
                            case 11:
                                {
                                   CanSetupDepartment = true;
                                    break;
                                }
                            case 12:
                                {
                                    CanSetupStaffLevel = true;
                                    break;
                                }
                            case 13:
                                {
                                    CanSetupCdjr = true;
                                    break;
                                }
                            case 14:
                                {
                                    CanAssignRolesUnderSupervisor = true;
                                    break;
                                }
                            case 15:
                                {
                                    CanAssignRolesUnderHod = true;
                                    break;
                                }
                            case 16:
                                {
                                    CanSetupMetricRating = true;
                                    break;
                                }
                            case 17:
                                {
                                    CanSetupMetrics = true;
                                    break;
                                }
                            case 18:
                                {
                                    CanCreateNewPeriod = true;
                                    break;
                                }
                            case 19:
                                {
                                    CanModifyPeriod = true;
                                    break;
                                }
                            case 20:
                                {
                                    CanSetCurrentPeriod = true;
                                    break;
                                }
                           
                        }

                        //can manage user
                        if (CanSetupUser == false && CanSetupRole == false && CanSetupRight == false && CanAssignRightToRole == false && CanAssignRoleToUser == false)
                        {
                            CanManageUser = false;
                        }
                        else
                        {
                            CanManageUser = true;
                        }

                        //can manage setup
                        if (CanSetupJobRole == false && CanSetupJobLevel == false && CanSetupDepartment == false && CanSetupStaffLevel == false && CanSetupCdjr == false && CanAssignRolesUnderSupervisor == false && CanAssignRolesUnderHod == false && CanSetupMetricRating == false && CanSetupMetrics == false && CanCreateNewPeriod == false && CanModifyPeriod == false && CanSetCurrentPeriod == false)
                        {
                            CanManageSetup = false;
                        }
                        else
                        {
                            CanManageSetup = true;
                        }

                        //can view report
                        if (CanViewReports == false)
                        {
                            CanManageReport = false;
                        }
                        else
                        {
                            CanManageReport = true;
                        }
                                                
                        //can view report
                        if (CanAppraiseStaff == false && CanAcceptOrRejectAppraisal == false)
                        {
                            CanDoAppraisal = false;
                        }
                        else
                        {
                            CanDoAppraisal = true;
                        }
                    }



                }
            }



        }



    }
}
