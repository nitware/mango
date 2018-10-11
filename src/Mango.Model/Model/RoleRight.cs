using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Mango.Model
{
    [DataContract]
    public class RoleRight
    {
        [DataMember]
        public Role Role { get; set; }
        [DataMember]
        public Right Right { get; set; }
        [DataMember]
        public string Description { get; set; }
    }




}
