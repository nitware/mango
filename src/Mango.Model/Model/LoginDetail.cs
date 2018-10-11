using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Mango.Model
{
    [DataContract]
    public class LoginDetail
    {
        [DataMember]
        public Staff Staff { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public byte[] Password { get; set; }
        [DataMember]
        public bool IsActivated { get; set; }
        [DataMember]
        public bool IsLocked { get; set; }
        [DataMember]
        public bool IsFirstLogon { get; set; }

    }


}
