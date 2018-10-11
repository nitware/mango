using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Mango.Model
{
    [DataContract]
    public class ScoreSummary
    {
        [DataMember]
        public string StaffId { get; set; }
        [DataMember]
        public string StaffName { get; set; }
        [DataMember]
        public double? Caterer1 { get; set; }
        [DataMember]
        public double? Caterer2 { get; set; }
       
    }


}
