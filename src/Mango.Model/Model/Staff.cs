using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;
using System.Collections;

namespace Mango.Model
{
    [DataContract]
    public class Staff
    {
        public enum Category
        {
            Staff = 1,
            Supervisor = 2,
            Hod = 3,
            Admin = 4
        }

        public Staff()
        {
            Location = new Location();
            Company = new Company();
            Department = new Department();
            JobRole = new JobRole();
            JobRoleLevel = new JobRoleLevel();
            Level = new Level();
        }

        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string OtherName { get; set; }
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public string LoginName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public Role Role { get; set; }
        [DataMember]
        public int CompanyDepartmentJobRoleId { get; set; }
        [DataMember]
        public bool IsSupervisor { get; set; }
        [DataMember]
        public byte Type { get; set; }
       

        [DataMember]
        public Location Location { get; set; }
        [DataMember]
        public Company Company { get; set; }
        [DataMember]
        public Department Department { get; set; }
        [DataMember]
        public JobRole JobRole { get; set; }
        [DataMember]
        public JobRoleLevel JobRoleLevel { get; set; }
        [DataMember]
        public Level Level { get; set; }

        [DataMember]
        public int MetricsCount { get; set; }
        [DataMember]
        public bool IsAdmin { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
    
        public void FormatNameToPascalCasing()
        {
            try
            {
                string newName = null;

                if (Name != "")
                {
                    string[] nameString = Name.Split(Convert.ToChar(" "));
                    if (nameString != null)
                    {
                        if (nameString.Length > 0)
                        {
                            //string newName;
                            for (int i = 0; i < nameString.Length; i++)
                            {
                                if (nameString[i] != "")
                                {
                                    string firstchar = nameString[i].Substring(0, 1);
                                    string otherChars = nameString[i].Substring(1, nameString[i].Length - 1);
                                    newName += firstchar.ToUpper() + otherChars.ToLower() + " ";
                                }
                            }

                            Name = newName;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






    }




}