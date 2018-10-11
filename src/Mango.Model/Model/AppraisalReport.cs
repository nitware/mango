using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;

namespace Mango.Model
{
    [DataContract]
    public class AppraisalReport
    {
        [DataMember]
        public string CompanyName { get; set; }
        [DataMember]
        public string DepartmentName { get; set; }
        [DataMember]
        public string StaffId { get; set; }
        [DataMember]
        public string StaffName { get; set; }
        [DataMember]
        public string SupervisorId { get; set; }
        [DataMember]
        public string SupervisorName { get; set; }
        [DataMember]
        public string JobRoleLevelName { get; set; }
        [DataMember]
        public string JobRoleName { get; set; }
        [DataMember]
        public decimal PaceScore { get; set; }
        [DataMember]
        public string PaceGrade { get; set; }
        [DataMember]
        public double MetricScore { get; set; }
        [DataMember]
        public byte MetricRating { get; set; }
        [DataMember]
        public string Recommendation { get; set; }
        [DataMember]
        public string PeriodName { get; set; }

        [DataMember]
        public string Strength { get; set; }
        [DataMember]
        public string Weakness { get; set; }
        [DataMember]
        public string TrainingNeeds { get; set; }
        [DataMember]
        public string SupervisorComment { get; set; }
        [DataMember]
        public string AppraiseeComment { get; set; }
        [DataMember]
        public string HodComment { get; set; }
    }



}