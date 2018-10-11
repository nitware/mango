using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Mango.Model.Model
{
    [DataContract]
    public class CompanyDepartmentJobRole
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public Company Company { get; set; }
        [DataMember]
        public Department Department { get; set; }
        [DataMember]
        public JobRole JobRole { get; set; }
        [DataMember]
        public string Description { get; set; }
    }



}
