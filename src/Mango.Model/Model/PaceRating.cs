using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;

namespace Mango.Model
{
    [DataContract]
    public class PaceRating
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Rating { get; set; }
        [DataMember]
        public decimal From { get; set; }
        [DataMember]
        public decimal To { get; set; }
        [DataMember]
        public byte Grade { get; set; }
        [DataMember]
        public string Definition { get; set; }
    }


}