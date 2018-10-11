using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Mango.Model.Model
{
    [DataContract]
    public class Status
    {
        [DataMember]
        public byte Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }




}
