using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;
using Mango.Model.Model;

namespace Mango.Model
{
    [DataContract]
    public class Period
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public byte StatusID { get; set; }
        [DataMember]
        public Status Status { get; set; }
        //[DataMember]
        //public string Type { get; set; }

        [DataMember]
        public PeriodType Type { get; set; }

        [DataMember]
        public byte Span { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }

        [DataMember]
        public int Year { get; set; }
    }


}