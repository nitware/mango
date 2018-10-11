using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;

namespace Mango.Model
{
    [DataContract]
    public class Recommendation
    {
        [DataMember]
        public byte Id { get; set; }
        [DataMember]
        public string Name { get; set; }


    }



}