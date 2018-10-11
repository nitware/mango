using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mango.Model.Model
{
    [DataContract]
    public class StaffInps
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public StaffMetric StaffMetric { get; set; }
        [DataMember]
        public Appraisal Appraisal { get; set; }
        [DataMember]
        public Inps Inps { get; set; }
        [DataMember]
        public decimal? Score { get; set; }

    }
}
