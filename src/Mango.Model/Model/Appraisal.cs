using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;
using Mango.Model.Model;

namespace Mango.Model
{
    [DataContract]
    public class Appraisal
    {
        //[DataMember]
        //public long Id { get; set; }
        //[DataMember]
        //public string StaffId { get; set; }
        //[DataMember]
        //public int PeriodId { get; set; }
        //[DataMember]
        //public string SupervisorId { get; set; }
        //[DataMember]
        //public DateTime SupervisorAppraisalDate { get; set; }
        //[DataMember]
        //public DateTime StaffResponseDate { get; set; }
        //[DataMember]
        //public string HodId { get; set; }
        //[DataMember]
        //public DateTime HodAppraisalDate { get; set; }
        //[DataMember]
        //public byte StatusId { get; set; }
        //[DataMember]
        //public string Status { get; set; }


        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public Period Period { get; set; }
        [DataMember]
        public Staff Staff { get; set; }
        [DataMember]
        public Staff Supervisor { get; set; }
        [DataMember]
        public DateTime AppraisalDate { get; set; }
        [DataMember]
        public DateTime? ResponseDate { get; set; }
        [DataMember]
        public Staff Hod { get; set; }
        [DataMember]
        public DateTime? HodAppraisalDate { get; set; }
        [DataMember]
        public Status Status { get; set; }
        


    }



}