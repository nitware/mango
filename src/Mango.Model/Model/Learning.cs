using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;

namespace Mango.Model
{
    [DataContract]
    public class Learning
    {
        [DataMember]
        public string StaffId { get; set; }
        [DataMember]
        public int PeriodId { get; set; }
        [DataMember]
        public decimal TrainingScore { get; set; }
        [DataMember]
        public decimal PercentageScore { get; set; }
    }



}