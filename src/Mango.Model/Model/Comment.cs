using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;

namespace Mango.Model
{
    [DataContract]
    public class Comment
    {
        //[DataMember]
        //public int Id { get; set; }

        //[DataMember]
        //public string StaffId { get; set; }
        //[DataMember]
        //public int PeriodId { get; set; }

        [DataMember]
        public long AppraisalHeaderId { get; set; }
        [DataMember]
        public byte RecommendationId { get; set; }
        [DataMember]
        public byte CommentId { get; set; }
        [DataMember]
        public byte OptionId { get; set; }
        [DataMember]
        public string Strenght { get; set; }
        [DataMember]
        public string Weakness { get; set; }
        [DataMember]
        public string TrainingNeed { get; set; }
        [DataMember]
        public string SupervisorComment { get; set; }
        [DataMember]
        public string AppraiseeComment { get; set; }
        [DataMember]
        public string HodComment { get; set; }

    }


}