using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mango.Model.Model
{
    [DataContract]
    public class CompanyJobRole
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public Company Company { get; set; }
        [DataMember]
        public JobRole JobRole { get; set; }
        [DataMember]
        public string Description { get; set; }
    }


}
