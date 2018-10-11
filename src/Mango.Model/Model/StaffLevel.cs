using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Mango.Model.Model
{
    [DataContract]
    public class StaffLevel
    {
        [DataMember]
        public Staff Staff { get; set; }
        [DataMember]
        public Level Level { get; set; }
        [DataMember]
        public Period Period { get; set; }
    }


}
