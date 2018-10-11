using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;

namespace Mango.Model
{
    [DataContract]
    public class StaffMetric
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long AppraisalHeaderId { get; set; }
        [DataMember]
        public long MetricId { get; set; }
        [DataMember]
        public decimal Score { get; set; }

    }



}