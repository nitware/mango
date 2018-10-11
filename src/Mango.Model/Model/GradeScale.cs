using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace Mango.Model
{
    [DataContract]
    public class GradeScale
    {
        [DataMember]
        public byte Id { get; set; }
        [DataMember]
        public decimal From { get; set; }
        [DataMember]
        public decimal To { get; set; }
    }


}