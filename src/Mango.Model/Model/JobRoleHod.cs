﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Mango.Model.Model
{
    [DataContract]
    public class JobRoleHod
    {
        [DataMember]
        public CompanyDepartmentJobRole HodCompanyDepartmentJobRole { get; set; }
        [DataMember]
        public CompanyDepartmentJobRole StaffCompanyDepartmentJobRole { get; set; }
        [DataMember]
        public Period Period { get; set; }
    }
}
