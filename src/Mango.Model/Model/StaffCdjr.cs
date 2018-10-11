using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Mango.Model.Model
{
    [DataContract]
    public class StaffCdjr
    {
        [DataMember]
        public Staff Staff { get; set; }
        [DataMember]
        public CompanyDepartmentJobRole CompanyDepartmentJobRole { get; set; }
        [DataMember]
        public Period Period { get; set; }
    }


}
